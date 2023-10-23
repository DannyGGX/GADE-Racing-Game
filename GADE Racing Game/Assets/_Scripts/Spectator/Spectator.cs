using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controller to be put on each spectator or maybe group of spectators
/// </summary>
public class Spectator : MonoBehaviour
{
    private Animator animator;
    private const string animatorParameterName = "State";
    private static readonly int animatorParameter = Animator.StringToHash(animatorParameterName);
    
    private SpectatorState currentState;

    private Transform mainCameraTransform;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        currentState = new DefaultState();
    }

    private void Start()
    {
        mainCameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        BillboardSprite();
    }
    private void BillboardSprite()
    {
        var position = transform.position;
        Vector3 targetPosition = position + mainCameraTransform.forward;

        targetPosition.y = position.y;
        
        transform.LookAt(targetPosition, Vector3.up);
    }

    public void SetNextState()
    {
        currentState = currentState.Transition();
        SetAnimator(currentState.StateID);
        StartCoroutine(TickState(currentState.GetStateDuration()));
    }

    public void SetCheerState()
    {
        currentState = new CheerState();
        SetAnimator(currentState.StateID);
        StopCoroutine(nameof(TickState));
        StartCoroutine(TickState(currentState.GetStateDuration()));
    }

    private void SetAnimator(int stateID)
    {
        animator.SetInteger(animatorParameter, stateID);
    }

    private IEnumerator TickState(float durationTime)
    {
        yield return new WaitForSeconds(durationTime);
        SetNextState();
    }
}
