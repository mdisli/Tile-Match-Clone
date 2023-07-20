using TMPro;
using UnityEngine;

namespace _Workspace.Scripts
{
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
            LevelGenerator.OnNewLevelLoaded += UpdateLevelText;
        }

        private void OnDisable()
        {
            TileHolder.OnGameFailed -= TileHolderOnOnGameFailed;
            TileHolder.OnGameCompleted -= TileHolderOnOnGameCompleted;
            LevelGenerator.OnNewLevelLoaded -= UpdateLevelText;
        }
        

        private void TileHolderOnOnGameFailed()
        {
            OpenFailUI();
        }
    
        private void TileHolderOnOnGameCompleted()
        {
            Invoke(nameof(OpenWinUI),1);
        }

        private void UpdateLevelText()
        {
            levelText.SetText($"Level {PlayerPrefsManager.Level}");
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
}
