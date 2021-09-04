using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public enum RefinementStage
{
    Normal = 0,
    Crushed = 1,
    Extract = 2
}

public class IngredientPanel : MonoBehaviour
{
    public static IngredientPanel Instance { get; private set; }

    public ScriptableObjectHolder SO_holder;
    public GameObject[] ingredientButton_obj;

    public RefinementStage panelRefinementStage = RefinementStage.Normal;
    public int refinementValue
    {
        get
        {
            return (int)panelRefinementStage * 20;
        }
    }

    void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        UpdateIngredientTotal();
    }

    void UpdateIngredientTotal()
    {
        for (int i = 0; i < ingredientButton_obj.Length; i++)
        {
            Text ingredientTotal_text = ingredientButton_obj[i].transform.GetChild(1).GetComponent<Text>();
            ingredientTotal_text.text = "" + PlayerProfile.shopProfile.ingredientPurchased[i + refinementValue];
        }
    }

    public void AssignButtonData()
    {
        for (int i = 0; i < ingredientButton_obj.Length; i++)
        {
            //assign image
            Image buttonImage = ingredientButton_obj[i].transform.GetChild(0).GetComponent<Image>();
            buttonImage.sprite = SO_holder.ingredientSO[i + refinementValue + 1].ingredientSprite;
            //lock or unlock
            GameObject lockImage_obj = ingredientButton_obj[i].transform.GetChild(3).gameObject;
            bool ingredientUnlocked = PlayerProfile.ingredientProfile[i + refinementValue].unlocked;
            if(ingredientUnlocked)
            {
                ingredientButton_obj[i].GetComponent<Button>().interactable = true;
                lockImage_obj.SetActive(false);
            }
            else
            {
                ingredientButton_obj[i].GetComponent<Button>().interactable = false;
                lockImage_obj.SetActive(true);
            }
            //assign tooltips data
            GameObject tooltipsObj = ingredientButton_obj[i].transform.GetChild(2).gameObject;
            IngredientData currentIngredientData = SO_holder.ingredientSO[i + refinementValue + 1];
            AssignToolTipsData(tooltipsObj, currentIngredientData);
        }
    }

    void AssignToolTipsData(GameObject _toolTipsObj, IngredientData _currentIngredientData)
    {
        TooltipsIngredientPanel tooltipsIngredientPanel = _toolTipsObj.GetComponent<TooltipsIngredientPanel>();
        tooltipsIngredientPanel.AssignTooltipsData(_currentIngredientData);
    }

    public void ChangeRefinementStage(int _refinementStage)
    {
        switch(_refinementStage)
        {
            case 0:
                {
                    panelRefinementStage = RefinementStage.Normal;
                    break;
                }
            case 1:
                {
                    panelRefinementStage = RefinementStage.Crushed;
                    break;
                }
            case 2:
                {
                    panelRefinementStage = RefinementStage.Extract;
                    break;
                }
        }
        //update button data
        AssignButtonData();
    }

    public void ActivateTooltips(TooltipsIngredientPanel _tooltips)
    {
        //get index
        int _ingIndex = _tooltips.buttonIndex + refinementValue +1;
        //check unlock state
        bool _unlocked = PlayerProfile.ingredientProfile[_ingIndex - 1].unlocked;

        if(_unlocked)
        {
            _tooltips.gameObject.SetActive(true);
        }
        else
        {
            _tooltips.gameObject.SetActive(false); 
        }
    }

    public void DeactivateTooltips(TooltipsIngredientPanel _tooltips)
    {
        _tooltips.gameObject.SetActive(false);
    }
}
