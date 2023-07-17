using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class TileHolder : MonoBehaviour
{
    private const int MaxTileCount = 7;
    [SerializeField] private List<RectTransform> referencePlacesList = new List<RectTransform>();
    
    public List<SingleTile> placedTilesList = new List<SingleTile>();

    private int PlacedTileCount => placedTilesList.Count;

    private Vector3 GetEmptyReferenceAnchorPosition(int id)
    {
        if (_placedTileIdDictionary.ContainsKey(id))
        {
            var x = placedTilesList.Where((tile) => tile.imageId == id).ToList();
            var newId = x[^1].PlaceId + 1;
            
            placedTilesList.Where(tile => tile.PlaceId >= newId).ToList().ForEach(tile =>
            {
                tile.SetPlaceId(tile.PlaceId+1);
            });

            RePlaceTiles();
            var refPosition = referencePlacesList[newId].anchoredPosition;
            return new Vector3(refPosition.x, refPosition.y, newId);

        }
        else
        {
            var refPosition = referencePlacesList[PlacedTileCount].anchoredPosition;
            return new Vector3(refPosition.x, refPosition.y, PlacedTileCount);
        }
          
    } 

    private Dictionary<int, int> _placedTileIdDictionary = new Dictionary<int, int>();

    public void PlaceTile(SingleTile tile)
    {
        tile.isPlaced = true;
        
        var refPos = GetEmptyReferenceAnchorPosition(tile.imageId);
        tile._rectTransform.DOAnchorPos(new Vector2(refPos.x,refPos.y), .25f);
        placedTilesList.Add(tile);

        if (_placedTileIdDictionary.ContainsKey(tile.imageId))
        {
            _placedTileIdDictionary[tile.imageId]++;
        }
        else
        {
            _placedTileIdDictionary.Add(tile.imageId,1);
        }

        tile.SetPlaceId((int)refPos.z);
        
        CheckGameStatus();
    }

    private void CheckGameStatus()
    {
        List<SingleTile> tilesToRemove = new List<SingleTile>();

        foreach (var dict in _placedTileIdDictionary)
        {
            if (dict.Value >= 3)
            {
                // tilesToRemove listesine ekleyin
                tilesToRemove.AddRange(placedTilesList.Where(tile => tile.imageId == dict.Key));
            }
        }

        foreach (var tile in tilesToRemove)
        {
            placedTilesList.Remove(tile);
            _placedTileIdDictionary[tile.imageId]--;

            if (_placedTileIdDictionary[tile.imageId] == 0)
                _placedTileIdDictionary.Remove(tile.imageId);
            tile.DestroyTile();
        }
        
        if(tilesToRemove.Count > 1)
            ReorderTiles();

        //TODO Game Over
        if (placedTilesList.Count >= MaxTileCount)
        {
            // ...
        }
        
    }

    private void ReOrderPlacedTilesByPlaceId()
    {
        placedTilesList = placedTilesList.OrderBy(x => x.PlaceId).ToList();
    }
    private void ReorderTiles()
    {
        ReOrderPlacedTilesByPlaceId();
        for (int i = 0; i < placedTilesList.Count; i++)
        {
            placedTilesList[i].SetPlaceId(i);
        }
        
        RePlaceTiles();
    }

    private void RePlaceTiles()
    {
        
        foreach (var tile in placedTilesList)
        {
            tile._rectTransform.DOAnchorPos(referencePlacesList[tile.PlaceId].anchoredPosition, .2f);
        }
    }
}
