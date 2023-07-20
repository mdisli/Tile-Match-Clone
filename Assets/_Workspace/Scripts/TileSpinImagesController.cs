using System.Collections.Generic;
using DG.Tweening;
using EasyTransition;
using UnityEngine;

namespace _Workspace.Scripts
{
    public class TileSpinImagesController : MonoBehaviour
    {
        [SerializeField] private List<RectTransform> spinImagesList = new List<RectTransform>();
        [SerializeField] private RectTransform imageHolder;

        private SingleTile _singleTile;
    
        private float _tileSize = 120;
        private int TileCount => spinImagesList.Count;

        [SerializeField]private int _imageId;
        
        private void Start()
        {
            _tileSize = spinImagesList[0].rect.width;
            _singleTile = GetComponentInParent<SingleTile>();
            _imageId = _singleTile.imageId;
            SlowImageChooseSequence();
        }

        private void OnEnable()
        {
            TransitionManager.Instance().onTransitionEnd += OnTransitionEnd;
            LevelGenerator.OnNewLevelLoaded += OnTransitionEnd;
        }
    
        private void OnDisable()
        {
            TransitionManager.Instance().onTransitionEnd -= OnTransitionEnd;
            LevelGenerator.OnNewLevelLoaded -= OnTransitionEnd;
        }

        private void OnTransitionEnd()
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
        
            HideSpinImages();
            ReScaleSpinImages(0.85f);
        
            //transform.GetChild(_imageId).SetSiblingIndex(TileCount - 1);
            
            spinImagesList[_imageId].SetSiblingIndex(TileCount - 1);

            seq.Join(FastImageChooseTween(.15f))
                .Append(FastImageChooseTween(.25f))
                .Append(FastImageChooseTween(.35f))
                .Append(FastImageChooseTween(.45f))
                .Append(FastImageChooseTween(.55f))
                .Append(FastImageChooseTween(.65f))
                .Append(imageHolder.DOAnchorPosY((TileCount-1) * _tileSize, 1.2f))
                .OnComplete(()=> ReScaleSpinImages(1));


            return seq;
        }

        #endregion

        public void ShowSelectedImage(int id)
        {
            var oldPos = imageHolder.anchoredPosition;
            oldPos.y = _tileSize * id;

            imageHolder.anchoredPosition = oldPos;
        }

        private void HideSpinImages()
        {
            var oldPos = imageHolder.anchoredPosition;
            oldPos.y = -_tileSize;
        
            imageHolder.anchoredPosition = oldPos;
        }

        private void ReScaleSpinImages(float newSize)
        {
            spinImagesList.ForEach(img => img.DOScale(Vector3.one*newSize,.5f));
        }
    }
}
