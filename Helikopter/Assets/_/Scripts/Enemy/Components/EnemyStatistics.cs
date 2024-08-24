using System;
using UnityEngine;

namespace EnemyNamespace
{
    [Serializable]
    public class EnemyStatistics
    {
        [field: SerializeField]
        public bool IsDead { get; private set; }

        [field: SerializeField]
        public float MaxHealthPoint { get; private set; }

        [field: SerializeField]
        public float CurrentHealthPoint { get; private set; }

        [field: SerializeField]
        public float Damage { get; private set; }

        [field: SerializeField]
        public float AttackRange { get; private set; }

        public float SqrAttackRange { get; private set; }

        [field: SerializeField]
        public float DespawnDistance { get; private set; }

        public float SqrDespawnDistance { get; private set; }

        public void SetSqrDespawnDistance()
        {
            SqrDespawnDistance = DespawnDistance * DespawnDistance;
            SqrAttackRange = AttackRange * AttackRange;
        }

        public void Set(float maxHP, float damage)
        {
            MaxHealthPoint = maxHP;
            SetCurrentHealthPoint(MaxHealthPoint);
            Damage = damage;
            IsDead = false;
        }

        public void SetCurrentHealthPoint(float value)
        {
            CurrentHealthPoint = value;
        }

        public void ChangeCurrentHealthPoint(float value)
        {
            CurrentHealthPoint += value;
            CheckIsDead();
        }

        public void CheckIsDead()
        {
            IsDead = CurrentHealthPoint <= 0;
        }
    }
}