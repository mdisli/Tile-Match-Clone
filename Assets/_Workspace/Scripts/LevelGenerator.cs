using System;
using System.Collections.Generic;
using _Workspace.Scripts;
using EasyTransition;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class LevelGenerator : MonoBehaviour
{
    public SingleTile singleTilePrefab;
    public SingleTileGroupController singleTileGroupControllerPrefab;
    public LevelController emptyLevelObject;

    public List<TextAsset> levelJsonList = new List<TextAsset>();

    public TextAsset testLevelJson;
    
    private LevelController _activeLevelController;

    public static event UnityAction OnNewLevelLoaded;
    private void Start()
    {
        GenerateLevel();
    }

    private void OnEnable()
    {
        WinUIController.OnNextLevelButtonClicked += LoadNextLevel;
    }

    private void OnDisable()
    {
        WinUIController.OnNextLevelButtonClicked -= LoadNextLevel;
    }

    private void LoadNextLevel()
    {

        Destroy(_activeLevelController.gameObject);
        GenerateLevel();
        
        OnNewLevelLoaded?.Invoke();
        TransitionManager.Instance().onTransitionEnd?.Invoke();
    }
    
    private void GenerateLevel()
    {

        TextAsset levelJson = levelJsonList[GameManager.instance.Level % levelJsonList.Count];

        LevelJsonClass levelData = JsonUtility.FromJson<LevelJsonClass>(levelJson.text);


        LevelController levelController= Instantiate(emptyLevelObject, transform);

        Transform tilePlacer = levelController.transform.GetChild(0);
        
        levelData.allSingleTilesSettings.ForEach(tile =>
        {
            SingleTile newTile = Instantiate(singleTilePrefab, tile.position, Quaternion.Euler(tile.rotation), tilePlacer);
            
            newTile.imageId = tile.imageId;
            
            levelController._singleTilesList.Add(newTile);
        });


        levelData.allTileGroupsSettings.ForEach(group =>
        {
            var newGroup = Instantiate(singleTileGroupControllerPrefab, group.position, Quaternion.Euler(group.rotation), tilePlacer);

            for (int i = group.tileCount-1; i >=0; i--)
            {
                var tileSetting = group.allTilesSettings[i];
                var newTile = Instantiate(singleTilePrefab, tileSetting.position, Quaternion.Euler(tileSetting.rotation), newGroup.transform);
                newTile.imageId = tileSetting.imageId;
                newGroup.allTilesList.Add(newTile);
            }
            
            newGroup.allTilesList = ReverseList(newGroup.allTilesList);
            levelController._singleTileGroupControllers.Add(newGroup);
            
        });

        _activeLevelController = levelController;
    }
    

    [Button()]
    public void GenerateLevelTest()
    {
        TextAsset levelJson = testLevelJson;

        LevelJsonClass levelData = JsonUtility.FromJson<LevelJsonClass>(levelJson.text);


        LevelController levelController= Instantiate(emptyLevelObject, transform);

        Transform tilePlacer = levelController.transform.GetChild(0);
        
        levelData.allSingleTilesSettings.ForEach(tile =>
        {
            SingleTile newTile = Instantiate(singleTilePrefab, tile.position, Quaternion.Euler(tile.rotation), tilePlacer);
            
            newTile.imageId = tile.imageId;
            
            levelController._singleTilesList.Add(newTile);
        });


        levelData.allTileGroupsSettings.ForEach(group =>
        {
            var newGroup = Instantiate(singleTileGroupControllerPrefab, group.position, Quaternion.Euler(group.rotation), tilePlacer);

            for (int i = group.tileCount-1; i >=0; i--)
            {
                var tileSetting = group.allTilesSettings[i];
                var newTile = Instantiate(singleTilePrefab, tileSetting.position, Quaternion.Euler(tileSetting.rotation), newGroup.transform);
                newTile.imageId = tileSetting.imageId;
                newGroup.allTilesList.Add(newTile);
            }
            
            newGroup.allTilesList = ReverseList(newGroup.allTilesList);
            levelController._singleTileGroupControllers.Add(newGroup);
            
        });
    }

    private List<SingleTile> ReverseList(List<SingleTile> list)
    {
        List<SingleTile> reversedList = new List<SingleTile>();

        for (int i = list.Count - 1; i >= 0; i--)
        {
            reversedList.Add(list[i]);
        }

        return reversedList;
    }

}
