using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Material targetMaterial;
    [SerializeField] private Material futureMaterial;
    [SerializeField] private Material passedMaterial;
    private string targetTag = "TargetCheckpoint";
    private string futureTag = "FutureCheckpoint";
    private string passedTag = "PassedCheckpoint";
    private MeshRenderer mesh;
    [Space]
    [SerializeField] private EventSenderSO onTargetCheckpointPassed;

    private void Awake()
    {
        mesh = gameObject.GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameObject.CompareTag(targetTag))
        {
            SetAsPassed();
            this.Log("Player passed checkpoint");
            onTargetCheckpointPassed.Invoke();
        }
    }
    public void SetAsTarget()
    {
        gameObject.tag = targetTag;
        mesh.material = targetMaterial;
    }

    public void SetAsFuture() //Called when passed whole lap or at start
    {
        gameObject.tag = futureTag;
        mesh.material = futureMaterial;
    }

    private void SetAsPassed()
    {
        mesh.material = passedMaterial;
        gameObject.tag = passedTag;
    }
}
