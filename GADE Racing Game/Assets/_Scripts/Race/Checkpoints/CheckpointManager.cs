using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField, Range(1, 3)] private int totalLaps = 1;
    private int currentLap = 1;
    [Space]
    [SerializeField] private Checkpoint[] checkpointsArray;
    private Stack<Checkpoint> checkpoints;


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
        checkpoints = new Stack<Checkpoint>();
        foreach (var checkPoint in checkpointsArray)
        {
            checkpoints.Push(checkPoint);
            checkPoint.SetAsFuture();
        }
    }

    private void SetFirstTargetCheckpoint()
    {
        checkpoints.Peek().SetAsTarget();
    }

    public void CurrentTargetCheckpointPassed()
    {
        if (checkpoints.Count > 1)
        {
            checkpoints.Pop();
            checkpoints.Peek().SetAsTarget();
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
