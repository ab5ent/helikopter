using Helikopter.Managers;
using UnityEngine;

namespace Helikopter.UserInterface
{
    public class LookTouchPadFetch : MonoBehaviour
    {
        public void Initialize()
        {
            InputManager.Instance.LookTouchPad = GetComponent<TouchPad>();
        }
    }
}