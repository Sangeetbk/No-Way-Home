using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;

public class SceneManagement : MonoBehaviour 
{
    private List<AsyncOperation> _loadOperations;
    private string _currentLevelName;

   // private Events.EventUnloadScene UnloadCurrentScene;

    protected int callbackvalue = 0;

    private void Start() 
    {
        _loadOperations = new List<AsyncOperation>();
        //  GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
        GameManager.Instance.OnLoadLevelChanged.AddListener(HandleLoadingLevel);
        GameManager.Instance.UnloadCurrentScene.AddListener(OnUnloadCurrentScene);
    }

    private void OnUnloadCurrentScene()
    {
        UnloadLevel(_currentLevelName);
    }

    private void Update()
    {
        
    }

    private void HandleLoadingLevel(bool callBack)
    {
        if(callBack == true)
        {
            _currentLevelName = GameManager.Instance._currentLevelName;

            LoadLevel(_currentLevelName);
        }
    }

    void OnLoadOperationComplete(AsyncOperation ao)
    {
        if (_loadOperations.Contains(ao))
        {
            _loadOperations.Remove(ao);

            if (_loadOperations.Count == 0)
            {
                GameManager.Instance.UpdateState(GameManager.GameState.RUNNING);
            }
        }
    }

    void OnUnloadOperationComplete(AsyncOperation ao)
    {
        // Clean up level is necessary, go back to main menu
    }

   

    void HandleMainMenuFadeComplete(bool fadeIn)
    {
        if (!fadeIn)
        {
            UnloadLevel(_currentLevelName);
        }
    }

/*    private void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState prevState)
    {
        if(currentState == GameManager.GameState.RUNNING)
        {
            if(_currentLevelName == null)
            {
                 LoadLevel("Prototype_1");
            }
            Debug.Log("Loading Prototype 1");
        }
    }*/

    public void LoadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Unable to load level " + levelName);
            return;
        }

        ao.completed += OnLoadOperationComplete;
        _loadOperations.Add(ao);

        _currentLevelName = levelName;
    }

    public void UnloadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
        ao.completed += OnUnloadOperationComplete;
    }

}