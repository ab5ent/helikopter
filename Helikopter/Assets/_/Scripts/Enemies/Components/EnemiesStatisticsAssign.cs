using UnityEngine;

namespace Enemies
{
    public class EnemiesStatisticsAssign : MonoBehaviour
    {
        private EnemiesManager manager;

        public void SetManager(EnemiesManager enemiesManager)
        {
            manager = enemiesManager;
        }

        public (float, float, float) GetStats()
        {
            float health = 1000;
            float damamge = 1000;
            float size = Random.Range(4, 10);

            return (health, damamge, size);
        }
    }
}