using System;
using DG.Tweening;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Workspace.Scripts
{
    public class WinUIController : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Button continueButton;
        [SerializeField] private RectTransform continueButtonRectTransform;
        [SerializeField] private CanvasGroup gainedGoldCanvasGroup;
        [SerializeField] private TextMeshProUGUI gainedGoldText;
        [SerializeField] private CanvasGroup wellDoneTextCanvasGroup;

        [SerializeField] private Button homeButton;


        public static event UnityAction OnNextLevelButtonClicked;


        private void Start()
        {
            continueButton.onClick.AddListener(CloseWinUI);
            homeButton.onClick.AddListener(() => SceneTransitionController.instance.LoadSceneWithTransitionEffect(0, 0));
        }

        #region Sequences
    

        [Button()]
        private Sequence AppearSequence()
        {
            Sequence seq = DOTween.Sequence();

            seq.Join(_canvasGroup.DOFade(1, .35f).SetEase(Ease.Linear))
                .Append(wellDoneTextCanvasGroup.DOFade(1, .3f).SetEase(Ease.Linear))
                .Append(gainedGoldCanvasGroup.DOFade(1, .35f).SetEase(Ease.Linear))
                .Join(SetGainedGoldTextTween())
                .Append(continueButtonRectTransform.DOScale(Vector3.one, .35f).SetEase(Ease.Linear));
            return seq;
        }

        #endregion

        public void OpenWinUI()
        {
            gameObject.SetActive(true);

            AppearSequence();
        }

        public void CloseWinUI()
        {
            _canvasGroup.DOFade(0, .35f)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    gameObject.SetActive(false);
                    OnNextLevelButtonClicked?.Invoke();
                });
        }

        private Tween SetGainedGoldTextTween()
        {
            return DOVirtual.Float(0, GameManager.instance.gainedCoinOnThisLevel, 1f,
                ((value) => gainedGoldText.SetText(((int)value).ToString())));
        }
    }
}
