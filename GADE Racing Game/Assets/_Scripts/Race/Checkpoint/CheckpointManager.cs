using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private Checkpoint[] checkpointsArray;
    private Stack<Checkpoint> checkpointsStack;

    [SerializeField] private EventSenderSO onEndLap;


    private void Start()
    {
        StartLap();
    }
    public void StartLap()
    {
        PopulateStack();
        SetFirstTargetCheckpoint();
    }
    private void PopulateStack() // Load checkpoints from the array into the stack
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
        }
        else
        {
            onEndLap.Invoke(); // To RaceManager
        }
    }
}
