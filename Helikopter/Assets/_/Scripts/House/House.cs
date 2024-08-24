using UnityEngine;

namespace Helikopter
{
    public class House : MonoBehaviour, IHitableEntity
    {
        private HouseStatistic Statistic;

        public void Die()
        {
            Debug.Log("Die");
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public void TakeDamage(float damage)
        {
            if (!Statistic.IsDead)
            {
                Statistic.ChangeCurrentHealthPoint(damage);
            }

            if (Statistic.IsDead)
            {
                Die();
            }
        }

        private void Awake()
        {
            Statistic = GetComponent<HouseStatistic>();
        }
    }
}