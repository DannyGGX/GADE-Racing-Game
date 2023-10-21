using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSequence : MonoBehaviour
{
    [SerializeField] private StartSequenceUI startSequenceUI;
    
    [SerializeField] private EventSenderSO onStartRace;

    private void Start()
    {
        Invoke(nameof(StartReadyUp), 0.5f);
    }

    private void StartReadyUp()
    {
        startSequenceUI.StartReadyUp();
    }

    public void StartRace()
    {
        onStartRace.Invoke();
    }

}
