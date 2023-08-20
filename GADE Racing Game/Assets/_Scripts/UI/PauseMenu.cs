using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuObject;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangePauseScreenState(GameManager.Instance.IsPaused);
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
        GameManager.Instance.SetPauseState(true);
    }

    public void ResumeGame()
    {
        menuObject.SetActive(false);
        GameManager.Instance.SetPauseState(false);
    }

    public void GoToMainMenu()
    {
        GameManager.Instance.SetPauseState(false);
        GameManager.Instance.StartMainMenu();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
