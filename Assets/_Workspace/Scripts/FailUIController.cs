using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Workspace.Scripts
{
    public class FailUIController : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
    {
        #region Variables

        [SerializeField] private CanvasGroup canvasGroup;
    
        [SerializeField] private RectTransform failTextRectTransform;
        [SerializeField] private RectTransform playWithPayButtonRectTransform;
        [SerializeField] private RectTransform giveUpButtonRectTransform;

        [SerializeField] private Button playWithPayButton;
        [SerializeField] private TextMeshProUGUI playWithPayText;
        [SerializeField] private Button giveUpButton;

        [SerializeField] private CanvasGroup notEnoughMoneyCanvasGroup;
        public static event UnityAction OnPlayWithPayButtonClicked;
        public static event UnityAction OnGiveUpButtonClicked;

        private bool _isPlayWithPayUsed = false;
        #endregion

        #region Unity Funcs

        private void Start()
        {
            playWithPayButton.onClick.AddListener(PlayWithPayButtonOnClicked);
            giveUpButton.onClick.AddListener(GiveUpButtonClicked);
        }

        private void PlayWithPayButtonOnClicked()
        {
            if (PlayerPrefsManager.CheckMoneyEnough(PlayerPrefsManager.PlayWithPayMoneyAmount))
            {
                OnPlayWithPayButtonClicked?.Invoke();
                PlayerPrefsManager.IncreasePlayWithPayCount();
                _isPlayWithPayUsed = true;
                CloseFailUI();
            }
            else
            {
                // dont have enough money
                notEnoughMoneyCanvasGroup.DOFade(1, .3f)
                    .OnComplete(() => notEnoughMoneyCanvasGroup.DOFade(0, .3f).SetDelay(.5f));
            }
        }

        private void GiveUpButtonClicked()
        {
                OnGiveUpButtonClicked?.Invoke();
                PlayerPrefsManager.AddHealth(-1);
                SceneTransitionController.instance.LoadSceneWithTransitionEffect(0,0);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            DOTween.Kill(canvasGroup);
            canvasGroup.DOFade(0, 1f);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            DOTween.Kill(canvasGroup);
            canvasGroup.DOFade(1, 1f);
        }

        #endregion

        #region UI Elements Tweens

        private Sequence UIAppearSequence()
        {
            Debug.Log("Sequence Worked");
            Sequence seq = DOTween.Sequence();

            seq.Join(canvasGroup.DOFade(1, 1f));
            seq.Append(failTextRectTransform.DOAnchorPosY(0, 1f).SetEase(Ease.OutBounce));

            if (!_isPlayWithPayUsed)
            {
                seq.Append(giveUpButtonRectTransform.DOAnchorPosX(270, 1f).SetEase(Ease.Linear));
                seq.Join(playWithPayButtonRectTransform.DOAnchorPosX(-270, 1f).SetEase(Ease.Linear));
            }
            else
            {
                playWithPayButtonRectTransform.DOAnchorPosX(5000, 0);
                seq.Append(giveUpButtonRectTransform.DOAnchorPosX(0, 1.2f).SetEase(Ease.Linear));
            }
                

            return seq;
        }

        #endregion

        public void OpenFailUI()
        {
            gameObject.SetActive(true);
            UpdatePlayWithPayText();
            UIAppearSequence();
        }

        public void CloseFailUI()
        {
            canvasGroup.DOFade(0, .3f)
                .OnComplete(() => gameObject.SetActive(false));
            
            playWithPayButtonRectTransform.DOAnchorPosX(-1250, 0);
            giveUpButtonRectTransform.DOAnchorPosX(1250, 0);
            failTextRectTransform.DOAnchorPosY(1550, 0);
        }

        private void UpdatePlayWithPayText()
        {
            playWithPayText.SetText($"Play On ${PlayerPrefsManager.PlayWithPayMoneyAmount}");
        }
   
    }
}
