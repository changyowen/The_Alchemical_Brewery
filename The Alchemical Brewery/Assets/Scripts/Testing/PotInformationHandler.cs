using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotInformationHandler : MonoBehaviour
{
    public int potIndex = 0;

    public int potPotionHolder = -1;
    public List<int> potIngredientHolderList = new List<int>();

    public bool potBoiling = false;

    public GameObject spawnIngredient_obj;
    public Transform ingredientSpawn;

    //reset all pot holder
    public void ResetPotHolder()
    {
        potPotionHolder = -1;
        potIngredientHolderList.Clear();
    }
}
