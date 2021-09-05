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

        //update formular images
    }

    void UpdateFormularImages(PotionData _potionData)
    {
        //for (int i = 0; i < potion; i++)
        //{

        //}
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
