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
        }

        private void OnDisable()
        {
            TileHolder.OnGameCompleted -= TileHolderOnOnGameCompleted;
            TileHolder.OnGameFailed -= TileHolderOnOnGameFailed;
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