using System;
using _Workspace.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] private Button levelStartButton;
    
    [SerializeField] private MainMenuInfoBar coinInfoBar;
    [SerializeField] private MainMenuInfoBar healthInfoBar;

    private void Start()
    {
        levelStartButton.onClick.AddListener(()=> SceneTransitionController.instance.LoadSceneWithTransitionEffect(1,0));
        
        UpdateInfoBars();
        
    }

    private void OnDestroy()
    {
        levelStartButton.onClick.RemoveListener(()=> SceneTransitionController.instance.LoadSceneWithTransitionEffect(1,0));
    }


    private void UpdateInfoBars()
    {
        coinInfoBar.UpdateAmountTxt(GameManager.instance.CoinAmount);
        healthInfoBar.UpdateAmountTxt(GameManager.instance.HealthAmount);
    }
}
