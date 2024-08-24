using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using Cinemachine.Utility;
using Helikopter.Managers;

namespace Helikopter
{
    public class HelicopterMainEngine : MonoBehaviour
    {
        private Rigidbody helicopterRigid;

        [SerializeField]
        private BladesRotator mainBladeRotator;

        [SerializeField]
        private BladesRotator subBladeRotator;

        [SerializeField]
        private GameObject model;

        #region Configures

        #region StartAndStopEngine

        [Header("StartEngine")]
        [SerializeField]
        private float startEngineDuration;

        [SerializeField]
        private float stopEngineDuration;

        [SerializeField]
        private float startEngineSpeed;

        #endregion

        [Header("Up/Down")]
        [SerializeField]
        private float engineLift = 0.0075f;

        [SerializeField]
        private float effectiveHeight;

        [Header("Forward/Backward")]
        [SerializeField]
        private float forwardForce;

        [SerializeField]
        private float backwardForce;

        [Header("Turn")]
        [SerializeField]
        private float turnForce;

        [SerializeField]
        private float rotateSpeed;

        [SerializeField]
        private float turnForceHelper = 1.5F;

        [Header("Tilts")]
        [SerializeField]
        private float forwardTiltForce;

        [SerializeField]
        private float turnTiltForce;

        [Header("Ground Check")]
        [SerializeField]
        private LayerMask groundLayerMask;

        #endregion

        #region Variables

        [SerializeField]
        private bool isOnGround;

        private HelicopterStates state;

        private float enginePower;

        private float turning;

        private Vector2 movement = Vector2.zero;

        private RaycastHit groundRaycastHit;

        private Vector2 tilting = Vector2.zero;

        private Transform cameraTransform;

        private Vector3 cameraForward;

        private Vector3 cameraRight;

        #endregion

        #region Events

        private Action onTakeOff;

        private Action onLand;

        #endregion

        #region Properties

        public float EnginePower
        {
            get => enginePower;
            set
            {
                enginePower = value;
                mainBladeRotator.BladeSpeed = enginePower * 250;
                subBladeRotator.BladeSpeed = enginePower * 500;
            }
        }

        #endregion

        #region MonoBehaviour

        private void Awake()
        {
            helicopterRigid = GetComponent<Rigidbody>();
            state = HelicopterStates.OnGround;

            cameraTransform = Camera.main.transform;
            tilting = new Vector2();

            onTakeOff += GetComponentInChildren<BrownianMotionController>().StartMotion;
            onLand += GetComponentInChildren<BrownianMotionController>().StopMotion;
        }

        private void Update()
        {
            HandleGroundCheck();
            HandleInputs();
        }

        private void FixedUpdate()
        {
            HandleRotateTowardCameraFoward();
            HelicopterHover();
            HelicopterMovements();
            HelicopterTilting();
        }

        private void OnDestroy()
        {
            onTakeOff -= GetComponentInChildren<BrownianMotionController>().StartMotion;
            onLand -= GetComponentInChildren<BrownianMotionController>().StopMotion;
        }

        #endregion

        #region Logics

        private void HandleInputs()
        {
            if (!isOnGround)
            {
                movement.x = InputManager.Instance.MoveInput.x;
                movement.y = InputManager.Instance.MoveInput.y;

                if (Input.GetKeyDown(KeyCode.C))
                {
                    EnginePower -= engineLift;

                    if (EnginePower <= 0)
                    {
                        EnginePower = 0;
                    }
                }
            }

            if (Input.GetAxis("Throttle") > 0)
            {
                EnginePower += engineLift;
            }
            else if (movement.x > 0 && !isOnGround)
            {
                EnginePower = Mathf.Lerp(EnginePower, 17.5f, 0.003f);

            }
            else if (Input.GetAxis("Throttle") < 0.5f && !isOnGround)
            {
                EnginePower = Mathf.Lerp(EnginePower, 10, 0.003f);
            }
        }

        private void HandleRotateTowardCameraFoward()
        {
            if (isOnGround)
            {
                return;
            }

            Vector3 targetDirection = cameraTransform.forward.ProjectOntoPlane(Vector3.up);
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            Quaternion smoothedRotation = Quaternion.Slerp(helicopterRigid.rotation, targetRotation, rotateSpeed * Time.fixedDeltaTime);
            helicopterRigid.MoveRotation(smoothedRotation);
        }

        private void HelicopterHover()
        {
            float upForce = 1 - Math.Clamp(helicopterRigid.transform.position.y / effectiveHeight, 0, 1);
            upForce = Mathf.Lerp(0, EnginePower, upForce) * helicopterRigid.mass;
            helicopterRigid.AddRelativeForce(Vector3.up * upForce);
        }

        private void HelicopterMovements()
        {
            if (isOnGround)
            {
                return;
            }

            helicopterRigid.AddForce(transform.forward * movement.y * Mathf.Max(0, forwardForce * helicopterRigid.mass) * Time.fixedDeltaTime, ForceMode.Impulse);
            helicopterRigid.AddForce(transform.right * movement.x * Mathf.Max(0, turnForce * helicopterRigid.mass) * Time.fixedDeltaTime, ForceMode.Impulse);
        }

        private void HelicopterTilting()
        {
            if (isOnGround)
            {
                return;
            }

            tilting.y = Mathf.Lerp(tilting.y, movement.y * forwardTiltForce, Time.fixedDeltaTime);
            tilting.x = Mathf.Lerp(tilting.x, movement.x * turnTiltForce, Time.fixedDeltaTime);
            model.transform.localRotation = Quaternion.Euler(-tilting.y, model.transform.localEulerAngles.y, tilting.x);
        }

        private void HandleGroundCheck()
        {
            Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.down).normalized);

            if (Physics.Raycast(ray, out groundRaycastHit, 3000, groundLayerMask))
            {
                isOnGround = groundRaycastHit.distance < 1;
            }

            Debug.DrawLine(transform.position, isOnGround ? groundRaycastHit.point : ray.direction * 100, isOnGround ? Color.green : Color.red);
        }

        #region StartAndStopEngine

        public void StartEngine()
        {
            DOTween.To(Starting, 0, startEngineSpeed, startEngineDuration).OnComplete(() =>
            {
                onTakeOff?.Invoke();
                state = HelicopterStates.OnAir;
            });
        }

        public void Starting(float value)
        {
            EnginePower = value;
        }

        public void StopEngine()
        {
            DOTween.To(Stopping, 0, 0, stopEngineDuration).OnComplete(() =>
            {
                onLand?.Invoke();
                state = HelicopterStates.OnGround;
            });
        }

        public void Stopping(float value)
        {
            EnginePower = value;
        }

        #endregion

        #endregion
    }
}