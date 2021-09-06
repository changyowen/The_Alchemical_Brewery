using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlmanacIngredientInformationHandler : MonoBehaviour
{
    public ScriptableObjectHolder SO_holder;

    [Header("Button Panel Reference")]
    public GameObject[] ingredientButton_obj;

    [Header("Information Reference")]
    public Image ingredientImage;
    public Text ingredientName;
    public TextMeshProUGUI ingredientDescription;
    public Image ingredientElementImage;
    public Image[] valueStars;
    public Image[] effectivenessStars;
    public Text ingredientPrice;

    [Header("Internal Data")]
    int currentIngredientIndex = 0;

    private void Update()
    {

    }

    public void UpdateButtonData()
    {
        for (int i = 0; i < ingredientButton_obj.Length; i++)
        {
            IngredientData ingredientData = SO_holder.ingredientSO[i + 1];
            IngredientProfile ingredientProfile = PlayerProfile.ingredientProfile[i];

            //update button image
            ingredientButton_obj[i].transform.GetChild(0).GetComponent<Image>().sprite = ingredientData.ingredientSprite;

            //update locked/unlocked
            Debug.Log(i);
            bool _unlocked = ingredientProfile.unlocked;
            if(_unlocked)
            {
                ingredientButton_obj[i].GetComponent<Button>().interactable = true;
                ingredientButton_obj[i].transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                ingredientButton_obj[i].GetComponent<Button>().interactable = false;
                ingredientButton_obj[i].transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }

    public void UpdateIngredientInformation()
    {
        //get ingredient SO
        IngredientData currentIngredientSO = SO_holder.ingredientSO[currentIngredientIndex + 1];

        //ingredient sprite
        ingredientImage.sprite = currentIngredientSO.ingredientSprite;
        //ingredient name
        ingredientName.text = "" + currentIngredientSO.ingredientName;
        //ingredient description
        ingredientDescription.text = "" + currentIngredientSO.ingredientDescription;
        //ingredient price
        ingredientPrice.text = "" + currentIngredientSO.ingredientPrice;

        //element sprite
        UpdateElementSprite(currentIngredientSO);

        //value
        UpdateValueStar(currentIngredientSO);

        //effectiveness
        UpdateEffectivenessStar(currentIngredientSO);
    }

    void UpdateElementSprite(IngredientData _currentIngredientSO)
    {
        switch(_currentIngredientSO.elementType)
        {
            case Element.Ignis:
                {
                    ingredientElementImage.sprite = SO_holder.ignisLogoSprite;
                    break;
                }
            case Element.Aqua:
                {
                    ingredientElementImage.sprite = SO_holder.aquaLogoSprite;
                    break;
                }
            case Element.Terra:
                {
                    ingredientElementImage.sprite = SO_holder.terraLogoSprite;
                    break;
                }
            case Element.Aer:
                {
                    ingredientElementImage.sprite = SO_holder.aerLogoSprite;
                    break;
                }
            case Element.Ordo:
                {
                    ingredientElementImage.sprite = SO_holder.ordoLogoSprite;
                    break;
                }
        }
    }

    void UpdateValueStar(IngredientData _currentIngredientSO)
    {
        int _priceVariable = _currentIngredientSO.priceVariable;
        int totalStar = 0;

        if (_priceVariable > 0 && _priceVariable <= 5)
        {
            totalStar = 0;
        }
        else if (_priceVariable > 5 && _priceVariable <= 10)
        {
            totalStar = 1;
        }
        else if (_priceVariable > 10 && _priceVariable <= 15)
        {
            totalStar = 2;
        }
        else if (_priceVariable > 15 && _priceVariable <= 20)
        {
            totalStar = 3;
        }
        else if (_priceVariable > 20 && _priceVariable <= 25)
        {
            totalStar = 4;
        }
        else if (_priceVariable > 25)
        {
            totalStar = 5;
        }

        for (int i = 0; i < valueStars.Length; i++)
        {
            if (i < totalStar) //if slot is not empty
            {
                valueStars[i].sprite = SO_holder.priceStar;
            }
            else //if slot is empty
            {
                valueStars[i].sprite = SO_holder.greyStar;
            }
        }
    }

    void UpdateEffectivenessStar(IngredientData _currentIngredientSO)
    {
        int _effectiveVariable = _currentIngredientSO.effectiveVariable;
        //get effective value that Absolute
        int tempAbs = Mathf.Abs(_effectiveVariable);

        int totalStar = 0;

        if (tempAbs >= 0 && tempAbs <= 5)
        {
            totalStar = 0;
        }
        else if (tempAbs > 5 && tempAbs <= 10)
        {
            totalStar = 1;
        }
        else if (tempAbs > 10 && tempAbs <= 15)
        {
            totalStar = 2;
        }
        else if (tempAbs > 15 && tempAbs <= 20)
        {
            totalStar = 3;
        }
        else if (tempAbs > 20 && tempAbs <= 25)
        {
            totalStar = 4;
        }
        else if (tempAbs > 25)
        {
            totalStar = 5;
        }

        for (int i = 0; i < effectivenessStars.Length; i++)
        {
            if (_effectiveVariable != 0) //not neutral
            {
                if (i < totalStar) //slot is not empty
                {
                    if (_effectiveVariable > 0) //green
                    {
                        effectivenessStars[i].sprite = SO_holder.greenStar;
                    }
                    else if (_effectiveVariable < 0) //red
                    {
                        effectivenessStars[i].sprite = SO_holder.redStar;
                    }
                }
                else //slot is empty
                {
                    effectivenessStars[i].sprite = SO_holder.greyStar;
                }
            }
            else //is neutral
            {
                if (i < 1)
                {
                    effectivenessStars[i].sprite = SO_holder.neutralStar;
                }
                else
                {
                    effectivenessStars[i].sprite = SO_holder.transparentSprite;
                }
            }
        }
    }

    public void ChangeAlmanacIngredientIndex(int _buttonIndex)
    {
        currentIngredientIndex = _buttonIndex;
        UpdateIngredientInformation();
    }
}
