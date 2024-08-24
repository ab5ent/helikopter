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

        [field: SerializeField]
        public HelicopterManager HelicopterManager { get; private set; }

        [SerializeField]
        private EnemiesManager enemiesManager;

        [SerializeField]
        private CameraController cameraController;

        [field: SerializeField]
        public GameState State;

        #region Properties

        public Helicopter CurrentHelicopter => HelicopterManager.CurrentHelicopter;

        public Map CurrentMap => mapManager.CurrentMap;

        #endregion

        public void LoadGame()
        {
            State = GameState.Loading;
            mapManager.LoadMap(mapManager.GetRandomMapIndex);
            HelicopterManager.LoadHelicopter(HelicopterManager.GetRandomHelicopterIndex);
            enemiesManager.ClearAllEnemies();
        }

        public void StartGame()
        {
            State = GameState.InGame;
            CurrentHelicopter.SetActive(true);
            CurrentHelicopter.SetPosition(CurrentMap.GetStartPoint());
            cameraController.SetTarget(HelicopterManager.CurrentHelicopter.transform);

            enemiesManager.StartSpawnEnemies();
        }

        public void Start()
        {
            State = GameState.Menu;
            UIManager.Instance.Show<UIMenu>();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                Win();
            }

            if (Input.GetKeyDown(KeyCode.F2))
            {
                Lose();
            }

            if (Input.GetKeyDown(KeyCode.F3))
            {
                Pause();
            }

            if (Input.GetKeyDown(KeyCode.F4))
            {
                Resume();
            }
        }

        #region ChangeState

        public void Win()
        {
            SetTimeScale(0);
            State = GameState.Win;
            UIManager.Instance.Show<UIWin>();
        }

        public void Lose()
        {
            SetTimeScale(0);
            State = GameState.Lose;
            UIManager.Instance.Show<UILose>();
        }

        public void Pause()
        {
            if (State == GameState.InGame)
            {
                SetTimeScale(0);
                State = GameState.Pause;
                UIManager.Instance.Show<UIPause>();
            }
        }

        public void Resume()
        {
            if (State == GameState.Pause)
            {
                SetTimeScale(1);
                State = GameState.InGame;
                UIManager.Instance.Show<UIGameplay>();
            }
        }

        #endregion

        #region Utility

        private void SetTimeScale(float value)
        {
            Time.timeScale = value;
        }

        #endregion
    }
}