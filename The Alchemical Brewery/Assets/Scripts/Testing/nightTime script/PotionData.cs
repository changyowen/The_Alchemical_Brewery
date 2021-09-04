using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PotionUsage
{
    Damaging = -1, Neutral = 0, Healing = 1
}

[System.Serializable]
public class PotionData 
{
    public int potionSpriteIndex = 0; 
    public string potionName;
    public int[] potionFormular;

    public int potionEffectiveScore;
    [Range(0, 100)] public float potionQuality;
    public List<Element> potionElement = new List<Element>();
    public PotionUsage potionUsage;

    public void AssignListIntoFormular(List<int> formularList)
    {
        //declare array
        potionFormular = new int[formularList.Count];

        //assign list into array
        for (int i = 0; i < formularList.Count; i++)
        {
            potionFormular[i] = formularList[i];
        }
    }
}
