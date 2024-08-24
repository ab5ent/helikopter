using Common;
using EnemyNamespace;
using Helikopter;
using System;
using UnityEngine;

namespace Enemies
{
    public class EnemiesManager : SingletonMonoBehaviour<EnemiesManager>
    {
        [field: Header("References"), SerializeField]
        public EnemiesSpawner Spawner { get; private set; }

        [field: SerializeField]
        public EnemiesPool Pool { get; private set; }

        [field: SerializeField]
        public EnemiesLocationAssign LocationAssign { get; private set; }

        [field: SerializeField]
        public EnemiesStatisticsAssign StatisticsAssign { get; private set; }

        [field: SerializeField]
        public EnemiesTargetAssign TargetAssign { get; private set; }

        [field: Header("Variables"), SerializeField]
        public EnemiesManagerConfigure Configure { get; private set; }

        [field: SerializeField]
        public EnemiesManagerRuntimeVariables RuntimeVariables { get; private set; }

        private EnemiesProcessor currentProcessor;

        protected override void Awake()
        {
            base.Awake();

            Spawner.SetManager(this);
            Pool.SetManager(this);
            LocationAssign.SetManager(this);
            StatisticsAssign.SetManager(this);
            TargetAssign.SetManager(this);

            RuntimeVariables.Initialize();
            currentProcessor = new EnemiesProcessor(this);
        }

        public void ClearAllEnemies()
        {
            ClearEnemies();
            ClearBosses();
        }

        public void StartSpawnEnemies()
        {
            Enemy.SetFocusObject(GameManager.Instance.CurrentHelicopter.gameObject);
            currentProcessor.SetCanUpdate(true);
        }

        private void Update()
        {
            currentProcessor?.OnSlowUpdate();
        }

        #region Methods

        public Enemy GetEnemy()
        {
            Vector3 position = LocationAssign.GetSpawnPosition();

            Enemy enemy = Spawner.SpawnEnemy(position, Quaternion.identity);
            enemy.SetSpawnPosition(position);
            enemy.SetActive(false);
            enemy.Initialize();

            (float, float, float) stats = StatisticsAssign.GetStats();
            enemy.SetStats(stats.Item1, stats.Item2, stats.Item3);

            return enemy;
        }

        public Boss GetBoss()
        {
            Vector3 position = LocationAssign.GetSpawnPosition();
            Boss boss = Spawner.SpawnBoss(position, Quaternion.identity);

            boss.SetActive(false);
            boss.Initialize();

            (float, float, float) stats = StatisticsAssign.GetStats();
            boss.SetStats(stats.Item1, stats.Item2, stats.Item3);

            return boss;
        }

        public void ClearEnemies()
        {
            while (RuntimeVariables.ListEnemies.Count > 0)
            {
                Enemy enemy = RuntimeVariables.ListEnemies[0];
                RuntimeVariables.RemoveEnemy(enemy);
                Pool.Despawn(enemy);
            }
        }

        public void ClearBosses()
        {
            while (RuntimeVariables.ListBosses.Count > 0)
            {
                Boss boss = RuntimeVariables.ListBosses[0];
                RuntimeVariables.RemoveBoss(boss);
                Pool.Despawn(boss);
            }
        }

        #endregion
    }
}