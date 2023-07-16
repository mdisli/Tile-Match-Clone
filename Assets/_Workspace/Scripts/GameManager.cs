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
        

        #endregion

        #region Unity Funcs

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #endregion
        
        #region Set & Get Coin Count

        public int CoinAmount => _coinAmount;
        public void IncreaseCoinCount(int value)
        {
            _coinAmount += value;
        }

        #endregion

        #region Set & Get Health Count

        public int HealthAmount => _healthAmount;

        #endregion
        
    }
}