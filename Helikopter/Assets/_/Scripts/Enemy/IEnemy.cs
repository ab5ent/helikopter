using Helikopter;
using UnityEngine;

namespace EnemyNamespace
{
    public interface IEnemy
    {
        public EnemyStatistics Statistics { get; }

        public EnemyId ID { get; }

        public bool HasBrain();

        public GameObject GetGameObject();

        public void SetSpawnPosition(Vector3 position);

        public void SetStats(float maxHP, float damage, float size);

        public void SetActive(bool value);

        public void SetBrain(EnemyBrain newBrain);

        public void SetTarget(IHitableEntity hitableEntity);

        public void Despawn();

        public Vector3 Center();
    }
}