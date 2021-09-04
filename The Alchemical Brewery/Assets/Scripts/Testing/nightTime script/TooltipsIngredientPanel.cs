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

        //update effectiveness

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
}
