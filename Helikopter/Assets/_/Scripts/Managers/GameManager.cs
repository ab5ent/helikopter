using Common;
using Enemies;
using Helikopter.Managers;
using Helikopter.UserInterface;
using UnityEngine;

namespace Helikopter
{
    public class GameManager : SingletonMonoBehaviour<GameManager>
    {
        [SerializeField]
        private MapManager mapManager;

        [SerializeField]
        private HelicopterManager helicopterManager;

        [SerializeField]
        private EnemiesManager enemiesManager;

        [SerializeField]
        private CameraController cameraController;

        #region Properties

        public Helicopter CurrentHelicopter => helicopterManager.CurrentHelicopter;

        public Map CurrentMap => mapManager.CurrentMap;

        #endregion

        public void StartGame()
        {
            mapManager.LoadMap(mapManager.GetRandomMapIndex);
            helicopterManager.LoadHelicopter(helicopterManager.GetRandomHelicopterIndex);

            CurrentHelicopter.SetPosition(CurrentMap.GetStartPoint());

            cameraController.SetTarget(helicopterManager.CurrentHelicopter.transform);
            enemiesManager.LoadEnemies();

            UIManager.Instance.Gameplay.SetActive(true);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                StartGame();
            }
        }
    }
}