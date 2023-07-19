using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Workspace.Scripts
{
    public class SingleTileGroupController : MonoBehaviour
    { 
        [SerializeField] public List<SingleTile> allTilesList = new List<SingleTile>();
        [SerializeField] private List<SingleTile> openTilesList = new List<SingleTile>();

        private int OpenedTileCount => openTilesList.Count;


        private IEnumerator Start()
        {
            yield return new WaitForSeconds(3.5f);
            OpenTile();
            CloseTiles();
            
        }

        public void OpenTile()
        {
            if(openTilesList.Count >= allTilesList.Count) return;
            
            var tile = allTilesList[OpenedTileCount];
            
            tile.OpenTile();
            openTilesList.Add(tile);
        }

        private void CloseTiles()
        {
            if (allTilesList.Count > 1)
            {
                for (int i = 1; i <= allTilesList.Count - 1; i++)
                {
                    allTilesList[i].CloseTile();
                }
            }
        }
    }
}