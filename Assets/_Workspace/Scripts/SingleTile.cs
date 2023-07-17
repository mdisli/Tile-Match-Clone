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
    [HideInInspector]  public bool isPlaced;
    
    private int _placeId;
    public int PlaceId => _placeId;
    
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
        transform.DOScale(Vector3.zero, .25f)
            .SetEase(Ease.InQuart)
            .SetDelay(.3f)
            .OnComplete(() => Destroy(gameObject));
    }

    public void SetPlaceId(int newId)
    {
        _placeId = newId;
    }

    [Button]
    public void ShowSelectedImage()
    {
        tileSpinner.ShowSelectedImage(imageId);
    }

}
