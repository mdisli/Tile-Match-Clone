using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileSpinImagesController : MonoBehaviour
{
    [SerializeField] private List<RectTransform> spinImagesList = new List<RectTransform>();
    [SerializeField] private RectTransform imageHolder;
    
    private float _tileSize = 120;
    private int TileCount => spinImagesList.Count;


    private void Awake()
    {
        _tileSize = spinImagesList[0].rect.width;
    }

    private void Start()
    {
        SlowImageChooseSequence();
    }

    #region Image Spin Sequence

    private Tween FastImageChooseTween(float duration)
    {
        return imageHolder.DOAnchorPosY(TileCount * _tileSize, duration)
            .SetEase(Ease.Linear)
            .OnComplete(()=>
            {
                imageHolder.anchoredPosition = new Vector2(imageHolder.anchoredPosition.x, -_tileSize);
            });
    }

    private Sequence SlowImageChooseSequence()
    {
        Sequence seq = DOTween.Sequence();

        int randomNum = Random.Range(0, TileCount-1);
        
        transform.GetChild(0).GetChild(randomNum).SetSiblingIndex(TileCount - 1);

        seq.Join(FastImageChooseTween(.15f))
            .Append(FastImageChooseTween(.25f))
            .Append(FastImageChooseTween(.35f))
            .Append(FastImageChooseTween(.45f))
            .Append(FastImageChooseTween(.55f))
            .Append(FastImageChooseTween(.65f))
            .Append(imageHolder.DOAnchorPosY((TileCount-1) * _tileSize, 1.2f));


        return seq;
    }

    #endregion

    public void ShowSelectedImage(int id)
    {
        var oldPos = imageHolder.anchoredPosition;
        oldPos.y = _tileSize * id;

        imageHolder.anchoredPosition = oldPos;
    }

    public void HideSpinImages()
    {
        var oldPos = imageHolder.anchoredPosition;
        oldPos.y = -_tileSize;
        
        imageHolder.anchoredPosition = oldPos;
    }
}
