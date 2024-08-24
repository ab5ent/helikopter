using UnityEngine;

namespace Helikopter
{
    public interface IHitableEntity
    {
        public GameObject GetGameObject();

        public void TakeDamage(float damage);

        public void Die();
    }
}