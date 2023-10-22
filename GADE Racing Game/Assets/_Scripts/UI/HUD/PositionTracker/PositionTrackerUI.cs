using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class PositionTrackerUI
{
    [SerializeField] private TextMeshProUGUI positionText;

    public void Hide()
    {
        positionText.gameObject.SetActive(false);
    }

    public void Show()
    {
        positionText.gameObject.SetActive(true);
    }
    public void UpdateUI(int position)
    {
        positionText.text = ToOrdinal(position);
    }

    private static string ToOrdinal(int value)
    {
        // switch(value % 100)
        // {
        //     case 11:
        //     case 12:
        //     case 13:
        //         return value + "th";
        // }
    
        switch(value % 10)
        {
            case 1:
                return value + "st";
            case 2:
                return value + "nd";
            case 3:
                return value + "rd";
            default:
                return value + "th";
        }
    }
}
