using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class WinUIController : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Button continueButton;
    [SerializeField] private RectTransform continueButtonRectTransform;
    [SerializeField] private CanvasGroup gainedGoldCanvasGroup;
    [SerializeField] private CanvasGroup wellDoneTextCanvasGroup;



    #region Sequences
    

    [Button()]
    private Sequence AppearSequence()
    {
        Sequence seq = DOTween.Sequence();

        seq.Join(_canvasGroup.DOFade(1, .35f).SetEase(Ease.Linear))
            .Append(wellDoneTextCanvasGroup.DOFade(1, .3f).SetEase(Ease.Linear))
            .Append(gainedGoldCanvasGroup.DOFade(1, .35f).SetEase(Ease.Linear))
            .Append(continueButtonRectTransform.DOScale(Vector3.one, .35f).SetEase(Ease.Linear));
        return seq;
    }

    #endregion

    public void OpenWinUI()
    {
        gameObject.SetActive(true);
        AppearSequence();
    }
}
