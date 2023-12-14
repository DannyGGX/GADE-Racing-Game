using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuObject;

    private void Awake()
    {
        menuObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangePauseScreenState(PauseManager.Instance.IsPaused);
        }
    }
    private void ChangePauseScreenState(bool pauseState)
    {
        if (pauseState)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        menuObject.SetActive(true);
        PauseManager.Instance.SetPauseState(true);
    }

    public void ResumeGame()
    {
        menuObject.SetActive(false);
        PauseManager.Instance.SetPauseState(false);
        SFXManager.Instance?.PlaySound(SoundNames.ButtonClick);
    }

    public void GoToMainMenu()
    {
        PauseManager.Instance.SetPauseState(false);
        SceneManagerScript.Instance.LoadScene(Scenes.MainMenu);
        SFXManager.Instance?.PlaySound(SoundNames.ButtonClick);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
