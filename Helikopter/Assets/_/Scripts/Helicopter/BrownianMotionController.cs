using DG.Tweening;
using Klak.Motion;
using UnityEngine;

namespace Helikopter
{
    public class BrownianMotionController : MonoBehaviour
    {
        private BrownianMotion brownianMotion;

        [SerializeField]
        private float startingMotionDuration = 3;

        [SerializeField]
        private float stopingMotionDuration = 1;

        [SerializeField]
        private float frequencyValue = 0.2f;

        private void Awake()
        {
            brownianMotion = GetComponent<BrownianMotion>();
            brownianMotion.positionFrequency = 0;
            brownianMotion.rotationFrequency = 0;
        }

        public void StartMotion()
        {
            DOTween.To(SetFrequency, 0f, frequencyValue, startingMotionDuration);
        }

        public void StopMotion()
        {
            DOTween.To(SetFrequency, brownianMotion.positionFrequency, 0, stopingMotionDuration);
        }

        public void SetFrequency(float value)
        {
            brownianMotion.positionFrequency = value;
            brownianMotion.rotationFrequency = value;
        }
    }
}