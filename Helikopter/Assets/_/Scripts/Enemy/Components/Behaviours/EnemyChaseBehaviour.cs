using Enemies;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyNamespace
{
    public class EnemyChaseBehaviour : EnemyBehaviour
    {
        private Transform targetTransform;

        public EnemyChaseBehaviour(Enemy newEnemy) : base(newEnemy)
        {
        }

        public override void OnEnter()
        {
            enemy.ResetNavMeshAgent();
            enemy.SetTarget(EnemiesManager.Instance.TargetAssign.GetTarget());
            targetTransform = enemy.Target.GetGameObject().transform;
            slowUpdateTimer = 0;
        }

        public override void OnExit()
        {
            enemy.Ref.NMAgent.enabled = false;
        }

        public override void OnUpdate()
        {
            OnSlowUpdate();
            enemy.transform.LookAt(Vector3.ProjectOnPlane(targetTransform.position, Vector3.up));
        }

        protected override void OnSlowUpdate()
        {
            if (slowUpdateTimer > 0)
            {
                slowUpdateTimer -= Time.deltaTime;
                return;
            }

            if (!Ref.NMAgent.isOnNavMesh || targetTransform == null)
            {
                SetBehaviour(enemy.GetEnemyBehaviour<EnemyIdleBehaviour>());
                return;
            }

            if (Vector3.Magnitude(enemy.transform.position - targetTransform.position) <= enemy.Statistics.SqrAttackRange)
            {
                SetBehaviour(enemy.GetEnemyBehaviour<EnemyAttackBehaviour>());
                return;
            }

            Ref.NMAgent.SetDestination(targetTransform.position);
            slowUpdateTimer = SlowUpdateTime;
        }
    }
}