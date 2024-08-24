using UnityEngine;
using UnityEngine.AI;

namespace EnemyNamespace
{
    public class EnemyReferences : MonoBehaviour
    {
        private Enemy owner;

        public void SetOwner(Enemy enemy)
        {
            owner = enemy;
            EventCallbackListener.SetEnemy(owner);
        }

        [field: SerializeField]
        public Transform Center { get; private set; }

        [field: SerializeField]
        public Collider EnemyCollider { get; private set; }

        [field: SerializeField]
        public NavMeshAgent NMAgent { get; protected set; }

        [field: SerializeField]
        public Animator EnemyAnimator { get; protected set; }

        [field: SerializeField]
        public Rigidbody EnemyRigidbody { get; protected set; }

        [field: SerializeField]
        public EnemyEventCallbackListener EventCallbackListener { get; private set; }
    }
}