using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHolder : MonoBehaviour
{
    [SerializeField] private List<RectTransform> referencePlacesList = new List<RectTransform>();
    [SerializeField] public List<SingleTile> placedTilesList = new List<SingleTile>();

    private int PlacedTileCount => placedTilesList.Count;
    public Vector2 GetEmptyReferenceAnchorPosition => referencePlacesList[PlacedTileCount].anchoredPosition;
}
