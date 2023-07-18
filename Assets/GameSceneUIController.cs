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

    private void UpdateLevelText()
    {
        levelText.SetText($"Level {GameManager.instance.Level}");
    }

    [Button()]
    public void OpenWinUI()
    {
        winUIController.OpenWinUI();
    }

    [Button()]
    public void OpenFailUI()
    {
        failUIController.OpenFailUI();
    }
}
