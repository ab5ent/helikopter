namespace EnemyNamespace
{
    public abstract class EnemyBehaviour
    {
        protected const float SlowUpdateTime = 0.5f;

        protected float slowUpdateTimer;

        protected Enemy enemy;

        protected EnemyReferences Ref => enemy.Ref;

        protected EnemyBehaviour(Enemy newEnemy)
        {
            enemy = newEnemy;
        }

        public abstract void OnEnter();

        public abstract void OnUpdate();

        protected abstract void OnSlowUpdate();

        public abstract void OnExit();

        protected void SetBehaviour(EnemyBehaviour behaviour)
        {
            enemy.Brain.SetBehaviour(behaviour);
        }
    }
}