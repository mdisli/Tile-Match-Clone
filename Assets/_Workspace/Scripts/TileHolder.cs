using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace _Workspace.Scripts
{
    public class TileHolder : MonoBehaviour
    {
        public static TileHolder instance;

        #region Unity Actions
        public static event UnityAction OnGameFailed;
        public static event UnityAction OnGameCompleted;

        #endregion




        #region Variables

        private const int MaxTileCount = 7;
        [SerializeField] private List<RectTransform> referencePlacesList = new List<RectTransform>();
        public List<SingleTile> placedTilesList = new List<SingleTile>();
        private int PlacedTileCount => placedTilesList.Count;
        private int _poppedTileCount=0;
        private int totalTileCount => _levelController.TotalTileCount;

        private Dictionary<int, int> _placedTileIdDictionary = new Dictionary<int, int>();

        private LevelController _levelController;
        #endregion

        #region Unity Funcs

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else 
                Destroy(gameObject);
        }

        private void Start()
        {
            _levelController = GameObject.FindObjectOfType<LevelController>();
        }

        #endregion

        private Vector3 GetEmptyReferenceAnchorPosition(int id)
        {
            if (_placedTileIdDictionary.ContainsKey(id))
            {
                var x = placedTilesList.Where((tile) => tile.imageId == id).ToList();
                var newId = x[^1].PlaceId + 1;
            
                SwipeTiles(newId);

                //await Task.Delay(150);
            
                var refPosition = referencePlacesList[newId].position;
                return new Vector3(refPosition.x, refPosition.y, newId);

            }
            else
            {
                var refPosition = referencePlacesList[PlacedTileCount].position;
                return new Vector3(refPosition.x, refPosition.y, PlacedTileCount);
            }
          
        } 

        

        public  void PlaceTile(SingleTile tile)
        {
            tile.isPlaced = true;
        
            var refPos = GetEmptyReferenceAnchorPosition(tile.imageId);
        

            tile._rectTransform.DOMove(new Vector3(refPos.x, refPos.y,0), .25f);

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
                    tilesToRemove.AddRange(placedTilesList.Where(tile => tile.imageId == dict.Key));
                }
            }

            if (tilesToRemove.Count > 0)
                StartCoroutine(PopTiles(tilesToRemove));
        

            //TODO Game Over
            if (placedTilesList.Count >= MaxTileCount)
            {
                OnGameFailed?.Invoke();
            }


            if (_poppedTileCount >= totalTileCount)
            {
                OnGameCompleted?.Invoke();
            }
        }

        private IEnumerator PopTiles(List<SingleTile> tilesToRemove)
        {
            foreach (var tile in tilesToRemove)
            {
                placedTilesList.Remove(tile);
                _placedTileIdDictionary[tile.imageId]--;

                if (_placedTileIdDictionary[tile.imageId] == 0)
                    _placedTileIdDictionary.Remove(tile.imageId);
                tile.DestroyTile();

                _poppedTileCount++;
            }

            yield return new WaitForSeconds(.45f);
        
            ReorderTiles();
        
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
        
            RePlaceTiles(delay:.1f);
        }

        private void RePlaceTiles(float duration = .2f, float delay=0f)
        {
            int i = 0;
            foreach (var tile in placedTilesList)
            {
                tile._rectTransform.DOMove(referencePlacesList[tile.PlaceId].position, duration)
                    .SetDelay(i * delay);

                i++;
            }
        }

        private void SwipeTiles(int startIndex)
        {
            placedTilesList.Where(tile => tile.PlaceId >= startIndex).ToList().ForEach(tile =>
            {
                tile.SetPlaceId(tile.PlaceId+1);
            });

            RePlaceTiles(.25f,0);
        }
    }
}
