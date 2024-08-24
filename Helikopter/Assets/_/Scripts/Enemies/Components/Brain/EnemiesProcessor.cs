using EnemyNamespace;
using Helikopter;
using System;
using UnityEngine;

namespace Enemies
{
    [Serializable]
    public class EnemiesProcessor
    {
        [SerializeField]
        protected EnemiesManager manager;

        protected const float SLOW_UPDATE_INTERVAL = .5f;

        protected float lastTimeFromSlowUpdate;

        protected float lastTimeSpawnBoss;

        protected bool canUpdate;

        public EnemiesProcessor(EnemiesManager enemiesManager)
        {
            lastTimeSpawnBoss = Time.time - 60;
            manager = enemiesManager;
            lastTimeFromSlowUpdate = Time.time - 60;
        }

        public void SetCanUpdate(bool canUpdate)
        {
            this.canUpdate = canUpdate;
        }

        public void OnSlowUpdate()
        {
            if (!canUpdate)
            {
                return;
            }

            if (Time.time - lastTimeFromSlowUpdate > SLOW_UPDATE_INTERVAL)
            {
                CheckAndSpawnEnemy();
                CheckAndSpawnBoss();

                lastTimeFromSlowUpdate = Time.time;
            }
        }

        #region Logic

        protected bool CanSpawnEnemy()
        {
            if (manager.RuntimeVariables.CountEnemies >= manager.Configure.MaxEnemies)
            {
                return false;
            }

            if (!manager.LocationAssign.HasSpawnPoint())
            {
                return false;
            }

            return true;
        }

        protected void CheckAndSpawnEnemy()
        {
            if (!CanSpawnEnemy())
            {
                return;
            }

            Enemy enemy = manager.GetEnemy();

            if (!enemy.HasBrain())
            {
                var brain = new EnemyBrain(enemy);
                enemy.SetBrain(brain);
            }

            enemy.SetActive(true);
            manager.RuntimeVariables.AddEnemy(enemy);
        }

        protected bool CanSpawnBoss()
        {
            if (manager.RuntimeVariables.CountBosses >= manager.Configure.MaxBosses)
            {
                return false;
            }

            if (!manager.LocationAssign.HasSpawnPoint())
            {
                return false;
            }

            if (Time.time - lastTimeSpawnBoss < manager.Configure.DelaySpawnBossTime)
            {
                return false;
            }

            return true;
        }

        protected void CheckAndSpawnBoss()
        {
            if (!CanSpawnBoss())
            {
                return;
            }

            Boss boss = manager.GetBoss();

            if (!boss.HasBrain())
            {
                var brain = new EnemyBrain(boss);
                boss.SetBrain(brain);
            }

            boss.SetActive(true);
            manager.RuntimeVariables.AddBoss(boss);

            lastTimeSpawnBoss = Time.time;
        }

        #endregion
    }
}