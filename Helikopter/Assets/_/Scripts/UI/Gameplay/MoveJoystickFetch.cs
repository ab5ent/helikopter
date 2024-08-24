using Helikopter.Managers;
using UnityEngine;

namespace Helikopter.UI
{
    public class MoveJoystickFetch : MonoBehaviour
    {
        private void Start()
        {
            InputManager.Instance.MoveJoystick = GetComponent<Joystick>();
        }
    }
}