using DG.Tweening;
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

        [SerializeField] private CanvasGroup youDontHaveHealthCanvasGroup;
        private void Start()
        {
            PlayerPrefsManager.OnHealthAmountIncreased += PlayerPrefsManagerOnOnHealthAmountIncreased;
            
            levelStartButton.onClick.AddListener(PlayButtonClicked);
            UpdateInfoBars();
            PlayerPrefsManager.CheckForHealthIncrease();
        
        }

        private void OnDestroy()
        {
            levelStartButton.onClick.RemoveListener(()=> SceneTransitionController.instance.LoadSceneWithTransitionEffect(1,0));
            PlayerPrefsManager.OnHealthAmountIncreased -= PlayerPrefsManagerOnOnHealthAmountIncreased;
        }

        private void PlayButtonClicked()
        {
            if (PlayerPrefsManager.HealthAmount > 0)
            {
                SceneTransitionController.instance.LoadSceneWithTransitionEffect(1, 0);
            }
            else
            {
                DontHaveHealthSeq();
            }
        }

        private Sequence DontHaveHealthSeq()
        {
            Sequence seq = DOTween.Sequence();
            seq.Join(youDontHaveHealthCanvasGroup.DOFade(1, 0.5f).SetEase(Ease.OutCubic))
                .Append(youDontHaveHealthCanvasGroup.DOFade(0, .35f).SetDelay(.5f));

            return seq;
        }
        private void UpdateInfoBars()
        {
            coinInfoBar.UpdateAmountTxt(PlayerPrefsManager.CoinAmount);
            healthInfoBar.UpdateAmountTxt(PlayerPrefsManager.HealthAmount);
            levelTxt.SetText($"Level {PlayerPrefsManager.Level}");
        }
        
        private void PlayerPrefsManagerOnOnHealthAmountIncreased()
        {
            UpdateInfoBars();
        }
    }
}
