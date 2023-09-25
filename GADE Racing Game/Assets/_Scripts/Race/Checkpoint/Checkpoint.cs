using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Material targetMaterial;
    [SerializeField] private Material futureMaterial;
    [SerializeField] private Material passedMaterial;
    private MeshRenderer mesh;
    
    private enum CheckpointStates
    {
        Future,
        Target,
        Passed
    }
    private CheckpointStates currentCheckpointState;
    
    [Space]
    [SerializeField] private EventSenderSO onTargetCheckpointPassed;

    private void Awake() // Called before CheckpointManager sets the checkpoints
    {
        mesh = gameObject.GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentCheckpointState == CheckpointStates.Target && other.CompareTag("Player"))
        {
            SetAsPassed();
            onTargetCheckpointPassed.Invoke();
        }
    }
    public void SetAsTarget()
    {
        currentCheckpointState = CheckpointStates.Target;
        mesh.material = targetMaterial;
    }

    public void SetAsFuture() // Called at start of the lap
    {
        currentCheckpointState = CheckpointStates.Future;
        mesh.material = futureMaterial;
    }

    private void SetAsPassed()
    {
        currentCheckpointState = CheckpointStates.Passed;
        mesh.material = passedMaterial;
    }
}
