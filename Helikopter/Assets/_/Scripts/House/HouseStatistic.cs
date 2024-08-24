using UnityEngine;

namespace Helikopter
{
    public class HouseStatistic : MonoBehaviour
    {
        [field: SerializeField]
        public bool IsDead { get; private set; }

        [field: SerializeField]
        public float MaxHealthPoint { get; private set; }

        [field: SerializeField]
        public float CurrentHealthPoint { get; private set; }

        public void Set(float maxHP, float damage)
        {
            MaxHealthPoint = maxHP;
            SetCurrentHealthPoint(MaxHealthPoint);
            IsDead = false;
        }

        public void SetCurrentHealthPoint(float value)
        {
            CurrentHealthPoint = value;
        }

        public void ChangeCurrentHealthPoint(float value)
        {
            CurrentHealthPoint += value;
            CheckIsDead();
        }

        public void CheckIsDead()
        {
            IsDead = CurrentHealthPoint <= 0;
        }
    }
}