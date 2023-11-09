using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Racer : MonoBehaviour
{
    public int RacerID { get; set; }
    
    public AudioSource AudioSource { get; set; }
}
