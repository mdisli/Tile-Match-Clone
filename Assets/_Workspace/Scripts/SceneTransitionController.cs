using System.Collections.Generic;
using EasyTransition;
using UnityEngine;

public class SceneTransitionController : MonoBehaviour
{
    public static SceneTransitionController instance;
    public List<TransitionSettings> transitionSettingsList;

    private void Awake()
    {
        //Singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);
    }

    public void LoadSceneWithTransitionEffect(int sceneIndex, float startDelay)
    {
        TransitionManager.Instance().Transition(sceneIndex, transitionSettingsList[Random.Range(0,transitionSettingsList.Count)], startDelay);
    }

}
