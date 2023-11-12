using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTracker : MonoBehaviour
{
    private TrackingEntity[] aiRacers;
    private TrackingEntity player;

    private int playerPositionNumber;

    [SerializeField] private PositionTrackerUI ui;

    private void Awake()
    {
        EventManager.OnPlayerWaypointPassed.Subscribe(PlayerWaypointPassed);
        EventManager.OnAIWaypointPassed.Subscribe(AIWaypointPassed);
        EventManager.OnSendRacerReferences.Subscribe(PopulateTrackerEntities);
        this.Log("PositionTracker Awake() called");
    }
    private void OnDisable()
    {
        EventManager.OnPlayerWaypointPassed.Unsubscribe(PlayerWaypointPassed);
        EventManager.OnAIWaypointPassed.Unsubscribe(AIWaypointPassed);
        EventManager.OnSendRacerReferences.Unsubscribe(PopulateTrackerEntities);
    }

    private void Start()
    {
        ui.Hide();
        
        GetFirstWaypoint();
    }
    
    private void FixedUpdate()
    {
        DeterminePlayerPosition();
        ui.UpdateUI(playerPositionNumber);
    }

    private void PopulateTrackerEntities(Racer[] racers)
    {
        List<TrackingEntity> aiRacers = new List<TrackingEntity>();
        
        foreach (var racer in racers)
        {
            if (racer is CarController2)
            {
                player = new TrackingEntity(racer.RacerID, racer.transform);
            }
            else
            {
                aiRacers.Add(new TrackingEntity(racer.RacerID, racer.transform));
            }
        }
        
        this.aiRacers = aiRacers.ToArray();
        
    }

    private void GetFirstWaypoint()
    {
        player.TargetWaypoint = WaypointManager.Instance.GetWaypointLinkedListHead();

        foreach (var aiRacer in aiRacers)
        {
            aiRacer.TargetWaypoint = WaypointManager.Instance.GetWaypointLinkedListHead();
        }
    }

    private void PlayerWaypointPassed()
    {
        RacerPassedWaypoint(player);
    }

    private void AIWaypointPassed(int racerID)
    {
        RacerPassedWaypoint(GetAIRacer(racerID));
    }

    private void RacerPassedWaypoint(TrackingEntity racer)
    {
        racer.PassedWaypoints++;
        racer.TargetWaypoint = racer.TargetWaypoint.NextNode;
    }

    private TrackingEntity GetAIRacer(int racerID)
    {
        foreach(var aiRacer in aiRacers)
        {
            if (aiRacer.RacerID == racerID)
            {
                return aiRacer;
            }
        }
        throw new NullReferenceException();
    }

    private void DeterminePlayerPosition()
    {
        player.DistanceToTargetWaypoint = GetDistanceToTargetWaypoint(player);
        foreach (var aiRacer in GetAllAIWithSameTargetAsPlayer())
        {
            aiRacer.DistanceToTargetWaypoint = GetDistanceToTargetWaypoint(aiRacer);
            CompareDistanceWithPlayer(aiRacer);
        }

        playerPositionNumber = GetPlayerPosition();
    }

    private int GetPlayerPosition()
    {
        int numberOfAIAheadOfPlayer = 0;
        foreach (var aiRacer in aiRacers)
        {
            if (aiRacer.AheadOfPlayer)
            {
                numberOfAIAheadOfPlayer++;
            }
        }
        return numberOfAIAheadOfPlayer + 1;
    }

    private TrackingEntity[] GetAllAIWithSameTargetAsPlayer()
    {
        return Array.FindAll(aiRacers, x => x.PassedWaypoints == player.PassedWaypoints);
    }

    private float GetDistanceToTargetWaypoint(TrackingEntity racer)
    {
        return Vector3.Distance(racer.RacerTransform.position, racer.TargetWaypoint.Data.transform.position);
    }

    private void CompareDistanceWithPlayer(TrackingEntity aiRacer)
    {
        aiRacer.AheadOfPlayer = aiRacer.DistanceToTargetWaypoint < player.DistanceToTargetWaypoint;
    }

    public void ShowUI() => ui.Show(); // called when race starts

    public void DisablePositionChange() => this.enabled = false; // called when race is complete or lost
}
