using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class RacerFactory : MonoBehaviour
{
    public Transform SpawnPosition;
    public Racer BaseRacerPrefab;
    
    public Racer CreateRacer()
    {
        BaseRacerPrefab = Instantiate(BaseRacerPrefab , SpawnPosition.position, SpawnPosition.rotation);
        return BaseRacerPrefab;
    }

    public abstract void ApplyStats();

}
