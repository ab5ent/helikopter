using UnityEngine;

namespace EnemyNamespace
{
    public class EnemyIdleBehaviour : EnemyBehaviour
    {
        public EnemyIdleBehaviour(Enemy newEnemy) : base(newEnemy)
        {
        }

        public override void OnEnter()
        {

        }

        public override void OnUpdate()
        {
            OnSlowUpdate();
        }

        protected override void OnSlowUpdate()
        {
            if (slowUpdateTimer > 0)
            {
                slowUpdateTimer -= Time.deltaTime;
            }

            slowUpdateTimer = SlowUpdateTime;
        }

        public override void OnExit()
        {

        }
    }
}