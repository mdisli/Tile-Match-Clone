using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NaughtyAttributes;
using UnityEngine;

namespace _Workspace.Scripts
{
    public class LevelController : MonoBehaviour
    {
        public string levelName;
        public List<SingleTile> _singleTilesList = new List<SingleTile>();
        public List<SingleTileGroupController> _singleTileGroupControllers = new List<SingleTileGroupController>();

        [HideInInspector]public int totalTileCount;

        public int levelId;

        #region Unity Funcs

        private IEnumerator Start()
        {
            CalculateTotalTileCount();
            
            // Wait for spin animation 
            yield return new WaitForSeconds(3.6f);
            //yield return null;
            GameManager.instance.SetGameStatus(GameStatus.Playing);
        }

        private void OnEnable()
        {
            LevelGenerator.OnNewLevelLoaded += OnNewLevelLoaded;
        }

        private void OnDisable()
        {
            LevelGenerator.OnNewLevelLoaded -= OnNewLevelLoaded;
        }

        #endregion

        private void OnNewLevelLoaded()
        {
            CalculateTotalTileCount();
        }

        private void CalculateTotalTileCount()
        {
            totalTileCount = GameObject.FindObjectsOfType<SingleTile>().Length;
        }

        #region Exporting Json

        private SingleTileJsonClass GetDataFromSingleTile(SingleTile tile)
        {
            SingleTileJsonClass tileData = new SingleTileJsonClass();

            //tileData.position = tile.transform.position;
            tileData.anchoredPosition = tile._rectTransform.anchoredPosition;
            tileData.anchorMax = tile._rectTransform.anchorMax;
            tileData.anchorMin = tile._rectTransform.anchorMin;
            tileData.rotation = tile.transform.eulerAngles;
            tileData.imageId = tile.imageId;

            return tileData;
        }

        private TileGroupJsonClass GetDataFromTileGroup(SingleTileGroupController tileGroupController)
        {
            TileGroupJsonClass tileGroupData = new TileGroupJsonClass();

            tileGroupData.position = tileGroupController.transform.position;
            tileGroupData.rotation = tileGroupController.transform.eulerAngles;
            tileGroupData.tileCount = tileGroupController.allTilesList.Count;
            
            var tileGroupDataAllTilesSettings = new List<SingleTileJsonClass>();

            foreach (var t in tileGroupController.allTilesList)
            {
                tileGroupDataAllTilesSettings.Add(GetDataFromSingleTile(t));
            }
            
            tileGroupData.allTilesSettings = tileGroupDataAllTilesSettings;
            
            return  tileGroupData;
        }


        [Button]
        public void ExportLevelJson()
        {
            var levelJsonClass = new LevelJsonClass();

            var allTileGroupsSettings = new List<TileGroupJsonClass>();
            var allSingleTilesSettings = new List<SingleTileJsonClass>();

            foreach (var t in _singleTileGroupControllers)
            {
                allTileGroupsSettings.Add(GetDataFromTileGroup(t));
            }

            foreach (var t in _singleTilesList)
            {
                allSingleTilesSettings.Add(GetDataFromSingleTile(t));
            }

            levelJsonClass.allTileGroupsSettings = allTileGroupsSettings;
            levelJsonClass.allSingleTilesSettings = allSingleTilesSettings;
            
            
            string json = JsonUtility.ToJson(levelJsonClass, true);

            string path = "Assets/_Workspace/LevelData/";
            path += levelName + ".json";
            File.WriteAllText(path, json);
            Debug.Log("Level Saved");

#if UNITY_EDITOR
            
            UnityEditor.AssetDatabase.Refresh();
#endif
        }

        #endregion
    }

    #region Classes for json

    [Serializable]
    public class LevelJsonClass
    {
        public List<TileGroupJsonClass> allTileGroupsSettings = new List<TileGroupJsonClass>();
        public List<SingleTileJsonClass> allSingleTilesSettings = new List<SingleTileJsonClass>();
    }
    
    [Serializable]
    public class SingleTileJsonClass
    {
        public Vector2 anchoredPosition;
        public Vector2 anchorMax;
        public Vector2 anchorMin;
        public Vector3 rotation;
        public int imageId;

    }

    [Serializable]
    public class TileGroupJsonClass
    {
        public Vector3 position;
        public Vector3 rotation;
        
        public int tileCount;

        public List<SingleTileJsonClass> allTilesSettings = new List<SingleTileJsonClass>();

    }

    #endregion
}
