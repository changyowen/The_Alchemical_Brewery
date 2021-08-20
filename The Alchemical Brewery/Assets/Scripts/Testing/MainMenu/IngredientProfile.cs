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
    }
}
