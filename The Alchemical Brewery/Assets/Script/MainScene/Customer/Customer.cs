using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Customer", menuName = "Customer")]
public class Customer : ScriptableObject
{
    public int customerIndex;
    public string Name;
    public int preferablePotion;
    public float waitingTime;
    public GameObject customertype;
}
