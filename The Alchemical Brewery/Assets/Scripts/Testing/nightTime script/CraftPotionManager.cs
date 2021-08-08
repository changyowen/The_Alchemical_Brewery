using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftPotionManager : MonoBehaviour
{
    public static CraftPotionManager Instance { get; private set; }

    public GameObject craftButton;
    public GameObject craftingAnimation_obj;
    public GameObject blackScreen_obj;

    public List<int> potIngredientList = new List<int>(); //List for Ingredient in pot

    public PotionData potionDataHolder = null;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        CraftButtonHandler();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SaveManager.Load();
        }
    }

    void CraftButtonHandler()
    {
        //activate when pot is full
        if (potIngredientList.Count == 4)
        {
            craftButton.SetActive(true);
        }
        else //deactivate when pot is not full
        {
            craftButton.SetActive(false);
        }
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
        if(potIngredientList.Count > buttonIndex)
        {
            potIngredientList.RemoveAt(buttonIndex);
        }
    }

    public void ResetPotIngredientList()
    {
        potIngredientList.Clear();
    }

    public void StartCraftPotion()
    {
        //reset current potion data holder
        potionDataHolder = null;

        //Start potion calculation
        int effectiveScore = CraftPotionCalculation.Instance.PotionEffectiveCalculation(potIngredientList);
       
        //effective score not pass
        if(effectiveScore <= -25 || effectiveScore > 25)
        {
            //reset pot ingredient list
            ResetPotIngredientList();
        }
        else //effective score pass
        {
            List<int> sortedPotIngredientList = new List<int>(potIngredientList);

            //create new potion data
            PotionData newPotionData = new PotionData();
            potionDataHolder = newPotionData;

            //assign potion formular into potion data 
            potionDataHolder.AssignListIntoFormular(sortedPotIngredientList);
            //assign score on potion data
            potionDataHolder.potionEffectiveScore = effectiveScore;
            //calculate element of potion
            potionDataHolder.potionElement = CraftPotionCalculation.Instance.ElementCalculation(sortedPotIngredientList);
            //determinne potion usage
            potionDataHolder.potionUsage = CraftPotionCalculation.Instance.PotionUsageDetermine(sortedPotIngredientList);

            //Start crafting animation
            StartingCraftingAnimation();
        }
    }

    void StartingCraftingAnimation()
    {
        //set active black screen
        blackScreen_obj.SetActive(true);
        //set active crafting animation
        craftingAnimation_obj.SetActive(true);
        //assign img
        CraftPotionCalculation.Instance.AssignCraftingAnimationSprite(potIngredientList);
    }

    public void EndingCraftingAnimation()
    {
        //Start mini game
        FishingMiniGame.Instance.StartMiniGame();
        //deactivate crafting animation
        craftingAnimation_obj.SetActive(false);
    }

    public void CraftPotionResult(PotionData newPotionData)
    {
        //deactivate black screen
        blackScreen_obj.SetActive(false);
        //assign back potion data
        potionDataHolder = newPotionData;

        //adding into player profile
        PlayerProfile.acquiredPotion.Add(potionDataHolder);
        SaveManager.Save();
    }

    public void StartMiniGame()
    {

    }
}
