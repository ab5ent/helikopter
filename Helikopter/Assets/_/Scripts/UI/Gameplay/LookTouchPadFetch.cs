using Helikopter.Managers;
using UnityEngine;

namespace Helikopter.UI
{
    public class LookTouchPadFetch : MonoBehaviour
    {
        private void Start()
        {
            InputManager.Instance.LookTouchPad = GetComponent<TouchPad>();
        }
    }
}