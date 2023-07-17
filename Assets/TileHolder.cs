using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class TileHolder : MonoBehaviour
{
    private const int MaxTileCount = 7;
    [SerializeField] private List<RectTransform> referencePlacesList = new List<RectTransform>();
    
    private List<SingleTile> placedTilesList = new List<SingleTile>();

    private int PlacedTileCount => placedTilesList.Count;

    private Vector3 GetEmptyReferenceAnchorPosition(int id)
    {
        if (_placedTileIdDictionary.ContainsKey(id))
        {
            var x = placedTilesList.Where((tile) => tile.imageId == id).ToList();
            var newId = x[^1].placeId + 1;
            
            placedTilesList.Where(tile => tile.placeId >= newId).ToList().ForEach(tile => tile.placeId++);
            
            ReorderTiles();

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

        tile.placeId = (int)refPos.z;
        
        CheckGameStatus();
    }

    private void CheckGameStatus()
    {
        foreach (var dict in _placedTileIdDictionary)
        {
            if (dict.Value >= 3)
            {
                // clear Items
                var tilesList = placedTilesList.Where(tile => tile.imageId == dict.Key).ToList();

                foreach (var tile in tilesList)
                {
                    placedTilesList.Remove(tile);
                    _placedTileIdDictionary[tile.imageId]--;
                    tile.DestroyTile();
                }


                int i = 0;
                foreach (var placedTile in placedTilesList)
                {
                    placedTile.placeId = i;
                    i++;
                }
            }
        }
        
        //TODO Game Over
        if (placedTilesList.Count >= MaxTileCount)
        {
            
        }
        
    }

    private void ReorderTiles()
    {
        foreach (var tile in placedTilesList)
        {
            tile._rectTransform.DOAnchorPos(referencePlacesList[tile.placeId].anchoredPosition, .2f);
        }
    }
}
