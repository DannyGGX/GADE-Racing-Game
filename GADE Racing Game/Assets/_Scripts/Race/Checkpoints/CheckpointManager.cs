using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField, Range(1, 3)] private int totalLaps = 1;
    private int currentLap = 1;
    [Space]
    [SerializeField] private Checkpoint[] checkpointsArray;
    private Stack<Checkpoint> checkpointsStack;


    private void Start()
    {
        StartLap();
    }
    private void StartLap()
    {
        PopulateStack();
        SetFirstTargetCheckpoint();
    }
    private void PopulateStack()
    {
        checkpointsStack = new Stack<Checkpoint>();
        for (int i = checkpointsArray.Length - 1; i >= 0; i--) // Add checkpoints backwards into the stack because of first in last out.
        {
            checkpointsStack.Push(checkpointsArray[i]);
            checkpointsArray[i].SetAsFuture();
        }
    }

    private void SetFirstTargetCheckpoint()
    {
        checkpointsStack.Peek().SetAsTarget();
    }

    public void CurrentTargetCheckpointPassed()
    {
        if (checkpointsStack.Count > 1)
        {
            checkpointsStack.Pop();
            checkpointsStack.Peek().SetAsTarget();
            this.Log("Set next checkpoint");
        }
        else if (currentLap < totalLaps)
        {
            currentLap++;
            StartLap();
        }
        else // Race is finished
        {
            this.Log("Finished race");
            // Invoke end race UI
        }
    }
}
