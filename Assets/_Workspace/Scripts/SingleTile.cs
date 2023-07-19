using System;
using _Workspace.Scripts;
using DG.Tweening;
using EasyTransition;
using NaughtyAttributes;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SingleTile : MonoBehaviour, IPointerDownHandler
{
    #region Variables
    
    public int imageId;
    [SerializeField] private TileSpinImagesController tileSpinner;
    [HideInInspector] public RectTransform _rectTransform;
    [HideInInspector]  public bool isPlaced;
    
    private int _placeId;
    public int PlaceId => _placeId;

    private bool _isOpen = true;
    [SerializeField] private CanvasGroup closedImage;

    private SingleTileGroupController _tileGroupController;
    #endregion

    #region Unity Funcs
    private void Start()
    {
        // If tile dont have group open directly
        if(!transform.parent.TryGetComponent<SingleTileGroupController>(out _tileGroupController))
            OpenTile();
        _rectTransform = GetComponent<RectTransform>();
        
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(GameManager.instance.ActiveGameStatus != GameStatus.Playing) return;
        if(isPlaced) return;
        if(!_isOpen) return;

        isPlaced = true;
        
        TileHolder.instance.PlaceTile(this);
        
        if(_tileGroupController !=null)
            _tileGroupController.OpenTile();

    }

    #endregion
    
    public void DestroyTile()
    {
        transform.DOScale(Vector3.zero, .25f)
            .SetEase(Ease.InQuart)
            .SetDelay(.15f)
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

    public void CloseTile()
    {
        closedImage.gameObject.SetActive(true);
        _isOpen = false;
        closedImage.DOFade(1, .3f).From(0);
    }


    public void OpenTile()
    {
        closedImage.DOFade(0, .3f)
            .OnComplete(() =>
            {
                _isOpen = true;
                closedImage.gameObject.SetActive(false);
            });
        
        
    }
}
