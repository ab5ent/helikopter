using Enemies;
using Helikopter;
using UnityEngine;

namespace EnemyNamespace
{
    public class Enemy : MonoBehaviour, IEnemy
    {
        public static GameObject FocusObject { get; private set; }

        [field: SerializeField]
        public EnemyReferences Ref { get; private set; }

        [field: SerializeField]
        public EnemyStatistics Statistics { get; private set; }

        [field: SerializeField]
        public EnemyBrain Brain { get; private set; }

        private EnemyBehaviour[] behaviours;

        public IHitableEntity Target { get; private set; }

        #region Properties

        [field: SerializeField]
        public EnemyId ID { get; private set; }

        public GameObject GetGameObject() => gameObject;

        public bool HasBrain() => Brain != null;

        #endregion

        #region Setter

        public void SetSpawnPosition(Vector3 position)
        {
            Ref.NMAgent.Warp(position);
        }

        public void SetStats(float maxHP, float damage, float size)
        {
            Statistics.Set(maxHP, damage);
            transform.localScale = Vector3.one * size;
        }

        public void Initialize()
        {
            Ref.EnemyCollider.enabled = true;
        }

        public void SetActive(bool value)
        {
            GetGameObject().SetActive(value);
            if (value && Brain != null)
            {
                Brain.OnEnter();
            }
        }

        public void ResetNavMeshAgent()
        {
            Ref.NMAgent.enabled = false;
            Ref.NMAgent.enabled = true;
        }

        public void SetBrain(EnemyBrain newBrain)
        {
            Brain = newBrain;
            Brain.SetBehaviour(GetEnemyBehaviour<EnemyEmptyBehaviour>());
        }

        public static void SetFocusObject(GameObject focusObject)
        {
            FocusObject = focusObject;
        }

        public void SetTarget(IHitableEntity hitableEntity)
        {
            Target = hitableEntity;
        }

        #endregion

        protected void Awake()
        {
            Statistics.SetSqrDespawnDistance();

            behaviours = new EnemyBehaviour[7]
            {
                new EnemyEmptyBehaviour(this),
                new EnemyOnSpawnBehaviour(this),
                new EnemyDieBehaviour(this),
                new EnemyHittedBehaviour(this),
                new EnemyChaseBehaviour(this),
                new EnemyIdleBehaviour(this),
                new EnemyAttackBehaviour(this)
            };

            Ref.SetOwner(this);
        }

        private void Update()
        {
            Brain.OnUpdate();
        }

        public void OnHit(float damage, Vector3 hitPos, Vector3 hitVector)
        {
            if (!Statistics.IsDead)
            {
                Statistics.ChangeCurrentHealthPoint(-damage);
                Brain.SetBehaviour(GetEnemyBehaviour<EnemyHittedBehaviour>());
            }

            if (Statistics.IsDead)
            {
                Die();
                transform.rotation = Quaternion.LookRotation(-hitVector);
            }
        }

        public void Die()
        {
            Brain.SetBehaviour(GetEnemyBehaviour<EnemyDieBehaviour>());
        }

        public float SqrMagnitudeFromFocusTarget()
        {
            if (FocusObject == null)
            {
                return -1;
            }

            return Vector3.SqrMagnitude(transform.position - FocusObject.transform.position);
        }

        public virtual void Despawn()
        {
            EnemiesManager.Instance.RuntimeVariables.RemoveEnemy(this);
            EnemiesManager.Instance.Pool.Despawn(this);
        }

        public void CheckAndDespawn()
        {
            float sqrMagnitude = SqrMagnitudeFromFocusTarget();

            if (sqrMagnitude < -1 || sqrMagnitude > Statistics.SqrDespawnDistance)
            {
                Despawn();
            }
        }

        public void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, Statistics.DespawnDistance);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, Statistics.AttackRange);
        }

        #region Utility

        public T GetEnemyBehaviour<T>() where T : EnemyBehaviour
        {
            for (int i = 0; i < behaviours.Length; i++)
            {
                if (behaviours[i] is T)
                {
                    return behaviours[i] as T;
                }
            }

            return null;
        }

        public Vector3 Center()
        {
            return Ref.Center.position;
        }

        #endregion
    }
}