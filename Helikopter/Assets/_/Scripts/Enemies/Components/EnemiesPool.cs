using EnemyNamespace;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemiesPool : MonoBehaviour
    {
        private EnemiesManager manager;

        private Dictionary<EnemyId, List<IEnemy>> inactivatedEnemies;

        public void SetManager(EnemiesManager enemiesManager)
        {
            manager = enemiesManager;
            inactivatedEnemies = new Dictionary<EnemyId, List<IEnemy>>();
        }

        public IEnemy Spawn(IEnemy enemy, Vector3 position, Quaternion rotation)
        {
            if (!inactivatedEnemies.ContainsKey(enemy.ID))
            {
                inactivatedEnemies.Add(enemy.ID, new List<IEnemy>());
            }

            List<IEnemy> usableEnemies = inactivatedEnemies.GetValueOrDefault(enemy.ID);

            if (usableEnemies.Count > 0)
            {
                IEnemy resultEnemy = usableEnemies[0];
                usableEnemies.Remove(resultEnemy);
                return resultEnemy;
            }
            else
            {
                GameObject resultEnemyObject = Instantiate(enemy.GetGameObject(), position, rotation, transform);
                IEnemy resultEnemy = resultEnemyObject.GetComponent<IEnemy>();
                return resultEnemy;
            }
        }

        public void Despawn(IEnemy enemy)
        {
            if (!inactivatedEnemies.ContainsKey(enemy.ID))
            {
                return;
            }

            enemy.SetActive(false);

            List<IEnemy> usableEnemies = inactivatedEnemies.GetValueOrDefault(enemy.ID);
            usableEnemies.Add(enemy);
        }
    }
}