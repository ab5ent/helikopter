using UnityEngine;
using UnityEngine.UI;

namespace Helikopter.UserInterface
{
    public class UIGameplayControl : MonoBehaviour
    {
        [SerializeField]
        private MoveJoystickFetch moveJoystickFetch;

        [SerializeField]
        private LookTouchPadFetch lookTouchPadFetch;

        [SerializeField]
        private Button buttonStartEngine;

        public void Initialize()
        {
            moveJoystickFetch.Initialize();
            lookTouchPadFetch.Initialize();

            buttonStartEngine.onClick.AddListener(GameManager.Instance.CurrentHelicopter.StartEngine);
        }

        private void OnEnable()
        {
            buttonStartEngine.gameObject.SetActive(!GameManager.Instance.HelicopterManager.IsStartedEngine);
        }

        public void DisableStartEngineButton()
        {
            buttonStartEngine.gameObject.SetActive(false);
        }
    }
}