using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Helikopter.UI
{
    public class TouchPad : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        #region Configure

        [SerializeField]
        private float sensitivity = 1f;

        #endregion

        private bool dragging;

        private PointerEventData pointerData;

        private Vector3 previousTouchPos;

        private Vector3 pointerDelta;

        private float maxScreenSide;

        private Vector3 lastInput;

        public Vector3 Direction { get; private set; }

        #region MonoBehaviour

        private void Awake()
        {
            maxScreenSide = Mathf.Max(Screen.width, Screen.height);
        }

        private void Update()
        {
            if (!dragging)
            {
                return;
            }

            Vector3 inputPosition = GetInputPosition();
            pointerDelta = Vector3.Lerp((inputPosition - previousTouchPos) / maxScreenSide, pointerDelta, 0.3f);
            previousTouchPos = inputPosition;

            Direction = pointerDelta * sensitivity;
        }

        #endregion

        public void OnPointerDown(PointerEventData eventData)
        {
            dragging = true;
            pointerData = eventData;
            previousTouchPos = eventData.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            dragging = false;
            pointerData = null;
        }

        private Vector3 GetInputPosition()
        {
            if (pointerData != null)
            {
                lastInput = pointerData.position;
            }
            return lastInput;
        }
    }
}