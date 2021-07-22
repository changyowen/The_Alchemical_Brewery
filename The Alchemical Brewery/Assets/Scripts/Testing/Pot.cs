using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot 
{
    public int potPotionHolder; 
    public List<int> potIngredientHolderList = new List<int>();

    public bool potBoiling = false;

    //reset all pot holder
    public void ResetPotHolder()
    {
        potPotionHolder = 0;
        potIngredientHolderList.Clear();
    }

    public int TakePotion()
    {
        int takenPotionIndex = potPotionHolder;
        //player get potion
        //reset pot potion holder
        potPotionHolder = 0;

        return takenPotionIndex;
    }

    public void PutIngredient(int ingredientIndex)
    {
        potIngredientHolderList.Add(ingredientIndex);
    }

    public IEnumerator CraftingPotion()
    {
        ResetPotHolder();
        yield return new WaitForSeconds(5f);
        potPotionHolder = 1;
    }
}
