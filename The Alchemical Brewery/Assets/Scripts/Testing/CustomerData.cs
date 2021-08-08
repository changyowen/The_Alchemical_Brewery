using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Customer", menuName = "Customer Data")]
public class CustomerData : ScriptableObject
{
    public int customerIndex;
    public string customerName;
    public Sprite customerSprite;

    public List<Element> preferElement;
    public List<PotionUsage> preferPotionUsage;

    public float baseAppearRate;
    public float levelingAppearRate;

    public float[] levelingExperience;
}
