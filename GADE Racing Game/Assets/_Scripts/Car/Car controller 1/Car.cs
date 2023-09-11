using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Car
{
    public CarStatsSO CarStats;
    public FrontWheel[] FrontWheels = new FrontWheel[2];
    public BackWheel[] BackWheels = new BackWheel[2];
}
