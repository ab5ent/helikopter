using Common;
using Helikopter.UI;
using UnityEngine;

namespace Helikopter.Managers
{
    public class InputManager : SingletonMonoBehaviour<InputManager>
    {
        public Joystick MoveJoystick { get; set; }

        public TouchPad LookTouchPad { get; set; }

        public Vector2 MoveInput => MoveJoystick.Direction;

        public Vector2 LookInput => LookTouchPad.Direction;
    }
}