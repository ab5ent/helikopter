using Common;
using UnityEngine;

namespace Helikopter.UserInterface
{
    public class UIManager : SingletonMonoBehaviour<UIManager>
    {
        [SerializeField]
        private UIScreen[] screens;

        private UIScreen currentScreen;

        public T GetScreen<T>() where T : UIScreen
        {
            for (int i = 0; i < screens.Length; i++)
            {
                if (screens[i] is T)
                {
                    return screens[i] as T;
                }
            }

            return null;
        }

        public void Show<T>() where T : UIScreen
        {
            T screen = GetScreen<T>();

            if (!screen)
            {
                Debug.LogError($"Can't find screen {typeof(T)}");
                return;
            }

            currentScreen?.Hide();
            currentScreen = screen;
            currentScreen.Show();
        }
    }
}