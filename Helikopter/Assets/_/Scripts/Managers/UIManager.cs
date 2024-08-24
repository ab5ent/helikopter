using Common;
using System;
using UnityEngine;

namespace Helikopter.UserInterface
{
    public class UIManager : SingletonMonoBehaviour<UIManager>
    {
        [field: SerializeField]
        public UIGameplay Gameplay { get; private set; }
    }
}