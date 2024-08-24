using UnityEngine;

namespace Helikopter.UserInterface
{
    public class UIGameplayHUD : MonoBehaviour
    {
        [field: SerializeField]
        public UICrosshair Crosshair { get; private set; }

        public void Initialize()
        {
            Crosshair.Initialize();
        }
    }
}