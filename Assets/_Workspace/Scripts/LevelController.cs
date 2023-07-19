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
        public List<SingleTile> _singleTilesList = new List<SingleTile>();
        public List<SingleTileGroupController> _singleTileGroupControllers = new List<SingleTileGroupController>();

        public int TotalTileCount {

            get
            {
                int count = 0;
                count += _singleTilesList.Count;
                _singleTileGroupControllers.ForEach(x => count += x.allTilesList.Count);
                
                return count;
            }
        }

        public int levelId;

        #region Unity Funcs

        private IEnumerator Start()
        {
            // Wait for spin animation 
            //yield return new WaitForSeconds(3.6f);
            yield return null;
            GameManager.instance.SetGameStatus(GameStatus.Playing);
        }

        #endregion


        #region Exporting Json

        private SingleTileJsonClass GetDataFromSingleTile(SingleTile tile)
        {
            SingleTileJsonClass tileData = new SingleTileJsonClass();

            tileData.position = tile.transform.position;
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
            path += "Level" + levelId + ".json";
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
        public Vector3 position;
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
