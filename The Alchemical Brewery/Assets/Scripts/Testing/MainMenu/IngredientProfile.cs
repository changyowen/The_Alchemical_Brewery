using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IngredientProfile 
{
    public int ingredientIndex;
    public bool unlocked = false;

    public void UnlockThisIngredient()
    {
        unlocked = true;
        int crushedIndex = ingredientIndex + 20;
        int extractedIndex = ingredientIndex + 40;
        if((crushedIndex - 1) < 40)
        {
            PlayerProfile.ingredientProfile[crushedIndex - 1].unlocked = true;
        }
        if ((extractedIndex - 1) < 60)
        {
            Debug.Log(ingredientIndex);
            PlayerProfile.ingredientProfile[extractedIndex - 1].unlocked = true;
        }
        
    }
}
