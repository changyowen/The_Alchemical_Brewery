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
        //ingredientPrice.text = "" + currentIngredientSO.

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
        //int totalStar = 0;
        //if(_currentIngredientSO.priceVariable )
    }

    void UpdateEffectivenessStar(IngredientData _currentIngredientSO)
    {

    }

    public void ChangeAlmanacIngredientIndex(int _buttonIndex)
    {
        currentIngredientIndex = _buttonIndex;
        UpdateIngredientInformation();
    }
}
