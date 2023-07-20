using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

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
        [SerializeField] private Button giveUpButton;

        public static event UnityAction OnPlayWithPayButtonClicked;
        #endregion

        #region Unity Funcs

        private void Start()
        {
            playWithPayButton.clicked += PlayWithPayButtonOnclicked;
        }

        private void PlayWithPayButtonOnclicked()
        {
            
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
            Sequence seq = DOTween.Sequence();

            seq.Join(failTextRectTransform.DOAnchorPosY(0, 1f).SetEase(Ease.OutBounce))
                .Append(playWithPayButtonRectTransform.DOAnchorPosX(270, 1f).SetEase(Ease.Linear))
                .Join(giveUpButtonRectTransform.DOAnchorPosX(-270, 1f).SetEase(Ease.Linear));

            return seq;
        }

        #endregion

        public void OpenFailUI()
        {
            gameObject.SetActive(true);
            UIAppearSequence();
        }

   
    }
}
