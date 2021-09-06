using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftPotionManager : MonoBehaviour
{
    public static CraftPotionManager Instance { get; private set; }

    public ScriptableObjectHolder so_Holder;
    public ResultPanelHandler resultPanelHandler;

    public GameObject craftButton;
    public GameObject craftingAnimation_obj;
    public GameObject blackScreen_obj;
    public GameObject craftingUI_obj;
    public GameObject craftResultPanel_obj;
    public Transform craftingMain_transform;

    public List<int> potIngredientList = new List<int>(); //List for Ingredient in pot

    [System.NonSerialized] public bool getBackPreviousPotion = false; 
    public PotionData potionDataHolder = null;

    public bool potFull = false;
    private bool getPreviousFormular = false;
    private int previousFormularIndex = 0;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        CraftButtonHandler();
        CheckPotFull();
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

    void CheckPotFull()
    {
        if (!potFull)
        {
            if (potIngredientList.Count == 4)
            {
                potFull = true;

                int formularIndex = 0;
                getPreviousFormular = CheckIfGettingPreviousFormular(out formularIndex);

                if(getPreviousFormular)
                {
                    resultPanelHandler.AssignPreviousPotion(formularIndex);
                    previousFormularIndex = formularIndex;
                }
                else
                {
                    resultPanelHandler.AssignUnknownPotion(potIngredientList);
                    previousFormularIndex = formularIndex;
                }
            }
        }
        else
        {
            if (potIngredientList.Count < 4)
            {
                resultPanelHandler.AssignNullFormular();
                previousFormularIndex = 0;
                potFull = false;
            }
        }
    }

    bool CheckIfGettingPreviousFormular(out int formularIndex)
    {
        for (int i = 0; i < PlayerProfile.acquiredPotion.Count; i++)
        {
            List<int> tempList = new List<int>(PlayerProfile.acquiredPotion[i].potionFormular);
            bool getSameFormular = CraftPotionCalculation.Instance.CompareLists(tempList, potIngredientList);

            if (getSameFormular)
            {
                formularIndex = i;
                return true;
            }
        }
        formularIndex = 0;
        return false;
    }

    public void AddIngredient(int ingredientIndex)
    {
        //check not more than 4
        if (potIngredientList.Count < 4)
        {
            int ingLeft = PlayerProfile.shopProfile.ingredientPurchased[ingredientIndex + IngredientPanel.Instance.refinementValue - 1];
            if (ingLeft > 0)
            {
                //Add into potIngredientList
                potIngredientList.Add(ingredientIndex + IngredientPanel.Instance.refinementValue);
                //Subtract from player ingredient purchased total;
                PlayerProfile.shopProfile.ingredientPurchased[ingredientIndex + IngredientPanel.Instance.refinementValue - 1] -= 1;
            }
        }
    }

    public void RemoveIngredient(int buttonIndex)
    {
        //remove from ingredientList
        if(potIngredientList.Count > buttonIndex)
        {
            //Return to player ingredient purchased total;
            PlayerProfile.shopProfile.ingredientPurchased[potIngredientList[buttonIndex] - 1] += 1;
            //remove from pot ingredient list
            potIngredientList.RemoveAt(buttonIndex);
        }
    }

    public void ActivateCloseButton(Transform _closebutton)
    {
        _closebutton.gameObject.SetActive(true);
    }

    public void DeactivateCloseButton(Transform _closebutton)
    {
        _closebutton.gameObject.SetActive(false);
    }

    public void ResetPotIngredientList(bool returnIngredient)
    {
        if(potIngredientList.Count > 0) //if pot has any ingredient
        {
            if (returnIngredient)
            {
                //return all to ingredient purchased total
                for (int i = 0; i < potIngredientList.Count; i++)
                {
                    PlayerProfile.shopProfile.ingredientPurchased[potIngredientList[i] - 1] += 1;
                }
                //clear pot
                potIngredientList.Clear();
            }
            else
            {
                //clear pot
                potIngredientList.Clear();
            }
        }
    }

    public void StartCraftPotion()
    {
        //reset current potion data holder
        potionDataHolder = null;

        //Start potion calculation
        int effectiveScore = CraftPotionCalculation.Instance.PotionEffectiveCalculation(potIngredientList);

        //effective score not pass
        if (effectiveScore >= 10000000f)
        {
            //reset pot ingredient list
            ResetPotIngredientList(false);
        }
        else //effective score pass
        {
            List<int> sortedPotIngredientList = new List<int>(potIngredientList);

            if (getPreviousFormular) //if its previous formular
            {
                //get back previous formular
                potionDataHolder = PlayerProfile.acquiredPotion[previousFormularIndex];
                getBackPreviousPotion = true;
            }
            else //if new formular
            {
                //create new potion data
                PotionData newPotionData = new PotionData();
                potionDataHolder = newPotionData;
            }

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
        //deactivate crafting ui
        craftingUI_obj.SetActive(false);
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
        //deactive black screen
        blackScreen_obj.SetActive(false);
        //deactivate crafting animation
        craftingAnimation_obj.SetActive(false);
    }

    public void CraftPotionResult(PotionData newPotionData)
    {
        //reactivate crafting ui
        craftingUI_obj.SetActive(true);
        //assign back potion data
        potionDataHolder = newPotionData;

        //adding into player profile [IF not prefious potion]
        if(!getBackPreviousPotion)
        {
            PlayerProfile.acquiredPotion.Add(potionDataHolder);

            //show result
            GameObject craftResultPanel = Instantiate(craftResultPanel_obj, Vector3.zero, Quaternion.identity);
            craftResultPanel.transform.SetParent(craftingMain_transform, false);
            ResultPanelHandler panelScript = craftResultPanel.GetComponent<ResultPanelHandler>();
            panelScript.potionData = PlayerProfile.acquiredPotion[PlayerProfile.acquiredPotion.Count - 1];
            panelScript.AssignPreviousPotion(PlayerProfile.acquiredPotion.Count - 1);


            SaveManager.Save();
        }
        else
        {
            //show result
            GameObject craftResultPanel = Instantiate(craftResultPanel_obj, Vector3.zero, Quaternion.identity);
            craftResultPanel.transform.SetParent(craftingMain_transform, false);
            ResultPanelHandler panelScript = craftResultPanel.GetComponent<ResultPanelHandler>();
            panelScript.potionData = potionDataHolder;
            int _position = PlayerProfile.acquiredPotion.IndexOf(potionDataHolder);
            panelScript.AssignPreviousPotion(_position);
            SaveManager.Save();
        }
        getBackPreviousPotion = false; //reset this bool
    }

    public void StartMiniGame()
    {

    }
}
