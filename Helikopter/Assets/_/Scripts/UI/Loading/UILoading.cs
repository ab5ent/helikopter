using UnityEngine;
using UnityEngine.UI;

namespace Helikopter.UserInterface
{
    public class UILoading : UIScreen
    {
        [SerializeField]
        private Slider slider;

        [SerializeField]
        private float loadTime;

        private float counter;

        public override void Show()
        {
            base.Show();
            counter = 0;
            GameManager.Instance.LoadGame();
        }

        private void Update()
        {
            if (counter < loadTime)
            {
                counter += Time.deltaTime;
                slider.value = (counter / loadTime);

                if (counter >= loadTime)
                {
                    GameManager.Instance.StartGame();
                    UIManager.Instance.Show<UIGameplay>();
                }
            }
        }
    }
}