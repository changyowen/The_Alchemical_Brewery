using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChosenPotionInformation : MonoBehaviour
{
    [Header("Scriptable Object Holder")]
    public ScriptableObjectHolder SO_holder;

    [Header("Reference")]
    public GameObject elementImage_obj;
    public Transform elementPanel_transform;
    public GameObject closeButton_obj;

    [Header("Information")]
    public Image[] formularImages;
    public Image[] refinementImages;

    public bool mouseIn = false;

    private void Update()
    {
        closeButton_obj.SetActive(mouseIn);
    }

    public void UpdatePanelInformation(PotionData _potionData)
    {
        ///UPDATE FORMULAR IMAGES
        UpdateFormularImages(_potionData);

        ///UPDATE ELEMENT IMAGES
        UpdateElementImages(_potionData);
    }

    public void UpdateEmptyInformation()
    {
        ///FORMULAR
        for (int i = 0; i < formularImages.Length; i++)
        {
            formularImages[i].sprite = SO_holder.transparentSprite;
            refinementImages[i].sprite = SO_holder.transparentSprite;
        }

        ///DESTROY ALL ELEMENT IMAGE
        int childs = elementPanel_transform.childCount;
        if(childs != 0)
        {
            for (int i = childs - 1; i >= 0; i--)
            {
                GameObject.Destroy(elementPanel_transform.GetChild(i).gameObject);
            }
        }
    }

    void UpdateFormularImages(PotionData _potionData)
    {
        ///FORMULAR
        for (int i = 0; i < _potionData.potionFormular.Length; i++)
        {
            //get ingredient SO
            IngredientData currentIngredientData = SO_holder.ingredientSO[_potionData.potionFormular[i]];
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

    void UpdateElementImages(PotionData _potionData)
    {
        ///ELEMENT
        //destroy all element image first
        int childs = elementPanel_transform.childCount;
        if (childs != 0)
        {
            for (int i = childs - 1; i >= 0; i--)
            {
                GameObject.Destroy(elementPanel_transform.GetChild(i).gameObject);
            }
        }
        //instantiate element image
        for (int i = 0; i < _potionData.potionElement.Count; i++)
        {
            switch (_potionData.potionElement[i])
            {
                case Element.Ignis:
                    {
                        GameObject newElementImage = Instantiate(elementImage_obj, Vector3.zero, Quaternion.identity) as GameObject;
                        newElementImage.transform.SetParent(elementPanel_transform, false);
                        newElementImage.GetComponent<Image>().sprite = SO_holder.ignisLogoSprite;
                        break;
                    }
                case Element.Aqua:
                    {
                        GameObject newElementImage = Instantiate(elementImage_obj, Vector3.zero, Quaternion.identity) as GameObject;
                        newElementImage.transform.SetParent(elementPanel_transform, false);
                        newElementImage.GetComponent<Image>().sprite = SO_holder.aquaLogoSprite;
                        break;
                    }
                case Element.Ordo:
                    {
                        GameObject newElementImage = Instantiate(elementImage_obj, Vector3.zero, Quaternion.identity) as GameObject;
                        newElementImage.transform.SetParent(elementPanel_transform, false);
                        newElementImage.GetComponent<Image>().sprite = SO_holder.ordoLogoSprite;
                        break;
                    }
                case Element.Terra:
                    {
                        GameObject newElementImage = Instantiate(elementImage_obj, Vector3.zero, Quaternion.identity) as GameObject;
                        newElementImage.transform.SetParent(elementPanel_transform, false);
                        newElementImage.GetComponent<Image>().sprite = SO_holder.terraLogoSprite;
                        break;
                    }
                case Element.Aer:
                    {
                        GameObject newElementImage = Instantiate(elementImage_obj, Vector3.zero, Quaternion.identity) as GameObject;
                        newElementImage.transform.SetParent(elementPanel_transform, false);
                        newElementImage.GetComponent<Image>().sprite = SO_holder.aerLogoSprite;
                        break;
                    }
                default:
                    {
                        GameObject newElementImage = Instantiate(elementImage_obj, Vector3.zero, Quaternion.identity) as GameObject;
                        newElementImage.transform.SetParent(elementPanel_transform, false);
                        newElementImage.GetComponent<Image>().sprite = SO_holder.transparentSprite;
                        break;
                    }
            }
        }
    }
}
