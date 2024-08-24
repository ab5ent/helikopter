using UnityEngine;

namespace EnemyNamespace
{
    public class EnemyBrain
    {
        protected const float SLOW_UPDATE_PACING = 1.5f;

        protected float lastTimeSlowUpdate;

        protected EnemyBehaviour enemyBehaviour;

        protected Enemy enemy;

        public EnemyBrain(Enemy newEnemy)
        {
            enemy = newEnemy;
        }

        public void OnEnter()
        {
            SetBehaviour(enemy.GetEnemyBehaviour<EnemyIdleBehaviour>());
        }

        public void OnUpdate()
        {
            SlowUpdate();
            enemyBehaviour.OnUpdate();
        }

        public void SlowUpdate()
        {
            if (Time.time - lastTimeSlowUpdate > SLOW_UPDATE_PACING)
            {
                enemy.CheckAndDespawn();
                lastTimeSlowUpdate = Time.time;
            }
        }

        public void SetBehaviour(EnemyBehaviour behaviour)
        {
            enemyBehaviour?.OnExit();
            enemyBehaviour = behaviour;
            enemyBehaviour.OnEnter();
        }
    }
}