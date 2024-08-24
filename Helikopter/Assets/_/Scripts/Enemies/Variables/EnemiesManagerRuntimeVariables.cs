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

        internal void Initialize()
        {
            ListEnemies = new List<Enemy>();
            ListBosses = new List<Boss>();
        }

        internal void AddEnemy(Enemy enemy)
        {
            ListEnemies.Add(enemy);
        }

        internal void AddBoss(Boss boss)
        {
            ListBosses.Add(boss);
        }

        internal void RemoveEnemy(Enemy enemy)
        {
            ListEnemies.Remove(enemy);
        }

        internal void RemoveBoss(Boss boss)
        {
            ListBosses.Remove(boss);
        }
    }
}