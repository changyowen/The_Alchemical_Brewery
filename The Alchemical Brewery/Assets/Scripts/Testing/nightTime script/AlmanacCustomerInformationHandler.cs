using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlmanacCustomerInformationHandler : MonoBehaviour
{
    public ScriptableObjectHolder SO_holder;

    [Header("Button Panel Reference")]
    public GameObject[] customerButton_obj;

    [Header("Information Reference")]
    public Image customerImage;
    public Text customerName;
    public TextMeshProUGUI customerDescription;
    public Image[] favorElement;
    public Text favorPotionUsage;
    public Color[] favorUsageColor;
    public Image[] customerUnlockElement;

    [Header("Internal Data")]
    int currentCustomerIndex = 0;

    public void UpdateButtonData()
    {
        for (int i = 0; i < customerButton_obj.Length; i++)
        {
            CustomerData customerData = SO_holder.customerDataSO[i];
            CustomerProfile customerProfile = PlayerProfile.customerProfile[i];

            //update button image
            customerButton_obj[i].transform.GetChild(0).GetComponent<Image>().sprite = customerData.customerSprite;

            //update locked/ulocked
            bool _unlocked = customerProfile.unlocked;
            if(_unlocked)
            {
                customerButton_obj[i].GetComponent<Button>().interactable = true;
                customerButton_obj[i].transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                customerButton_obj[i].GetComponent<Button>().interactable = false;
                customerButton_obj[i].transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }

    public void UpdateCustomerInformation()
    {
        //get customer SO
        CustomerData currentCustomerSO = SO_holder.customerDataSO[currentCustomerIndex];

        //customer sprite
        customerImage.sprite = currentCustomerSO.customerSprite;
        //customer name
        customerName.text = "" + currentCustomerSO.customerName;
        //customer description
        //customerDescription.text = "" + currentCustomerSO.;

        //favor element sprite
        UpdateFavorElementSprite(currentCustomerSO);

        //favor potion usage
        UpdateFavorPotionUsage(currentCustomerSO);

        //unlock element
        UpdateUnlockElement(currentCustomerSO);
    }

    void UpdateFavorElementSprite(CustomerData _currentCustomerSO)
    {
        List<Element> _favorElementList = new List<Element>(_currentCustomerSO.preferElement);

        for (int i = 0; i < favorElement.Length; i++)
        {
            if(i < _favorElementList.Count) //if slot not empty
            {
                switch (_favorElementList[i])
                {
                    case Element.Ignis:
                        {
                            favorElement[i].sprite = SO_holder.ignisLogoSprite;
                            break;
                        }
                    case Element.Aqua:
                        {
                            favorElement[i].sprite = SO_holder.aquaLogoSprite;
                            break;
                        }
                    case Element.Terra:
                        {
                            favorElement[i].sprite = SO_holder.terraLogoSprite;
                            break;
                        }
                    case Element.Aer:
                        {
                            favorElement[i].sprite = SO_holder.aerLogoSprite;
                            break;
                        }
                    case Element.Ordo:
                        {
                            favorElement[i].sprite = SO_holder.ordoLogoSprite;
                            break;
                        }
                }
            }
            else //if slot empty
            {
                favorElement[i].sprite = SO_holder.transparentSprite;
            }
        }
    }

    void UpdateFavorPotionUsage(CustomerData _currentCustomerSO)
    {
        PotionUsage potionUsage = _currentCustomerSO.preferPotionUsage[0];
        switch(potionUsage)
        {
            case PotionUsage.Healing:
                {
                    favorPotionUsage.text = "" + potionUsage.ToString();
                    favorPotionUsage.color = favorUsageColor[0];
                    break;
                }
            case PotionUsage.Damaging:
                {
                    favorPotionUsage.text = "" + potionUsage.ToString();
                    favorPotionUsage.color = favorUsageColor[1];
                    break;
                }
            case PotionUsage.Neutral:
                {
                    favorPotionUsage.text = "" + potionUsage.ToString();
                    favorPotionUsage.color = favorUsageColor[2];
                    break;
                }
        }
    }

    void UpdateUnlockElement(CustomerData _currentCustomerSO)
    {
        for (int i = 0; i < customerUnlockElement.Length; i++)
        {
            UnlockType _unlockType = _currentCustomerSO.unlockTypes[i];
            switch(_unlockType)
            {
                case UnlockType.Ingredient:
                    {
                        int _ingIndex = _currentCustomerSO.unlockIndex[i];
                        customerUnlockElement[i].sprite = SO_holder.ingredientSO[_ingIndex].ingredientSprite;
                        break;
                    }
                case UnlockType.Customer:
                    {
                        int _customerIndex = _currentCustomerSO.unlockIndex[i];
                        customerUnlockElement[i].sprite = SO_holder.customerDataSO[_customerIndex].customerSprite;
                        break;
                    }
                case UnlockType.MagicChest:
                    {
                        customerUnlockElement[i].sprite = SO_holder.magicChestSprite;
                        break;
                    }
                case UnlockType.Pot:
                    {
                        customerUnlockElement[i].sprite = SO_holder.potSprite;
                        break;
                    }
                case UnlockType.Counter:
                    {
                        customerUnlockElement[i].sprite = SO_holder.counterSprite;
                        break;
                    }
                case UnlockType.RefinementStation:
                    {
                        int _refinementStationIndex = _currentCustomerSO.unlockIndex[i];
                        customerUnlockElement[i].sprite = SO_holder.refinementStationSprite[_refinementStationIndex];
                        break;
                    }
                case UnlockType.Region:
                    {
                        //empty first
                        break;
                    }
                case UnlockType.CustomerAppearRate:
                    {
                        //empty first
                        break;
                    }
            }
        }
    }

    public void ChangeAlmanacCustomerIndex(int _buttonIndex)
    {
        currentCustomerIndex = _buttonIndex;
        UpdateCustomerInformation();
    }
}
