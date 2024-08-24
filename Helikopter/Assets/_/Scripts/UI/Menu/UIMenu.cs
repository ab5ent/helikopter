using UnityEngine;
using UnityEngine.UI;

namespace Helikopter.UserInterface
{
    public class UIMenu : UIScreen
    {
        [SerializeField]
        private Button ButtonPlay;

        private void Awake()
        {
            ButtonPlay.onClick.AddListener(() => UIManager.Instance.Show<UILoading>());
        }
    }
}