using EnemyNamespace;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(fileName = "EnemiesSpawnerData", menuName = "Enemies/EnemiesSpawnerData")]
    public class EnemiesSpawnerData : ScriptableObject
    {
        [field: SerializeField]
        public List<Enemy> EnemyPrefabs { get; private set; }

        [field: SerializeField]
        public List<Boss> BossPrefabs { get; private set; }

        public Enemy GetRandomEnemy()
        {
            int randomIndex = 0;/*Random.Range(0, EnemyPrefabs.Count);*/
            return EnemyPrefabs[randomIndex];
        }

        public Enemy GetRandomBoss()
        {
            int randomIndex = 0;/*Random.Range(0, BossPrefabs.Count);*/
            return BossPrefabs[randomIndex];
        }
    }
}