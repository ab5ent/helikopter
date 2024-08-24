using System.Collections.Generic;
using UnityEngine;

namespace Helikopter
{
    public class Map : MonoBehaviour
    {
        [SerializeField]
        private Transform spawnPositionsHolder;

        [SerializeField]
        private Transform housesHolder;

        [SerializeField]
        private Transform startPoint;

        private Queue<Transform> spawnPositionsQueue;

        private House[] houses;

        private void Awake()
        {
            spawnPositionsQueue = new Queue<Transform>();
            for (int i = 0; i < spawnPositionsHolder.childCount; i++)
            {
                spawnPositionsQueue.Enqueue(spawnPositionsHolder.GetChild(i));
            }

            houses = new House[housesHolder.childCount];
            for (int i = 0; i < housesHolder.childCount; i++)
            {
                houses[i] = housesHolder.GetChild(i).GetComponent<House>();
            }
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        public Transform GetRandomSpawnPosition()
        {
            Transform transform = spawnPositionsQueue.Dequeue();
            spawnPositionsQueue.Enqueue(transform);
            return transform;
        }

        public IHitableEntity GetHouse()
        {
            return houses[Random.Range(0, houses.Length)];
        }

        public Transform GetStartPoint()
        {
            return startPoint;
        }
    }
}