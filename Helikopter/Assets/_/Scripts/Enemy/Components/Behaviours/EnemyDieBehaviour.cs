using UnityEngine;

namespace EnemyNamespace
{
    public class EnemyDieBehaviour : EnemyBehaviour
    {
        private const float disolveTime = 3.5f;

        private float dissolveTimer;

        public EnemyDieBehaviour(Enemy newEnemy) : base(newEnemy)
        {
        }

        public override void OnEnter()
        {
            Ref.EnemyRigidbody.velocity = Vector3.zero;
            Ref.EnemyCollider.enabled = false;
            Ref.NMAgent.enabled = false;

            Ref.EnemyAnimator.SetTrigger("Dead");
            Ref.EnemyAnimator.SetBool("IsDead", true);

            dissolveTimer = disolveTime;
        }

        public override void OnExit()
        {
            Ref.EnemyAnimator.SetBool("IsDead", false);
            enemy.Despawn();
        }

        public override void OnUpdate()
        {
            dissolveTimer -= Time.deltaTime;

            if (dissolveTimer <= 0)
            {
                SetBehaviour(enemy.GetEnemyBehaviour<EnemyEmptyBehaviour>());
            }
        }

        protected override void OnSlowUpdate()
        {
        }
    }
}