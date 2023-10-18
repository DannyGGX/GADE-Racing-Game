using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PositionTrackerUI
{
    [SerializeField] private TextMeshProUGUI positionText;

    public void UpdateUI(int position)
    {
        positionText.text = ToOrdinal(position);
    }

    private string ToOrdinal(int value)
    {
        
        
        throw new NotImplementedException();
    }
}
