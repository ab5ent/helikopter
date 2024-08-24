using System;
using UnityEngine;

namespace Enemies
{
    [Serializable]
    public class EnemiesManagerConfigure
    {
        [field: SerializeField]
        public int MaxEnemies { get; private set; }

        [field: SerializeField]
        public int MaxBosses { get; private set; }

        [field: SerializeField]
        public float DelaySpawnBossTime { get; private set; }
    }
}