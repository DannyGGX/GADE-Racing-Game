using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneManagerScript : MonoBehaviour
{
    public static SceneManagerScript Instance { get; private set; }

    [SerializeField] private SceneSO[] scenesArray;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public void LoadScene(Scenes sceneName)
    {
        SceneSO targetScene;
        try
        {
            targetScene = Array.Find(scenesArray, x => x.sceneName == sceneName);
        }
        catch
        {
            this.LogError($"Didn't find scene: {sceneName}");
            return;
        }

        SceneManager.LoadScene(targetScene.buildIndex);
    }

}
