using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// For managing checkpoints on a branching racetrack.
/// There should be only 1 checkpoint manager in the scene.
/// </summary>
public class BranchingCheckpointManager : MonoBehaviour
{
    [SerializeField, Tooltip("Need to be in order")] 
    private CheckpointStage[] checkpointStages;
    
    [SerializeField] private EventSenderSO onEndLap;
    
    private Stack<CheckpointStage> checkpointStagesStack;
    private Checkpoint[] currentStageCheckpoints;

    private void Start()
    {
        StartLap();
    }

    public void StartLap()
    {
        PopulateStack();
        SetFirstTargetCheckpoint();
    }

    private void SetFirstTargetCheckpoint()
    {
        checkpointStagesStack.Peek().Checkpoints[0].SetAsTarget(); // there must only be 1 first checkpoint.
    }

    private void PopulateStack()
    {
        checkpointStagesStack = new Stack<CheckpointStage>();
        for (int stage = checkpointStages.Length - 1; stage >= 0; stage--) // iterate backwards because of stack first in last out principle
        {
            // Set each checkpoint in the current stage to a future checkpoint
            for (int checkpoint = 0; checkpoint < checkpointStages[stage].Checkpoints.Length; checkpoint++)
            {
                checkpointStages[stage].Checkpoints[checkpoint].SetAsFuture();
            }
            checkpointStagesStack.Push(checkpointStages[stage]);
        }
    }

    public void CurrentTargetCheckpointPassed()
    {
        if (checkpointStagesStack.Count > 1)
        {
            // Set all checkpoints on the same stage as the passed checkpoint to passed.
            currentStageCheckpoints = checkpointStagesStack.Peek().Checkpoints;
            for (int i = 0; i < currentStageCheckpoints.Length; i++)
            {
                currentStageCheckpoints[i].SetAsPassed();
            }
            
            checkpointStagesStack.Pop();
            
            // Set the next stage of checkpoints as target.
            currentStageCheckpoints = checkpointStagesStack.Peek().Checkpoints;
            for (int i = 0; i < currentStageCheckpoints.Length; i++)
            {
                currentStageCheckpoints[i].SetAsTarget();
            }
        }
        else
        {
            onEndLap.Invoke(); // To RaceManager
        }
    }
}

[System.Serializable]
public struct CheckpointStage
{
    /// <summary>
    /// Array will only have one checkpoint if that checkpoint is on a part of track that is not branching.
    /// </summary>
    public Checkpoint[] Checkpoints;
}