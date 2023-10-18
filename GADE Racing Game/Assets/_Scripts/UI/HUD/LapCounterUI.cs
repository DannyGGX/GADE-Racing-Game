using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class LapCounterUI
{
    [SerializeField] private TextMeshProUGUI lapText;
    private int currentLap = 1;
    private int _totalLaps;

    public void Initialize(int totalLaps)
    {
        // get the total laps from the lap manager
        _totalLaps = totalLaps;
        lapText.text = $"{currentLap}/{_totalLaps}";
    }

    public void SetNextLap()
    {
        currentLap++;
        lapText.text = $"{currentLap}/{_totalLaps}";
    }
}
