using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool IsPaused { get; private set; } = false;
    public bool ControlsEnabled { get; private set; } = true;


    private enum Scenes
    {
        MainMenu,
        Checkpoint_Intro,
        Checkpoint_Race
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        DontDestroyOnLoad(gameObject);

    }

    void Start()
    {
        //StartMainMenu();
    }
    public void StartMainMenu()
    {
        SceneManager.LoadScene((int)Scenes.MainMenu);
    }
    public void StartIntro()
    {
        
    }
    public void StartRace()
    {
        // Decide which one to start from which intro scene is active
        // Probably have scriptable objects for each scene with their scene index
    }


    public void PauseToggle()
    {
        if (IsPaused)
            Unpause();
        else
            Pause();
    }
    public void SetPauseState(bool state)
    {
        if (state)
            Pause();
        else
            Unpause();
    }
    private void Pause()
    {
        Time.timeScale = 0;
        IsPaused = true;
        ControlsEnabled = false;
    }
    private void Unpause()
    {
        Time.timeScale = 1;
        IsPaused = false;
        ControlsEnabled = true;
    }
}
