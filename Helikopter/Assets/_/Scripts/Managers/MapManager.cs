using UnityEngine;

namespace Helikopter.Managers
{
    public class MapManager : MonoBehaviour
    {
        [SerializeField]
        private Map[] maps;

        public Map CurrentMap { get; private set; }

        public int GetRandomMapIndex => Random.Range(0, maps.Length);

        public void LoadMap(int index)
        {
            if (CurrentMap)
            {
                Destroy(CurrentMap);
            }

            CurrentMap = Instantiate<Map>(maps[index], transform);
            CurrentMap.SetActive(true);
        }
    }
}