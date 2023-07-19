using System;
using _Workspace.Scripts;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class GameSceneUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private WinUIController winUIController;
    [SerializeField] private FailUIController failUIController;

    private void Start()
    {
        UpdateLevelText();
    }

    private void OnEnable()
    {
        TileHolder.OnGameFailed += TileHolderOnOnGameFailed;
        TileHolder.OnGameCompleted += TileHolderOnOnGameCompleted;
    }

    private void OnDisable()
    {
        TileHolder.OnGameFailed -= TileHolderOnOnGameFailed;
        TileHolder.OnGameCompleted -= TileHolderOnOnGameCompleted;
    }

    private void TileHolderOnOnGameFailed()
    {
        OpenFailUI();
    }
    
    private void TileHolderOnOnGameCompleted()
    {
        OpenWinUI();
    }

    private void UpdateLevelText()
    {
        levelText.SetText($"Level {GameManager.instance.Level}");
    }
    
    public void OpenWinUI()
    {
        winUIController.OpenWinUI();
    }
    
    public void OpenFailUI()
    {
        failUIController.OpenFailUI();
    }
}
