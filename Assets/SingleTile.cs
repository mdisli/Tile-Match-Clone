using System;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.EventSystems;

public class SingleTile : MonoBehaviour, IPointerDownHandler
{
    #region Variables
    
    public int imageId;
    [SerializeField] private TileSpinImagesController tileSpinner;
    [HideInInspector] public RectTransform _rectTransform;
    [HideInInspector] public bool isPlaced;
    [HideInInspector] public int placeId;
    
    private TileHolder _activeTileHolder;
    #endregion

    #region Unity Funcs

    private void Start()
    {

        _activeTileHolder = GetComponentInParent<TileHolder>();
        _rectTransform = GetComponent<RectTransform>();
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if(isPlaced) return;
        isPlaced = true;
        
        _activeTileHolder.PlaceTile(this);

    }

    #endregion

    public void DestroyTile()
    {
        transform.DOScale(Vector3.zero, .15f).SetEase(Ease.Linear).OnComplete(() => Destroy(gameObject));
    }

    [Button]
    public void ShowSelectedImage()
    {
        tileSpinner.ShowSelectedImage(imageId);
    }

}
