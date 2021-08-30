using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionInformationHandler : MonoBehaviour
{
    [Header("Scriptable Object Holder")]
    public ScriptableObjectHolder SO_holder;

    [Header("Information")]
    public Image potionIcon;
    public Text potionName_text;
    public Text usage_text;
    public Text quality_text;
    public Text price_text;
    public Image[] elementImages;
    public Image[] formularImages;
    public Image[] refinementImages;

    [Header("Reference")]
    public GameObject bg_shine_obj;
    public GameObject bg_dull_obj;

    public bool mouseIn = false;
    public bool alreadyChosen = false;
    PotionData currentPotionData = null;

    private void Update()
    {
        bg_shine_obj.SetActive(mouseIn);
        bg_dull_obj.SetActive(alreadyChosen);
    }

    public void UpdateDataInformation(PotionData _potionData)
    {
        ///UPDATE POTION DATA
        currentPotionData = _potionData;

        ///ICON
        //potionIcon.sprite = 

        ///NAME
        potionName_text.text = "" + currentPotionData.potionName;

        ///PRICE
        //price_text.text = "$" + _potionData.

        ///USAGE
        usage_text.text = "" + currentPotionData.potionUsage.ToString();

        ///QUALITY
        quality_text.text = currentPotionData.potionQuality + "%";

        ///ELEMENT UPDATE
        ElementImagesUpdate();

        ///FORMULAR IMAGES UPDATE
        FormularImagesUpdate();

        ///UPDATE IF ALREADY CHOSEN
        UpdateAlreadyChosen();
    }

    void ElementImagesUpdate()
    {

        ///ELEMENT
        for (int i = 0; i < currentPotionData.potionElement.Count; i++)
        {
            switch (currentPotionData.potionElement[i])
            {
                case Element.Ignis:
                    {
                        elementImages[i].sprite = SO_holder.ignisLogoSprite;
                        break;
                    }
                case Element.Aqua:
                    {
                        elementImages[i].sprite = SO_holder.aquaLogoSprite;
                        break;
                    }
                case Element.Ordo:
                    {
                        elementImages[i].sprite = SO_holder.ordoLogoSprite;
                        break;
                    }
                case Element.Terra:
                    {
                        elementImages[i].sprite = SO_holder.terraLogoSprite;
                        break;
                    }
                case Element.Aer:
                    {
                        elementImages[i].sprite = SO_holder.aerLogoSprite;
                        break;
                    }
                default:
                    {
                        elementImages[i].sprite = SO_holder.transparentSprite;
                        break;
                    }
            }
        }
        //assign transparent sprite for empty slot
        int totalEmptyslot = 4 - currentPotionData.potionElement.Count;
        if (totalEmptyslot != 0)
        {
            for (int i = elementImages.Length - 1; i >= elementImages.Length - totalEmptyslot; i--)
            {
                elementImages[i].sprite = SO_holder.transparentSprite;
            }
        }
    }

    void FormularImagesUpdate()
    {
        ///FORMULAR
        for (int i = 0; i < currentPotionData.potionFormular.Length; i++)
        {
            //get ingredient SO
            IngredientData currentIngredientData = SO_holder.ingredientSO[currentPotionData.potionFormular[i]];
            switch (currentIngredientData.refineStage)
            {
                case RefinementStage.Normal:
                    {
                        formularImages[i].sprite = currentIngredientData.ingredientSprite;
                        refinementImages[i].sprite = SO_holder.transparentSprite;
                        break;
                    }
                case RefinementStage.Crushed:
                    {
                        formularImages[i].sprite = currentIngredientData.originalIngredient.ingredientSprite;
                        refinementImages[i].sprite = SO_holder.crushedLogoSprite;
                        break;
                    }
                case RefinementStage.Extract:
                    {
                        formularImages[i].sprite = currentIngredientData.originalIngredient.ingredientSprite;
                        refinementImages[i].sprite = SO_holder.extractLogoSprite;
                        break;
                    }
            }
        }
    }

    public void UpdateAlreadyChosen()
    {
        ///UPDATE IF ALREADY CHOSEN
        for (int i = 0; i < ChosenPotionHandler.Instance.chosenPotionList.Count; i++)
        {
            if (ChosenPotionHandler.Instance.chosenPotionList[i] == currentPotionData)
            {
                alreadyChosen = true;
                return;
            }
        }
        alreadyChosen = false;
    }

    public void ChoosePotion()
    {
        if(!alreadyChosen && ChosenPotionHandler.Instance.chosenPotionList.Count < 4)
        {
            ChosenPotionHandler.Instance.chosenPotionList.Add(currentPotionData);
            ChosenPotionHandler.Instance.UpdateChosenPotion();
            UpdateAlreadyChosen();
            alreadyChosen = true;
        }
    }
}
