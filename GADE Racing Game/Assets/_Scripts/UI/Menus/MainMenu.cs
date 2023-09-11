using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject MainScreen;
    //[SerializeField] private GameObject OptionsScreen;
    //[SerializeField] private GameObject CreditsScreen;
    [SerializeField] private GameObject RacingGameModesScreen;

    public void BackButtonClicked()
    {
        //CreditsScreen.SetActive(false);
        //OptionsScreen.SetActive(false);
        MainScreen.SetActive(true);
        RacingGameModesScreen.SetActive(false);
    }

    public void GoToCredits()
    {
        //CreditsScreen.SetActive(true);
        MainScreen.SetActive(false);
    }
    public void GoToOptions()
    {
        //OptionsScreen.SetActive(true);
        MainScreen.SetActive(false);
    }

    public void GoToRacingGameModes()
    {
        MainScreen.SetActive(false);
        RacingGameModesScreen.SetActive(true);
    }

    public void StartCheckpointRace()
    {
        SceneManagerScript.Instance.LoadScene(Scenes.Checkpoint_Intro);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
