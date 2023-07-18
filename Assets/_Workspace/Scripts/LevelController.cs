using System;
using System.Collections;
using UnityEngine;

namespace _Workspace.Scripts
{
    public class LevelController : MonoBehaviour
    {
        private IEnumerator Start()
        {
            // Wait for spin animation 
            //yield return new WaitForSeconds(3.6f);
            yield return null;
            GameManager.instance.SetGameStatus(GameStatus.Playing);
        }
    }
}
