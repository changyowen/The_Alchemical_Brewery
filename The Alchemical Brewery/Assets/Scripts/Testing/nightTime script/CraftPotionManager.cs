using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftPotionManager : MonoBehaviour
{
    public static CraftPotionManager Instance { get; private set; }

    public List<int> potIngredientList; //List for Ingredient in pot

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        potIngredientList = new List<int>();
    }

    public void AddIngredient(int ingredientIndex)
    {
        //check not more than 4
        if (potIngredientList.Count < 4)
        {
            //Add into potIngredientList
            potIngredientList.Add(ingredientIndex);

            //get button index
            int buttonIndex = potIngredientList.Count - 1;
        }
    }

    public void RemoveIngredient(int buttonIndex)
    {
        //remove from ingredientList
        potIngredientList.RemoveAt(buttonIndex);
    }

    public void StartCraftPotion()
    {
        //Start potion calculation
        int totalScore = CraftPotionCalculation.Instance.PotionCalculate(potIngredientList);

        //score not pass
        if(totalScore <= 25 || totalScore > 25)
        {
            
        }
        else //score pass
        {

        }
        //Start crafting animation

    }

    public void StartMiniGame()
    {

    }
}
