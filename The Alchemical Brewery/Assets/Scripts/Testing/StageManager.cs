using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }

    public ScriptableObjectHolder so_Holder;
    public StageAssetInstantiate stageAssetInstantiate;

    public static int stageIndex = 1;
    public static List<PotionData> potionListToday = new List<PotionData>();
    public static List<int> ingrdientOrderToday = new List<int>();
    public List<CustomerData> customerTypeToday;
    public List<float> customerAppearRateList = new List<float>();
    public float totalCustomerAppearRate = 0;

    public static bool dayTimeGameplay = false;
    public static bool pauseGame = false;
    public static bool accelerateGame = false;

    [Header("Testing Use")]
    public bool testing = false;
    public List<int> ingOrder_test = new List<int>();


    void Awake()
    {
        Instance = this;

        if(testing)
        {
            SaveManager.Load();
            PlayerProfile.GM_TestingUse();

            //assign potion sell today
            AssignPotionToday();
            //assign all customer type for this stage
            AssignCustomerTypeToday();
            //calculate each customer appear rate
            AssignCustomerAppearRate();
        }
        else
        {

        }
    }

    void Start()
    {
        if(testing)
        {
            InstantiateAssetPrefab();
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

        for (int i = 0; i < stageData.CustomerAppear.Count && i < 5; i++)
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

    void InstantiateAssetPrefab()
    {
        //spawn stage asset position
        GameObject stageAssetPos = Instantiate(stageAssetInstantiate.stageAsset[stageIndex], Vector3.zero, Quaternion.identity) as GameObject;

        //get each position transform
        Transform[] counter_Pos = stageAssetPos.transform.GetChild(1).Cast<Transform>().ToArray();
        Transform[] magicChest_pos = stageAssetPos.transform.GetChild(2).Cast<Transform>().ToArray();
        Transform[] pot_pos = stageAssetPos.transform.GetChild(3).Cast<Transform>().ToArray();
        Transform grinder_pos = stageAssetPos.transform.GetChild(4).GetChild(0).transform;
        Transform distillStand_pos = stageAssetPos.transform.GetChild(4).GetChild(1).transform;

        //spawn each prefab
        List<GameObject> counterPrefabList = InstantiateEachAsset(stageAssetInstantiate.counter_prefab, counter_Pos);
        List<GameObject> magicChestPrefabList = InstantiateEachAsset(stageAssetInstantiate.magicChest_prefab, magicChest_pos);
        List<GameObject> potPrefabList = InstantiateEachAsset(stageAssetInstantiate.pot_prefab, pot_pos);
        GameObject grinderPrefab = Instantiate(stageAssetInstantiate.grinder_prefab, grinder_pos.position, Quaternion.identity) as GameObject;
        GameObject distillStandPrefab = Instantiate(stageAssetInstantiate.distillStand_prefab, distillStand_pos.position, Quaternion.identity) as GameObject;

        //assign parent
        AssignParent(counterPrefabList, stageAssetPos.transform.GetChild(5));
        AssignParent(magicChestPrefabList, stageAssetPos.transform.GetChild(5));
        AssignParent(potPrefabList, stageAssetPos.transform.GetChild(5));
        grinderPrefab.transform.SetParent(stageAssetPos.transform.GetChild(5), false);
        distillStandPrefab.transform.SetParent(stageAssetPos.transform.GetChild(5), false);

        //assign counter data
        AssignCounterData(counterPrefabList);
        //assign magicChest data
        AssignMagicChestData(magicChestPrefabList, ingrdientOrderToday);
        //assign pot data
        AssignPotData(potPrefabList);
    }

    List<GameObject> InstantiateEachAsset(GameObject _prefab, Transform[] instantiatePos)
    {
        //declare a empty list for prefab list
        List<GameObject> prefabList = new List<GameObject>();

        //start spawning and assign into prefab list
        for (int i = 0; i < instantiatePos.Length; i++)
        {
            GameObject spawnedPrefab = Instantiate(_prefab, instantiatePos[i].position, Quaternion.identity) as GameObject;
            prefabList.Add(spawnedPrefab);
        }

        //return prefab list
        return prefabList;
    }

    void AssignParent(List<GameObject> _prefab, Transform parent_transform)
    {
        for (int i = 0; i < _prefab.Count; i++)
        {
            _prefab[i].transform.SetParent(parent_transform, false);
        }
    }

    void AssignCounterData(List<GameObject> _counterPrefabList)
    {
        //declare queuePositionList array size
        CustomerQueueHandler.Instance.queuePositionList = new List<Vector3>[_counterPrefabList.Count];

        CustomerHandler.Instance.DeclareCustomerClass(_counterPrefabList.Count, _counterPrefabList[0].transform.GetChild(0).childCount);

        for (int i = 0; i < _counterPrefabList.Count; i++)
        {
            //get queue interaction script
            QueueInteraction queueInteraction = _counterPrefabList[i].GetComponent<QueueInteraction>();
            //assign queue Index
            queueInteraction.queueIndex = i;
            //assign queue location into queuePositionList
            CustomerQueueHandler.Instance.AssignQueueLocation(_counterPrefabList[i], i);
        }
    }

    void AssignMagicChestData(List<GameObject> _magicChestPrefabList, List<int> _ingrdientOrderToday)
    {
        for (int i = 0; i < _magicChestPrefabList.Count; i++)
        {
            //get magic chest ShelfInteraction script
            ShelfInteraction shelfInteraction = _magicChestPrefabList[i].GetComponent<ShelfInteraction>();
            //set shelfIndex
            shelfInteraction.shelfIndex = i;
            //set ingredient
            shelfInteraction.ingredientIndex = ingOrder_test[i];
            //set shelf refresh Time
            shelfInteraction.shelfReopenTime = 3f;
        }
    }

    void AssignPotData(List<GameObject> _potPrefabList)
    {
        for (int i = 0; i < _potPrefabList.Count; i++)
        {
            //get PotInformationHandler script
            PotInformationHandler potInformationHandler = _potPrefabList[i].GetComponent<PotInformationHandler>();
            //assign pot Index
            potInformationHandler.potIndex = i;
        }
    }

    void StartDayEffect()
    {

    }

    public void StartDayTimeScene()
    {
        //set daytimeGameplay to true
        dayTimeGameplay = true;
        //start clock system
        ClockSystem.Instance.StartClock();
    }
    
    public void EndDayTimeGamePlay()
    {
        //set daytimeGameplay to true
        dayTimeGameplay = false;
        //stop clock system
        ClockSystem.Instance.StopClock();
    }
}
