using Helikopter.Managers;
using System;
using UnityEngine;

namespace Helikopter
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private Transform target;

        [SerializeField]
        private Vector3 offset = new Vector3(0, 2, -10);

        [SerializeField]
        private Vector3 lookAtOffset;

        [SerializeField]
        private float smoothTime = 0.1f;

        [SerializeField]
        private float rotationSpeed = 5f;

        private Vector2 currentMouseLook;

        private Vector3 velocity = Vector3.zero;

        [SerializeField]
        private float minYRotation = -60f; // Minimum X rotation angle

        [SerializeField]
        private float maxYRotation = 60f;

        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
            transform.SetParent(newTarget);

            if (target)
            {
                transform.SetLocalPositionAndRotation(Vector3.one, Quaternion.identity);
            }
        }

        private void LateUpdate()
        {
            if (!target)
            {
                return;
            }

            // Get mouse input for camera rotation
            Vector2 mouseDelta = new Vector2(InputManager.Instance.LookInput.x, InputManager.Instance.LookInput.y);
            currentMouseLook.x += mouseDelta.x * rotationSpeed * Time.deltaTime;
            currentMouseLook.y -= mouseDelta.y * rotationSpeed * Time.deltaTime;

            currentMouseLook.y = Mathf.Clamp(currentMouseLook.y, minYRotation, maxYRotation);  // Clamp vertical rotation

            // Calculate target rotation
            Quaternion targetRotation = Quaternion.Euler(currentMouseLook.y, currentMouseLook.x, 0);

            // Calculate the desired camera position with the offset
            Vector3 desiredPosition = target.position + targetRotation * offset;

            // Smoothly rotate the camera towards the target rotation
            transform.SetPositionAndRotation(
                Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime),
                Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime)
            );

            // Ensure the camera is looking at the target
            transform.LookAt(target);
        }
    }
}