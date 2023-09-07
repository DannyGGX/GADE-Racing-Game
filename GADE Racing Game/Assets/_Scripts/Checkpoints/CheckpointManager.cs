using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private Checkpoint[] checkpointsArray;
    private Stack<Checkpoint> checkpoints;


    private void Awake()
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
    }
}
