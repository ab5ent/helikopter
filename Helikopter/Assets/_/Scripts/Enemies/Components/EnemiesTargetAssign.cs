using Enemies;
using Helikopter;
using UnityEngine;

namespace EnemyNamespace
{
    public class EnemiesTargetAssign : MonoBehaviour
    {
        private EnemiesManager manager;

        public void SetManager(EnemiesManager enemiesManager)
        {
            manager = enemiesManager;
        }

        public IHitableEntity GetTarget()
        {
            return GameManager.Instance.CurrentMap.GetHouse();
        }
    }
}