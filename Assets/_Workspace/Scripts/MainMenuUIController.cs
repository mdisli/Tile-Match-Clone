using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Workspace.Scripts
{
    public class MainMenuUIController : MonoBehaviour
    {
        [SerializeField] private Button levelStartButton;
        [SerializeField] private TextMeshProUGUI levelTxt;
    
        [SerializeField] private MainMenuInfoBar coinInfoBar;
        [SerializeField] private MainMenuInfoBar healthInfoBar;
    
        private const string CoinPrefsKey = "CoinCount";
        private const string HealthPrefsKey = "HealthCount";
        private const string LevelPrefsKey = "Level";

        private int CoinCount => PlayerPrefs.GetInt(CoinPrefsKey, 0);
        private int HealthAmount => PlayerPrefs.GetInt(HealthPrefsKey, 0);
        private int Level => PlayerPrefs.GetInt(LevelPrefsKey, 0);
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
            coinInfoBar.UpdateAmountTxt(CoinCount);
            healthInfoBar.UpdateAmountTxt(HealthAmount);
            levelTxt.SetText($"Level {Level}");
        }
    }
}
