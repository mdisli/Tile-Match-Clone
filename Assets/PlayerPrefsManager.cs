using System;
using UnityEngine;
using UnityEngine.Events;

public static class PlayerPrefsManager
{
    private const string CoinPrefsKey = "CoinCount";
    private const string HealthPrefsKey = "HealthCount";
    private const string LevelPrefsKey = "Level";
    private const string LastDecreaseHealthTimePrefsKey = "LastDecreaseHealthTime";

    private const string PlayWithPayCountPrefsKey = "PlayWithPayCount";

    public static event UnityAction OnHealthAmountIncreased;

    #region Money

    public static int CoinAmount => PlayerPrefs.GetInt(CoinPrefsKey, 100);

    public static void AddMoney(int amount)
    {
        PlayerPrefs.SetInt(CoinPrefsKey, CoinAmount + amount);
    }

    public static bool CheckMoneyEnough(int amount)
    {
        if (CoinAmount >= amount)
        {
            AddMoney(-amount);
            return true;
        }
        else
        {
            return false;
        }
    }

    #endregion


    #region Health

    private static int _maxHealth = 5;
    public static int HealthAmount => PlayerPrefs.GetInt(HealthPrefsKey, 5);

    public static void AddHealth(int amount)
    {
        PlayerPrefs.SetInt(HealthPrefsKey, HealthAmount + amount);

        if (amount < 0)
            LastHealthDecreaseTime = DateTime.Now.ToString();
    }

    private static string LastHealthDecreaseTime
    {
        get => PlayerPrefs.GetString(LastDecreaseHealthTimePrefsKey, DateTime.Now.ToString());
        set => PlayerPrefs.SetString(LastDecreaseHealthTimePrefsKey, value);
    }

    public static void CheckForHealthIncrease()
    {
        var timeDif = DateTime.Now - DateTime.Parse(LastHealthDecreaseTime);
        if (!(timeDif.TotalMinutes > 10)) return;
        if (HealthAmount >= _maxHealth) return;

        AddHealth((int)timeDif.TotalMinutes / 10);
        OnHealthAmountIncreased?.Invoke();
        LastHealthDecreaseTime = DateTime.Now.ToString();
    }

    #endregion


    #region Level

    public static int Level => PlayerPrefs.GetInt(LevelPrefsKey, 1);

    public static void IncreaseLevel()
    {
        PlayerPrefs.SetInt(LevelPrefsKey, Level + 1);
    }

    #endregion



    #region PlayWithPay

    public static int PlayWithPayCount => PlayerPrefs.GetInt(PlayWithPayCountPrefsKey, 1);

    public static int PlayWithPayMoneyAmount => 100 * PlayWithPayCount;

    public static void IncreasePlayWithPayCount()
    {
        PlayerPrefs.SetInt(PlayWithPayCountPrefsKey, PlayWithPayCount + 1);

    }
    #endregion
}
