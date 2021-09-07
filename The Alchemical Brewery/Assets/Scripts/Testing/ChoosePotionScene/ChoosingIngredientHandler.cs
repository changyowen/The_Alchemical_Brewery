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

    }
}
