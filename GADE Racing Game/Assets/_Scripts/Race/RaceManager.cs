using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour
{
    public static RaceManager Instance { get; private set; }

    [field: SerializeField] public int TotalLaps { get; private set; }

    [SerializeField] private CarController1[] racers; // to enable and disable inputs on all racers


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        DisableRacerInputs();
    }

    private void DisableRacerInputs()
    {
        foreach (var racer in racers)
        {
            racer.InputController = new NoInput();
        }
    }

    public void EndRace()
    {

    }

}
