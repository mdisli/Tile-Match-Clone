using System.Collections.Generic;
using UnityEngine;

namespace _Workspace.Scripts
{
    public class SingleTileGroupController : MonoBehaviour
    { 
        [SerializeField] private List<SingleTile> allTilesList = new List<SingleTile>();
        [SerializeField] private List<SingleTile> openTilesList = new List<SingleTile>();

        private int OpenedTileCount => openTilesList.Count;


        private void Start()
        {
            OpenTile();
        }

        public void OpenTile()
        {
            if(openTilesList.Count >= allTilesList.Count) return;
            
            var tile = allTilesList[OpenedTileCount];
            
            tile.OpenTile();
            openTilesList.Add(tile);
        }
    }
}