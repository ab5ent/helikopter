using Helikopter;
using UnityEngine;

namespace Enemies
{
    public class EnemiesLocationAssign : MonoBehaviour
    {
        private EnemiesManager manager;

        public void SetManager(EnemiesManager enemiesManager)
        {
            manager = enemiesManager;
        }

        public Vector3 GetSpawnPosition()
        {
            Transform spawnPosition = GameManager.Instance.CurrentMap.GetRandomSpawnPosition();
            return spawnPosition.position;
        }

        public bool HasSpawnPoint() => true;
    }
}