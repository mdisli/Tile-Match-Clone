using System;
using UnityEngine;
using UnityEngine.Events;

namespace _Workspace.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        #region Unity Actions

        public static event UnityAction OnCoinCountChanged;
        public static event UnityAction OnHealthCountChanged;

        #endregion

        #region Variables

        private const string CoinPrefsKey = "CoinCount";
        private const string HealthPrefsKey = "HealthCount";
        private const string LevelPrefsKey = "Level";

        private int _coinAmount
        {
            get => PlayerPrefs.GetInt(CoinPrefsKey,0);
            set
            {
                PlayerPrefs.SetInt(CoinPrefsKey, value);
                OnCoinCountChanged?.Invoke();
            }
        }
        private int _healthAmount
        {
            get => PlayerPrefs.GetInt(HealthPrefsKey, 5);
            set
            {
                PlayerPrefs.SetInt(HealthPrefsKey,value);
                OnHealthCountChanged?.Invoke();
            }
        }

        private int _level
        {
            get => PlayerPrefs.GetInt(LevelPrefsKey, 1);
            set
            {
                PlayerPrefs.SetInt(LevelPrefsKey,value);
            }
        }


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
            SetGameStatus(GameStatus.GameEnd);
        }

        private void TileHolderOnOnGameCompleted()
        {
            SetGameStatus(GameStatus.GameEnd);
        }

        #endregion
        
        #region Set & Get Coin Count

        public int CoinAmount => _coinAmount;
        public void IncreaseCoinCount(int value)
        {
            _coinAmount += value;
        }
        private bool CheckMoneyEnough(int amount)
        {
            if (CoinAmount >= amount)
            {
                IncreaseCoinCount(-amount);
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region Set & Get Health Count

        public int HealthAmount => _healthAmount;

        #endregion

        #region Set & Get Level

        public int Level => _level;

        public void IncreaseLevel()
        {
            _level++;
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
        GameEnd
    }
}