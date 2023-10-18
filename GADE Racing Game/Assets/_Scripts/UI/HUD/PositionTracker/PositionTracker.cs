using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionTracker : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform[] _aiRacers;

    public void Change(object racerId)
    {
        this.Log(racerId);
    }
}
