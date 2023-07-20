using System;
using UnityEngine;
using UnityEngine.Events;

namespace _Workspace.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        
        #region Variables

        private GameStatus _gameStatus;
        public GameStatus ActiveGameStatus => _gameStatus;

        [HideInInspector]public int gainedCoinOnThisLevel = 0;
        #endregion

        #region Unity Funcs

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                Application.targetFrameRate = 60;
            }
            else
            {
                Destroy(gameObject);
            }
            
            SetGameStatus(GameStatus.WaitingToStart);
        }

        private void OnEnable()
        {
            TileHolder.OnGameCompleted += TileHolderOnOnGameCompleted;
            TileHolder.OnGameFailed += TileHolderOnOnGameFailed;
            TileHolder.OnTilesPopped += TileHolderOnOnTilesPopped;
            LevelGenerator.OnNewLevelLoaded += LevelGeneratorOnOnNewLevelLoaded;
            FailUIController.OnPlayWithPayButtonClicked += FailUIControllerOnOnPlayWithPayButtonClicked;
        }

        private void OnDisable()
        {
            TileHolder.OnGameCompleted -= TileHolderOnOnGameCompleted;
            TileHolder.OnGameFailed -= TileHolderOnOnGameFailed;
            TileHolder.OnTilesPopped -= TileHolderOnOnTilesPopped;
            FailUIController.OnPlayWithPayButtonClicked -= FailUIControllerOnOnPlayWithPayButtonClicked;
            LevelGenerator.OnNewLevelLoaded -= LevelGeneratorOnOnNewLevelLoaded;
        }

        private void LevelGeneratorOnOnNewLevelLoaded()
        {
            // reset gained gold
            gainedCoinOnThisLevel = 0;
        }

        private void FailUIControllerOnOnPlayWithPayButtonClicked()
        {
            SetGameStatus(GameStatus.Playing);
        }

        private void TileHolderOnOnTilesPopped(TileHolder.OnTilesPoppedActionClass arg0)
        {
            PlayerPrefsManager.AddMoney(arg0.poppedTileCount*10);
            gainedCoinOnThisLevel += arg0.poppedTileCount*10;
        }

        private void TileHolderOnOnGameFailed()
        {
            SetGameStatus(GameStatus.LevelFailed);
        }

        private void TileHolderOnOnGameCompleted()
        {
            SetGameStatus(GameStatus.LevelCompleted);
            
            PlayerPrefsManager.IncreaseLevel();
        }

        #endregion

        #region Set Game Status

        public void SetGameStatus(GameStatus newStatus)
        {
            _gameStatus = newStatus;
        }

        #endregion
    }

    public enum GameStatus
    {
        WaitingToStart,
        Playing,
        LevelCompleted,
        LevelFailed,
    }
}