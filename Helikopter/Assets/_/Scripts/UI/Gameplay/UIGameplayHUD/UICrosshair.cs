using EnemyNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace Helikopter.UserInterface
{
    public class UICrosshair : MonoBehaviour
    {
        [SerializeField]
        private RectTransform crosshairOriginPosition;

        [SerializeField]
        private Image crosshair;

        [SerializeField]
        private CanvasGroup crosshairCanvasGroup;

        [SerializeField]
        private Color onTargetColor;

        [SerializeField]
        private Color defaultColor;

        private Enemy target;

        public void Initialize()
        {
            crosshairCanvasGroup.alpha = 0;
            crosshair.rectTransform.position = crosshairOriginPosition.position;
        }

        public void OnTargetChange(Enemy newTarget)
        {
            target = newTarget;

            if (target)
            {
                crosshairCanvasGroup.alpha = 1;
                crosshair.color = onTargetColor;
            }
            else
            {
                crosshairCanvasGroup.alpha = 0;
                crosshair.color = defaultColor;
                crosshair.rectTransform.position = crosshairOriginPosition.position;
            }
        }

        private void Update()
        {
            if (target)
            {
                Vector3 inScreenTargetPosition = Camera.main.WorldToScreenPoint(target.Center());

                if (inScreenTargetPosition.z < 0)
                {
                    crosshairCanvasGroup.alpha = 0;
                }
                else
                {
                    crosshairCanvasGroup.alpha = 1;
                }

                transform.position = inScreenTargetPosition;
            }
        }
    }
}