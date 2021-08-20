using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Customer", menuName = "Customer Data")]
public class CustomerData : ScriptableObject
{
    public enum UnlockType
    {
        Ingredient,
        Customer,
        MagicChest,
        Pot,
        Counter,
        RefinementStation,
        Region,
        CustomerAppearRate
    }

    [Header("Customer Information")]
    public int customerIndex;
    public string customerName;
    public Sprite customerSprite;

    [Header("Customer Preferred")]
    public List<Element> preferElement;
    public List<PotionUsage> preferPotionUsage;

    [Header("Customer Appear Rate")]
    public float baseAppearRate;
    public float levelingAppearRate;

    [Header("Customer Experience Needed")]
    public float[] levelingExperience;

    [Header("Unlocked Stuff Each Level")]
    public UnlockType[] unlockTypes = new UnlockType[5];
    public int[] unlockIndex = new int[5];

    public void LevelUpUnlock(int level)
    {
        int index = level - 1;

        switch(unlockTypes[index])
        {
            case UnlockType.Ingredient:
                {
                    UnlockIngredient(unlockIndex[index]);
                    break;
                }
            case UnlockType.Customer:
                {
                    UnlockCustomer(unlockIndex[index]);
                    break;
                }
            case UnlockType.MagicChest:
                {
                    UnlockMagicChest();
                    break;
                }
            case UnlockType.Pot:
                {
                    UnlockPot();
                    break;
                }
            case UnlockType.Counter:
                {
                    UnlockCounter();
                    break;
                }
            case UnlockType.RefinementStation:
                {
                    UnlockRefinementStation(unlockIndex[index]);
                    break;
                }
            case UnlockType.Region:
                {
                    UnlockRegion(unlockIndex[index]);
                    break;
                }
            case UnlockType.CustomerAppearRate:
                {
                    IncreaseCustomerAppearRate();
                    break;
                }
        }
    }

    void UnlockIngredient(int index)
    {
        PlayerProfile.ingredientProfile[index - 1].UnlockThisIngredient();
    }

    void UnlockCustomer(int index)
    {
        PlayerProfile.customerProfile[index].UnlockThisCustomer();
    }

    void UnlockMagicChest()
    {
        PlayerProfile.fairyShopProfile.magicChestUnlocked++;
    }

    void UnlockPot()
    {
        PlayerProfile.fairyShopProfile.potUnlocked++;
    }

    void UnlockCounter()
    {
        PlayerProfile.fairyShopProfile.counterUnlocked++;
    }

    void UnlockRefinementStation(int index)
    {
        PlayerProfile.fairyShopProfile.refinementStationUnlocked[index] = true;
    }

    void UnlockRegion(int index)
    {
        
    }

    void IncreaseCustomerAppearRate()
    {
        PlayerProfile.shopProfile.increaseCustomerAppearRate = true;
    }
}
