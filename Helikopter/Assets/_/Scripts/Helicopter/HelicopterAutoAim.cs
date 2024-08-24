using UnityEngine;
using R3;
using EnemyNamespace;
using Enemies;
using System.Collections.Generic;
using Helikopter.UserInterface;

namespace Helikopter
{
    public class HelicopterAutoAim : MonoBehaviour
    {
        private const float SLOW_UPDATE_TIME = 0.5f;

        [SerializeField]
        private List<Enemy> enemies;

        [field: SerializeField]
        public SerializableReactiveProperty<Enemy> Target { get; private set; }

        [SerializeField]
        private float attackRange;

        private float sqrAttackRange;

        private float slowUpdateTimer;

        private void Awake()
        {
            Target = new SerializableReactiveProperty<Enemy>();
            sqrAttackRange = attackRange * attackRange;
            enemies = EnemiesManager.Instance.RuntimeVariables.ListEnemies;
            Target.Subscribe(UIManager.Instance.GetScreen<UIGameplay>().HUD.Crosshair.OnTargetChange);
        }

        private void Update()
        {
            SlowUpdate();
        }

        private void CheckEnemies()
        {
            if (Target.Value == null)
            {
                GetNearbyEnemies();
            }
            else
            {
                CheckCurrentEnemy();
            }
        }

        private void GetNearbyEnemies()
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (Vector3.SqrMagnitude(transform.position - enemies[i].Center()) < sqrAttackRange)
                {
                    if (!InAimRange(Camera.main.WorldToViewportPoint(enemies[i].Center())))
                    {
                        continue;
                    }

                    Target.Value = enemies[i];
                    break;
                }
            }
        }

        private void CheckCurrentEnemy()
        {
            if (Vector3.SqrMagnitude(transform.position - Target.Value.Center()) > sqrAttackRange)
            {
                Target.Value = null;
            }

            if (Target.Value && !InAimRange(Camera.main.WorldToViewportPoint(Target.Value.Center())))
            {
                Target.Value = null;
            }
        }

        private void SlowUpdate()
        {
            if (slowUpdateTimer > 0)
            {
                slowUpdateTimer -= Time.deltaTime;
            }

            CheckEnemies();
            slowUpdateTimer = SLOW_UPDATE_TIME;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Target.Value ? Color.red : Color.green;
            Gizmos.DrawWireSphere(transform.position, attackRange);

            if (Target.Value)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(transform.position, Target.Value.Center());
            }
        }

        private bool InAimRange(Vector3 viewPortPosition)
        {
            bool result = true;

            result &= viewPortPosition.x >= 0.25f && viewPortPosition.x <= 0.75f;
            result &= viewPortPosition.y >= 0.4f && viewPortPosition.y <= 0.9f;
            result &= viewPortPosition.z > 0f;

            return result;
        }
    }
}