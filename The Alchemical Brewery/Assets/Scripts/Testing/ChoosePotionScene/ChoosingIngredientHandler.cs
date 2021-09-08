using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoosingIngredientHandler : MonoBehaviour
{
    public static ChoosingIngredientHandler Instance { get; private set; }

    public ScriptableObjectHolder SO_holder;

    public GameObject[] ingredientButtons;
    public GameObject[] miniMapIngredientIcon;

    public List<IngredientData> chosenIngredientForGameplay = new List<IngredientData>();

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        UpdateMinimapIcon();
    }

    public void UpdateIngredientButtons()
    {
        for (int i = 0; i < ingredientButtons.Length; i++)
        {
            if (ChosenPotionHandler.Instance.choosingIngredientMode)
            {
                if(i < ChosenPotionHandler.Instance.chosenIngredientList.Count)
                {
                    //set active button
                    ingredientButtons[i].SetActive(true);
                    //set button image
                    IngredientData _ingredientData = ChosenPotionHandler.Instance.chosenIngredientList[i];
                    ingredientButtons[i].transform.GetChild(0).GetComponent<Image>().sprite = _ingredientData.ingredientSprite;
                }
                else
                {
                    ingredientButtons[i].SetActive(false); //deactive all button
                }
            }
            else
            {
                ingredientButtons[i].SetActive(false); //deactive all button
            }
        }
    }

    public void UpdateMinimapIcon()
    {
        for (int i = 0; i < miniMapIngredientIcon.Length; i++)
        {
            if(ChosenPotionHandler.Instance.choosingIngredientMode)
            {
                if (i < chosenIngredientForGameplay.Count)
                {
                    miniMapIngredientIcon[i].transform.GetChild(0).GetComponent<Image>().sprite = chosenIngredientForGameplay[i].ingredientSprite;
                }
                else
                {
                    miniMapIngredientIcon[i].transform.GetChild(0).GetComponent<Image>().sprite = SO_holder.transparentSprite;
                }
            }
            else
            {
                miniMapIngredientIcon[i].transform.GetChild(0).GetComponent<Image>().sprite = SO_holder.transparentSprite;
            }
        }
    }

    public void ChooseIngredient(int buttonIndex)
    {
        IngredientData _ingredientData = ChosenPotionHandler.Instance.chosenIngredientList[buttonIndex];

        if (chosenIngredientForGameplay.Count < 10)
        {
            chosenIngredientForGameplay.Add(_ingredientData);
        }
    }

    public void ReturnChosingPotion()
    {
        if(ChosenPotionHandler.Instance.choosingIngredientMode)
        {
            ChosenPotionHandler.Instance.choosingIngredientMode = false;
            chosenIngredientForGameplay.Clear();
            UpdateIngredientButtons();
        }
    }

    public void ConfirmStartGame()
    {
        if(chosenIngredientForGameplay.Count < 10) //player didnt filled all the slot
        {
            NotificationSystem.Instance.SendPopOutNotification("You must place place 10 ingredients to start game!");
        }
        else //player filled all the slot
        {
            //check if player take all ingredient acquired
            bool _getAllIng = CheckListContainList(chosenIngredientForGameplay, ChosenPotionHandler.Instance.chosenIngredientList);

            if(!_getAllIng) //player didnt choose all ingredient needed
            {
                NotificationSystem.Instance.SendPopOutNotification("You didnt't choose all ingredient required for your menus!");
            }
            else
            {
                //start game
                ClearAllChosenIngredientForGameplay();
                ChoosePotionSceneManager.Instance.OpenShop();
            }
        }
    }

    bool CheckListContainList(List<IngredientData> listA, List<IngredientData> listB)
    {
        //check if listB never bigger than listA
        if (listB.Count > listA.Count)
            return false;

        for (int i = 0; i < listB.Count; i++)
        {
            if(!listA.Contains(listB[i]))
            {
                return false;
            }
        }
        return true;
    }

    public void ClearAllChosenIngredientForGameplay()
    {
        chosenIngredientForGameplay.Clear();
    }
}
