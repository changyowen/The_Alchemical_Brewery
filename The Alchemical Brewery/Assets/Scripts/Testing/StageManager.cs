using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }

    public bool testing = false;

    public ScriptableObjectHolder so_Holder;

    public static int stageIndex = 1;

    public static List<PotionData> potionListToday = new List<PotionData>();

    public List<CustomerData> customerTypeToday;

    public List<float> customerAppearRateList = new List<float>();
    public float totalCustomerAppearRate = 0;

    void Awake()
    {
        Instance = this;

        if(testing)
        {
            SaveManager.Load();
            PlayerProfile.GM_TestingUse();

            AssignPotionToday();
            AssignCustomerTypeToday();
            AssignCustomerAppearRate();
        }
    }

    void AssignPotionToday()
    {
        if (PlayerProfile.acquiredPotion.Count < 4)
        {
            for (int i = 0; i < PlayerProfile.acquiredPotion.Count; i++)
            {
                potionListToday.Add(PlayerProfile.acquiredPotion[i]);
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                potionListToday.Add(PlayerProfile.acquiredPotion[i]);
            }
        }
    }

    void AssignCustomerTypeToday()
    {
        StageDataAssign stageData = so_Holder.stageDataSO[stageIndex];

        for (int i = 0; i < stageData.CustomerAppear.Count; i++)
        {
            int customerIndex = stageData.CustomerAppear[i];
            customerTypeToday.Add(so_Holder.customerDataSO[customerIndex]);
        }
    }

    void AssignCustomerAppearRate()
    {
        for (int i = 0; i < customerTypeToday.Count; i++)
        {
            //get customer index and level
            int customerIndex = customerTypeToday[i].customerIndex;
            Debug.Log(PlayerProfile.customerProfile.Length);
            int customerLevel = PlayerProfile.customerProfile[customerIndex].customerLevel;
            //get customer base & leveling appear rate
            float baseAppearRate = customerTypeToday[i].baseAppearRate;
            float levelingAppearRate = customerLevel * customerTypeToday[i].levelingAppearRate;
            //assign total appear rate into customerAppearrateList
            float customerAppearRate = baseAppearRate + levelingAppearRate;
            customerAppearRateList.Add(customerAppearRate);
            //sum into totalCustomerAppearRate
            totalCustomerAppearRate += customerAppearRate; 
        }
    }
}
