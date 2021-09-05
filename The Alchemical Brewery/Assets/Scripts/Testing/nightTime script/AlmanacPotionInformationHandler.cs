using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlmanacPotionInformationHandler : MonoBehaviour
{
    public ScriptableObjectHolder SO_holder;

    [Header("Button Panel Reference")]
    public Transform buttonContainer_transform;
    public GameObject[] potionButton_objList;
    public GameObject potionButton_prefab;

    [Header("Information Reference")]
    public Image potionIcon;
    public Text potionName;
    public Image[] potionFormularImages;
    public Image[] potionElementImages;
    public Text potionUsage;
    public Color[] potionUsageColor;
    public Text potionQualityText;
    public Color[] potionQualityColor;
    public Text potionPricetext;

    [Header("Internal Data")]
    int currentPotionIndex = 0;

    public void UpdateButtonData()
    {
        //clear all button
        ClearAllButton();

        //get player all potion data
        List<PotionData> potionDataList = new List<PotionData>(PlayerProfile.acquiredPotion);

        //create button again [from acquired potion backward]
        for (int i = potionDataList.Count - 1; i >= 0; i--)
        {
            //instantiate button
            GameObject newPotionButtonPrefab = Instantiate(potionButton_prefab, Vector3.zero, Quaternion.identity) as GameObject;
            newPotionButtonPrefab.transform.SetParent(buttonContainer_transform, false);

            //update button script index
            AlmanacPotionButtonInteraction buttonScript = newPotionButtonPrefab.GetComponent<AlmanacPotionButtonInteraction>();
            buttonScript.buttonIndex = i;

            //update button icon
            newPotionButtonPrefab.transform.GetChild(0).GetComponent<Image>().sprite = SO_holder.potionIconList[potionDataList[i].potionSpriteIndex];
        }
    }

    public void UpdatePotionInformation()
    {
        //get potion data
        PotionData _potionData = PlayerProfile.acquiredPotion[currentPotionIndex];

        //potion icon
        potionIcon.sprite = SO_holder.potionIconList[_potionData.potionSpriteIndex];
        //potion name
        potionName.text = "" + _potionData.potionName;

        //potion price
        //potionPricetext.text = "" + _potionDat
        
        //update formular images
        UpdateFormularImages(_potionData);

        //update potion element
        UpdateElementImages(_potionData);

        //update potion usage
        UpdatePotionUsage(_potionData);
    }

    void UpdateFormularImages(PotionData _potionData)
    {
        for (int i = 0; i < potionFormularImages.Length; i++)
        {
            potionFormularImages[i].sprite = SO_holder.ingredientSO[_potionData.potionFormular[i]].ingredientSprite;
        }
    }

    void UpdateElementImages(PotionData _potionData)
    {
        for (int i = 0; i < potionElementImages.Length; i++)
        {
            if(i < _potionData.potionElement.Count)//slot not empty
            {
                switch(_potionData.potionElement[i])
                {
                    case Element.Ignis:
                        {
                            potionElementImages[i].sprite = SO_holder.ignisLogoSprite;
                            break;
                        }
                    case Element.Aqua:
                        {
                            potionElementImages[i].sprite = SO_holder.aquaLogoSprite;
                            break;
                        }
                    case Element.Terra:
                        {
                            potionElementImages[i].sprite = SO_holder.terraLogoSprite;
                            break;
                        }
                    case Element.Aer:
                        {
                            potionElementImages[i].sprite = SO_holder.aerLogoSprite;
                            break;
                        }
                    case Element.Ordo:
                        {
                            potionElementImages[i].sprite = SO_holder.ordoLogoSprite;
                            break;
                        }
                }
            }
            else //slot empty
            {
                potionElementImages[i].sprite = SO_holder.transparentSprite;
            }
        }
    }

    void UpdatePotionUsage(PotionData _potionData)
    {
        potionUsage.text = "" + _potionData.potionUsage.ToString();

        switch(_potionData.potionUsage)
        {
            case PotionUsage.Healing:
                {
                    potionUsage.color = potionUsageColor[0];
                    break;
                }
            case PotionUsage.Damaging:
                {
                    potionUsage.color = potionUsageColor[1];
                    break;
                }
            case PotionUsage.Neutral:
                {
                    potionUsage.color = potionUsageColor[2];
                    break;
                }
        }
    }

    void UpdatePotionQuality(PotionData _potionData)
    {
        if(_potionData.potionQuality <= 100f && _potionData.potionQuality > 80f)
        {
            potionQualityText.text = "Awesome";
            potionQualityText.color = potionQualityColor[0];
        }
        else if(_potionData.potionQuality <= 80f && _potionData.potionQuality > 60f)
        {
            potionQualityText.text = "Good";
            potionQualityText.color = potionQualityColor[1];
        }
        else if(_potionData.potionQuality <= 60f && _potionData.potionQuality > 40f)
        {
            potionQualityText.text = "Normal";
            potionQualityText.color = potionQualityColor[2];
        }
        else if (_potionData.potionQuality <= 40f && _potionData.potionQuality > 20f)
        {
            potionQualityText.text = "Poor";
            potionQualityText.color = potionQualityColor[3];
        }
        else if (_potionData.potionQuality <= 20f && _potionData.potionQuality >= 0f)
        {
            potionQualityText.text = "Awful";
            potionQualityText.color = potionQualityColor[4];
        }
    }

    void ClearAllButton()
    {
        for (int i = buttonContainer_transform.childCount - 1; i >= 0; i--)
        {
            GameObject button_obj = buttonContainer_transform.GetChild(i).gameObject;
            Destroy(button_obj);
        }
    }

    public void ChangeAlmanacPotionIndex(int _buttonIndex)
    {
        currentPotionIndex = _buttonIndex;
        UpdatePotionInformation();
    }
}
