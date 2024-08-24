using EnemyNamespace;
using UnityEngine;

namespace Enemies
{
    public class EnemiesSpawner : MonoBehaviour
    {
        private EnemiesManager manager;

        [SerializeField]
        private EnemiesSpawnerData data;

        public void SetManager(EnemiesManager enemiesManager)
        {
            manager = enemiesManager;
        }

        public Enemy SpawnEnemy(Vector3 position, Quaternion rotation)
        {
            return manager.Pool.Spawn(data.GetRandomEnemy(), position, rotation) as Enemy;
        }

        public Boss SpawnBoss(Vector3 position, Quaternion rotation)
        {
            return manager.Pool.Spawn(data.GetRandomBoss(),position, rotation) as Boss;
        }
    }
}