using Helikopter.Managers;
using UnityEngine;

namespace Helikopter.UserInterface
{
    public class MoveJoystickFetch : MonoBehaviour
    {
        public void Initialize()
        {
            InputManager.Instance.MoveJoystick = GetComponent<Joystick>();
        }
    }
}