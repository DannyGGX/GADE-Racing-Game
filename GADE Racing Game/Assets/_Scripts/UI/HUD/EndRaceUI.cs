using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndRaceUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI raceFinishedMessage;
    [SerializeField] private TextMeshProUGUI raceLoseMessage;
    [SerializeField] private Button mainMenuButton;

    private void Awake()
    {
        
    }

    public void RaceFinished()
    {
        raceFinishedMessage.gameObject.SetActive(true);
        mainMenuButton.gameObject.SetActive(true);
    }

    public void RaceLose()
    {
        raceLoseMessage.gameObject.SetActive(true);
        mainMenuButton.gameObject.SetActive(true);
    }

    public void GoToMainMenu() // called on button click
    {
        SceneManagerScript.Instance.LoadScene(Scenes.MainMenu);
    }
}
