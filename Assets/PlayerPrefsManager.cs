using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPrefsManager
{
    private const string CoinPrefsKey = "CoinCount";
    private const string HealthPrefsKey = "HealthCount";
    private const string LevelPrefsKey = "Level";
    private const string LastDecreaseHealthTimePrefsKey = "LastDecreaseHealthTime";


    #region Money

    public static int CoinAmount => PlayerPrefs.GetInt(CoinPrefsKey,0);
    public static void AddMoney(int amount)
    {
        PlayerPrefs.SetInt(CoinPrefsKey, CoinAmount+amount);
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
        PlayerPrefs.SetInt(HealthPrefsKey, HealthAmount+amount);
        
        if(amount < 0)
            LastHealthDecreaseTime = Time.time.ToString();
    }

    private static string LastHealthDecreaseTime
    {
        get=>PlayerPrefs.GetString(LastDecreaseHealthTimePrefsKey, "0");
        set=>PlayerPrefs.SetString(LastDecreaseHealthTimePrefsKey, value);
    }

    public static void CheckForHealthIncrease()
    {
        float timeDif = Time.time - float.Parse(LastHealthDecreaseTime); 
        if (!( timeDif > 10)) return;
        if(HealthAmount >= _maxHealth) return;
        
        AddHealth((int)timeDif/10);
        LastHealthDecreaseTime = Time.time.ToString();
    }

    #endregion


    #region Level

    public static int Level => PlayerPrefs.GetInt(LevelPrefsKey, 1);
    
    public static void IncreaseLevel()
    {
        PlayerPrefs.SetInt(LevelPrefsKey,Level+1);
    }

    #endregion
}
