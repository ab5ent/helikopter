using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyNamespace
{
    public class EnemyHittedBehaviour : EnemyBehaviour
    {
        public EnemyHittedBehaviour(Enemy newEnemy) : base(newEnemy)
        {
        }

        public override void OnEnter()
        {
            Ref.EnemyAnimator.SetTrigger("Hitted");
            Ref.EnemyAnimator.SetFloat("MovementSpeed", 0);
        }

        public override void OnExit()
        {
        }

        public override void OnUpdate()
        {
        }

        protected override void OnSlowUpdate()
        {
        }
    }
}