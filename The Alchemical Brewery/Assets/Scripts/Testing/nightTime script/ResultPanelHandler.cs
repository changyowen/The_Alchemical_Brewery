using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultPanelHandler : MonoBehaviour
{
    public ScriptableObjectHolder so_Holder;

    public Image potionIcon;
    public Text potionName_text;
    public TMP_InputField potionName_inputText;
    public Text quality_text;
    public Text element_text;
    public Text usage_text;
    public Text price_text;
    public Image[] formular_image;

    public Transform resultPanel_transform;
    public GameObject changePotionIcon_panel;
    public PotionData potionData = null;

    public void AssignPreviousPotion(int formularIndex)
    {
        PotionData currentPotionData = PlayerProfile.acquiredPotion[formularIndex];
        if(potionName_text != null)
        {
            potionName_text.text = "" + currentPotionData.potionName;
        }
        quality_text.text = "" + currentPotionData.potionQuality;
        AssignElementString(currentPotionData.potionElement);
        usage_text.text = "" + currentPotionData.potionUsage.ToString();
        //price_text.text = "" + currentPotionData.
        List<int> currentPotionFormular = new List<int>(currentPotionData.potionFormular);
        AssignFormularImage(currentPotionFormular);
    }

    public void AssignUnknownPotion(List<int> formularIndex)
    {
        potionName_text.text = "???";
        quality_text.text = "???";
        AssignElementString(null);
        usage_text.text = "???";
        //price_text.text = "" + currentPotionData.
        AssignFormularImage(formularIndex);
    }

    public void AssignNullFormular()
    {
        potionName_text.text = "-";
        quality_text.text = "-";
        element_text.text = "-";
        usage_text.text = "-";
        //price_text.text = "" + currentPotionData.
        AssignFormularImage(null);
    }

    public void AssignElementString(List<Element> _potionElement)
    {
        if(_potionElement != null)
        {
            string _elementString = "";
            for (int i = 0; i < _potionElement.Count; i++)
            {
                if(i != _potionElement.Count - 1)
                {
                    _elementString = _elementString + _potionElement[i].ToString() + "+";
                }
                else
                {
                    _elementString = _elementString + _potionElement[i].ToString();
                }
            }
            element_text.text = "" + _elementString;
        }
        else
        {
            element_text.text = "???";
        }
    }

    public void AssignFormularImage(List<int> _formularIndex)
    {
        if(_formularIndex != null)
        {
            for (int i = 0; i < formular_image.Length; i++)
            {
                Sprite _ingSprite = so_Holder.ingredientSO[_formularIndex[i]].ingredientSprite;
                formular_image[i].sprite = _ingSprite;
            }
        }
        else
        {
            for (int i = 0; i < formular_image.Length; i++)
            {
                Sprite _transSprite = so_Holder.transparentSprite;
                formular_image[i].sprite = _transSprite;
            }
        }
    }

    public void ChangePotionIcon()
    {
        GameObject changeIconPanel = Instantiate(changePotionIcon_panel, Vector3.zero, Quaternion.identity);
        changeIconPanel.transform.SetParent(resultPanel_transform, false);

        ChangePotionIconPanel panelScript = changeIconPanel.GetComponent<ChangePotionIconPanel>();
        panelScript.potionData = potionData;
        panelScript.resultPanelScript = this;
    }

    public void RefreshPotionLogo()
    {
        potionIcon.sprite = so_Holder.potionIconList[potionData.potionSpriteIndex];
    }

    void UpdateNewPotionData()
    {
        if(potionData != null)
        {
            string newName = potionName_inputText.text;
            potionData.potionName = newName;
        }
    }

    public void CloseResultPanel()
    {
        UpdateNewPotionData();
        SaveManager.Save();
        Destroy(this.gameObject);
    }
}
