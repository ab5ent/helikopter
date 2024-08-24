namespace EnemyNamespace
{
    public class EnemyOnSpawnBehaviour : EnemyBehaviour
    {
        public EnemyOnSpawnBehaviour(Enemy newEnemy) : base(newEnemy)
        {
        }

        public override void OnEnter()
        {
            OnComplete();
        }

        public override void OnUpdate()
        {
        }

        protected override void OnSlowUpdate()
        {
        }

        public override void OnExit()
        {
        }

        private void OnComplete()
        {
            SetBehaviour(enemy.GetEnemyBehaviour<EnemyChaseBehaviour>());
        }
    }
}