using System;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SingleTile : MonoBehaviour, IPointerDownHandler
{
    #region Variables
    
    [SerializeField] private TileSpinImagesController tileSpinner;

    private TileHolder _activeTileHolder;
    private RectTransform _rectTransform;
    
    [SerializeField] private int imageId;

    #endregion

    #region Unity Funcs

    private void Start()
    {

        _activeTileHolder = GetComponentInParent<TileHolder>();
        _rectTransform = GetComponent<RectTransform>();
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        var refPos = _activeTileHolder.GetEmptyReferenceAnchorPosition;
        _rectTransform.DOAnchorPos(refPos, .25f);
        _activeTileHolder.placedTilesList.Add(this);

    }

    #endregion


    [Button]
    public void ShowSelectedImage()
    {
        tileSpinner.ShowSelectedImage(imageId);
    }

}
