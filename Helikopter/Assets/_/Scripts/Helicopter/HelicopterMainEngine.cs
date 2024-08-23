using System;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

namespace Helikopter
{
    public class HelicopterMainEngine : MonoBehaviour
    {
        private Rigidbody helicopterRigid;

        [SerializeField]
        private BladesRotator mainBladeRotator;

        [SerializeField]
        private BladesRotator subBladeRotator;

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

        private HelicopterStates state;

        private float enginePower;

        private float turning;

        private Vector2 movement = Vector2.zero;

        private bool isOnGround;

        private RaycastHit groundRaycastHit;

        private Vector2 tilting = Vector2.zero;

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

            onTakeOff += GetComponentInChildren<BrownianMotionController>().StartMotion;
            onLand += GetComponentInChildren<BrownianMotionController>().StopMotion;
        }

        private void Update()
        {
            HandleGroundCheck();
            HandleInputs();
            HandleInvokes();
            HandleEngine();
        }

        private void FixedUpdate()
        {
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
                movement.x = Input.GetAxis("Horizontal");
                movement.y = Input.GetAxis("Vertical");

                if (Input.GetKey(KeyCode.C))
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
            else if (Input.GetAxis("Vertical") > 0 && !isOnGround)
            {
                EnginePower = Mathf.Lerp(EnginePower, 17.5f, 0.003f);

            }
            else if (Input.GetAxis("Throttle") < 0.5f && !isOnGround)
            {
                EnginePower = Mathf.Lerp(EnginePower, 10, 0.003f);
            }
        }

        private void HandleEngine()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                StartEngine();
            }
            else if (Input.GetKeyDown(KeyCode.Y) && isOnGround)
            {
                StopEngine();
            }
        }

        private void HandleInvokes()
        {
            if (!isOnGround && state == HelicopterStates.OnGround)
            {
                onTakeOff?.Invoke();
                state = HelicopterStates.OnAir;
            }
            else if (isOnGround && state == HelicopterStates.OnAir)
            {
                onLand?.Invoke();
                state = HelicopterStates.OnGround;
            }
        }

        private void HelicopterHover()
        {
            float upForce = 1 - Math.Clamp(helicopterRigid.transform.position.y / effectiveHeight, 0, 1);
            upForce = Mathf.Lerp(0, EnginePower, upForce) * helicopterRigid.mass;
            helicopterRigid.AddRelativeForce(Vector3.up * upForce);
        }

        private void HelicopterMovements()
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                helicopterRigid.AddRelativeForce(Vector3.forward * Mathf.Max(0, movement.y * forwardForce * helicopterRigid.mass));
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                helicopterRigid.AddRelativeForce(Vector3.back * Mathf.Max(0, -movement.y * backwardForce * helicopterRigid.mass));
            }

            float turn = turnForce * Mathf.Lerp(movement.x, movement.x * (turnForceHelper - Mathf.Abs(movement.y)), Mathf.Max(0, movement.y));
            turning = Mathf.Lerp(turning, turn, Time.fixedDeltaTime * turnForce);
            helicopterRigid.AddRelativeTorque(0, turning * helicopterRigid.mass, 0);
        }

        private void HelicopterTilting()
        {
            tilting.y = Mathf.Lerp(tilting.y, movement.y * forwardTiltForce, Time.fixedDeltaTime);
            tilting.x = Mathf.Lerp(tilting.x, movement.x * turnTiltForce, Time.fixedDeltaTime);

            helicopterRigid.transform.localRotation = Quaternion.Euler(tilting.y, helicopterRigid.transform.localEulerAngles.y, -tilting.x);
        }

        private void HandleGroundCheck()
        {
            Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.down));

            if (Physics.Raycast(ray, out groundRaycastHit, 3000, groundLayerMask))
            {
                isOnGround = groundRaycastHit.distance < 2;
            }
        }

        #region StartAndStopEngine

        public void StartEngine()
        {
            DOTween.To(Starting, 0, startEngineSpeed, startEngineDuration);
        }

        public void Starting(float value)
        {
            EnginePower = value;
        }

        public void StopEngine()
        {
            DOTween.To(Stopping, 0, 0, stopEngineDuration);
        }

        public void Stopping(float value)
        {
            EnginePower = value;
        }

        #endregion

        #endregion
    }
}