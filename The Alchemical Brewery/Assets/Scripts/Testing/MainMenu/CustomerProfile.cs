using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CustomerProfile 
{
    public int customerIndex;

    public bool unlocked = false;
    [Range(1, 6)] public int customerLevel = 1;
    public float customerExperience;
}
