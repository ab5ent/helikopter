using UnityEngine;

namespace Helikopter.Managers
{
    public class HelicopterManager : MonoBehaviour
    {
        [SerializeField]
        private Helicopter[] helicopters;

        public Helicopter CurrentHelicopter { get; private set; }

        public int GetRandomHelicopterIndex => Random.Range(0, helicopters.Length);

        public void LoadHelicopter(int index)
        {
            if (CurrentHelicopter)
            {
                Destroy(CurrentHelicopter);
            }

            CurrentHelicopter = Instantiate(helicopters[index], transform);
            CurrentHelicopter.Initialize();
        }
    }
}