using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyNamespace
{
    public class EnemyEventCallbackListener : MonoBehaviour
    {
        private Enemy enemy;

        public void SetEnemy(Enemy newEnemy)
        {
            enemy = newEnemy;
        }

        public void ChaseTarget()
        {
            enemy.Brain.SetBehaviour(enemy.GetEnemyBehaviour<EnemyChaseBehaviour>());
        }

        public void OnAttackComplete()
        {
            enemy.GetEnemyBehaviour<EnemyAttackBehaviour>().SetDelayAttack(3f);
            enemy.Ref.EnemyAnimator.SetBool("IsAttacking", false);
            enemy.Ref.EnemyAnimator.SetInteger("AttackType", -1);
        }

        public void DealDamage()
        {
        }
    }
}