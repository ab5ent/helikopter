using EnemyNamespace;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    [Serializable]
    public class EnemiesManagerRuntimeVariables
    {
        [field: SerializeField]
        public List<Enemy> ListEnemies { get; private set; }

        [field: SerializeField]
        public List<Boss> ListBosses { get; private set; }

        public int CountEnemies => ListEnemies.Count;

        public int CountBosses => ListBosses.Count;

        public int CountEntities => ListBosses.Count + ListEnemies.Count;

        public void Initialize()
        {
            ListEnemies = new List<Enemy>();
            ListBosses = new List<Boss>();
        }

        public void AddEnemy(Enemy enemy)
        {
            ListEnemies.Add(enemy);
        }

        public void AddBoss(Boss boss)
        {
            ListBosses.Add(boss);
        }

        public void RemoveEnemy(Enemy enemy)
        {
            ListEnemies.Remove(enemy);
        }

        public void RemoveBoss(Boss boss)
        {
            ListBosses.Remove(boss);
        }
    }
}