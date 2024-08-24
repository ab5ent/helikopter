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
        private Button startEngineBtn;

        [SerializeField]
        private Button fireBtn;

        public void Initialize()
        {
            moveJoystickFetch.Initialize();
            lookTouchPadFetch.Initialize();
            startEngineBtn.onClick.AddListener(GameManager.Instance.CurrentHelicopter.StartEngine);
            //fireBtn.onClick.AddListener(GameManager.Instance.CurrentHelicopter.Shoot);
        }
    }
}