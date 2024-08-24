using UnityEngine;

namespace EnemyNamespace
{
    public class EnemyAttackBehaviour : EnemyBehaviour
    {
        private float delayAttackTime;

        public int AttackType { get; private set; }

        public EnemyAttackBehaviour(Enemy newEnemy) : base(newEnemy)
        {
        }

        public override void OnEnter()
        {
            Ref.EnemyAnimator.SetBool("IsInCombat", true);
            Ref.NMAgent.enabled = false;
            delayAttackTime = 0;
        }

        public override void OnUpdate()
        {
            OnSlowUpdate();

            bool isAttacking = Ref.EnemyAnimator.GetBool("IsAttacking");

            //enemy.transform.LookAt(enemy.transform);

            if (delayAttackTime > 0)
            {
                delayAttackTime -= Time.deltaTime;
                return;
            }
        }

        protected override void OnSlowUpdate()
        {
            //if (Vector3.Magnitude(enemy.transform.position - enemy.Target.transform.position) >= enemy.Statistics.SqrAttackRange)
            //{
            //    SetBehaviour(enemy.GetEnemyBehaviour<EnemyChaseBehaviour>());
            //}
        }

        public override void OnExit()
        {
            Ref.EnemyAnimator.SetBool("IsInCombat", false);
            Ref.EnemyAnimator.SetBool("IsAttacking", false);
            Ref.EnemyAnimator.SetInteger("AttackType", -1);
            AttackType = -1;
        }

        public void SetDelayAttack(float value)
        {
            delayAttackTime = value;
            AttackType = -1;
        }
    }
}