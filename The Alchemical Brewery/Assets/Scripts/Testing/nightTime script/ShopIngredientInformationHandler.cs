using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopIngredientInformationHandler : MonoBehaviour
{
    public ScriptableObjectHolder SO_holder;

    public GameObject[] normalIngredientButton;
    public GameObject[] regionalIngredientButton;

    int shopRefinementState = 0;
    int refinementValue
    {
        get
        {
            return (int)shopRefinementState * 20;
        }
    }
    int regionalValue
    {
        get
        {
            return (int)PlayerProfile.stageChosen * 5;
        }
    }

    private void OnEnable()
    {
        UpdateAllButtons();
    }

    void UpdateAllButtons()
    {
        Canvas.ForceUpdateCanvases();
        UpdateNormalIngredientButton();
        UpdateRegionalIngredientButton();
    }

    void UpdateNormalIngredientButton()
    {
        for (int i = 0; i < normalIngredientButton.Length; i++)
        {
            //get ingredient data & profile
            IngredientData _ingredientData = SO_holder.ingredientSO[i + 1 + refinementValue];
            IngredientProfile _ingredientProfile = PlayerProfile.ingredientProfile[i + refinementValue];

            //update button image
            normalIngredientButton[i].transform.GetChild(0).GetComponent<Image>().sprite = _ingredientData.ingredientSprite;

            //update price
            normalIngredientButton[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "$ " + _ingredientData.ingredientPrice;

            //update locked/unlocked
            bool _unlocked = _ingredientProfile.unlocked;
            if(_unlocked)
            {
                normalIngredientButton[i].GetComponent<Button>().interactable = true;
                normalIngredientButton[i].transform.GetChild(2).gameObject.SetActive(false);
            }
            else
            {
                normalIngredientButton[i].GetComponent<Button>().interactable = false;
                normalIngredientButton[i].transform.GetChild(2).gameObject.SetActive(true);
            }

            //update tooltips
            TooltipsIngredientPanel tooltipsScript = normalIngredientButton[i].transform.GetChild(4).GetComponent<TooltipsIngredientPanel>();
            tooltipsScript.AssignTooltipsData(_ingredientData);
        }
    }

    void UpdateRegionalIngredientButton()
    {
        for (int i = 0; i < regionalIngredientButton.Length; i++)
        {
            //get ingredient data & profile
            IngredientData _ingredientData = SO_holder.ingredientSO[i + 1 + regionalValue + refinementValue];
            IngredientProfile _ingredientProfile = PlayerProfile.ingredientProfile[i + regionalValue + refinementValue];

            //update button image
            regionalIngredientButton[i].transform.GetChild(0).GetComponent<Image>().sprite = _ingredientData.ingredientSprite;

            //update price
            regionalIngredientButton[i].transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "$ " + _ingredientData.ingredientPrice;

            //update locked/unlocked
            bool _unlocked = _ingredientProfile.unlocked;
            if (_unlocked)
            {
                regionalIngredientButton[i].GetComponent<Button>().interactable = true;
                regionalIngredientButton[i].transform.GetChild(2).gameObject.SetActive(false);
            }
            else
            {
                regionalIngredientButton[i].GetComponent<Button>().interactable = false;
                regionalIngredientButton[i].transform.GetChild(2).gameObject.SetActive(true);
            }

            //update tooltips
            TooltipsIngredientPanel tooltipsScript = regionalIngredientButton[i].transform.GetChild(4).GetComponent<TooltipsIngredientPanel>();
            tooltipsScript.AssignTooltipsData(_ingredientData);
        }
    }

    public void ChangeRefinementStatus(int buttonIndex)
    {
        shopRefinementState = buttonIndex;
        UpdateAllButtons();
    }

    public void PurchaseCommonIngredient(int buttonIndex)
    {
        PlayerProfile.shopProfile.ingredientPurchased[buttonIndex + refinementValue]++;
    }

    public void PurchaseRegionalIngredient(int buttonIndex)
    {
        PlayerProfile.shopProfile.ingredientPurchased[buttonIndex + refinementValue + regionalValue]++;
    }
}
