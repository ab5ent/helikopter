using System;
using UnityEngine;

namespace EnemyNamespace
{
    public class EnemySpawnVirtualEffect : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem virtualEffect;

        [SerializeField]
        private GameObject model;

        [field: SerializeField]
        public float Duration { get; private set; } = 1;

        [field: SerializeField]
        public bool IsComplete { get; private set; }

        public void Setup()
        {
            virtualEffect.gameObject.SetActive(true);
            model.SetActive(false);
            IsComplete = false;
        }

        public void Play()
        {
            virtualEffect.Stop();
            model.SetActive(true);
            virtualEffect.Play();
            Invoke(nameof(Complete), Duration);
        }

        public void Stop()
        {
            virtualEffect.Stop();
            virtualEffect.gameObject.SetActive(false);
        }

        private void Complete()
        {
            IsComplete = true;
        }
    }
}