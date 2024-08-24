using UnityEngine;

namespace Helikopter.UserInterface
{
    public class UIGameplay : UIScreen
    {
        [field: SerializeField]
        public UIGameplayControl Control { get; private set; }

        [field: SerializeField]
        public UIGameplayHUD HUD { get; private set; }

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            Control.Initialize();
            HUD.Initialize();
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}