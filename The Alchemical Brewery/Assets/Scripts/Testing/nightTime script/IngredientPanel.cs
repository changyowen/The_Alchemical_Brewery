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
            ingredientTotal_text.text = "" + PlayerProfile.shopProfile.ingredientPurchased[i];
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
            GameObject lockImage_obj = ingredientButton_obj[i].transform.GetChild(2).gameObject;
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
        }
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
}
