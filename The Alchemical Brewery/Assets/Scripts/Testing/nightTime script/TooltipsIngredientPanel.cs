using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TooltipsIngredientPanel : MonoBehaviour
{
    public ScriptableObjectHolder SO_holder;

    public int buttonIndex = 0;

    public TextMeshProUGUI name_TMP;
    public Image element_img;
    public Image[] valueStar_img;
    public Image[] effectivenessStar_img;

    public void AssignTooltipsData(IngredientData _currentIngredientData)
    {
        //update name
        name_TMP.text = _currentIngredientData.ingredientName;
        //update element
        Element currentElement = _currentIngredientData.elementType;
        UpdateElementImage(currentElement);

        //update value
        int ingredientPriceVariable = _currentIngredientData.priceVariable;
        UpdatePriceVariable(ingredientPriceVariable);

        //update effectiveness
        int ingredientEffectiveVariable = _currentIngredientData.effectiveVariable;
        UpdateEffectiveVariable(ingredientEffectiveVariable);
    }

    void UpdateElementImage(Element _currentElement)
    {
        switch(_currentElement)
        {
            case Element.Ignis:
                {
                    element_img.sprite = SO_holder.ignisLogoSprite;
                    break;
                }
            case Element.Aqua:
                {
                    element_img.sprite = SO_holder.aquaLogoSprite;
                    break;
                }
            case Element.Terra:
                {
                    element_img.sprite = SO_holder.terraLogoSprite;
                    break;
                }
            case Element.Aer:
                {
                    element_img.sprite = SO_holder.aerLogoSprite;
                    break;
                }
            case Element.Ordo:
                {
                    element_img.sprite = SO_holder.ordoLogoSprite;
                    break;
                }
        }
    }

    void UpdatePriceVariable(int _priceVariable)
    {
        int totalStar = 0;

        if(_priceVariable > 0 && _priceVariable <= 5)
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
        else if(_priceVariable > 25)
        {
            totalStar = 5;
        }

        for (int i = 0; i < valueStar_img.Length; i++)
        {
            if(i < totalStar) //if slot is not empty
            {
                valueStar_img[i].sprite = SO_holder.priceStar;
            }
            else //if slot is empty
            {
                valueStar_img[i].sprite = SO_holder.greyStar;
            }
        }
    }

    void UpdateEffectiveVariable(int _effectiveVariable)
    {
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

        for (int i = 0; i < effectivenessStar_img.Length; i++)
        {
            if(_effectiveVariable != 0) //not neutral
            {
                if(i < totalStar) //slot is not empty
                {
                    if(_effectiveVariable > 0) //green
                    {
                        effectivenessStar_img[i].sprite = SO_holder.greenStar;
                    }
                    else if(_effectiveVariable < 0) //red
                    {
                        effectivenessStar_img[i].sprite = SO_holder.redStar;
                    }
                }
                else //slot is empty
                {
                    effectivenessStar_img[i].sprite = SO_holder.greyStar;
                }
            }
            else //is neutral
            {
                if(i < 1)
                {
                    effectivenessStar_img[i].sprite = SO_holder.neutralStar;
                }
                else
                {
                    effectivenessStar_img[i].sprite = SO_holder.transparentSprite;
                }
            }
        }
    }
}
