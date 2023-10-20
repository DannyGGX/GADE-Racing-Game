using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AIRacerFactory : Factory
{
    public AI_StatsSO RacerStats;
    
    public override void ApplyStats()
    {
        AI_Racer aiRacer = (AI_Racer)BaseRacerPrefab;
        aiRacer.RacerStats = RacerStats;
        aiRacer.enabled = true;
    }
}
