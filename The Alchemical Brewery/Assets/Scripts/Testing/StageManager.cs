using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }

    public ScriptableObjectHolder so_Holder;
    public StageAssetInstantiate stageAssetInstantiate;
    public InstantiateAssetHandler instantiateAssetHandler;

    public static int stageIndex = 1;
    public static List<PotionData> potionListToday = new List<PotionData>();
    public static List<int> ingredientOrderToday = new List<int>();
    public static List<CustomerData> customerTypeToday = null;
    public List<float> customerAppearRateList = new List<float>();
    public float totalCustomerAppearRate = 0;

    public static bool dayTimeGameplay = false;
    public static bool pauseGame = false;
    public static bool accelerateGame = false;
    private bool endScene = false;

    [Header("Testing Use")]
    public bool testing = false;
    public List<int> ingOrder_test = new List<int>();


    void Awake()
    {
        Instance = this;

        if(testing)
        {
            //PlayerProfile.GM_TestingUse();
            SaveManager.Load();

            //assign potion sell today
            AssignPotionToday();
            //assign all customer type for this stage (IF NULL)
            if(customerTypeToday == null)
            {
                AssignCustomerTypeToday();
            }
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
            instantiateAssetHandler.InstantiateAssetPrefab(stageIndex, ingredientOrderToday);
            ResultManager.Instance.AssignInitialCustomerLevelArray(customerTypeToday);
            StartCoroutine(StartDayTimeScene());
        }
    }

    void Update()
    {
        if(ClockSystem.Instance.TimeOfDay >= 20f)
        {
            if(dayTimeGameplay)
            {
                StartCoroutine(EndDayTimeGamePlay());
            }
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

        customerTypeToday = new List<CustomerData>();
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

    IEnumerator StartDayTimeScene()
    {
        //start day time intro & return its animator
        Animator dayTimeIntro_anim = instantiateAssetHandler.StartDayTimeIntro();

        //loop if day time intro havent finish
        while(dayTimeIntro_anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1)
        {
            yield return null; 
        }
        //disable day time intro
        instantiateAssetHandler.DisableDayTimeIntro();

        //set daytimeGameplay to true
        dayTimeGameplay = true;

        //start spawning villager and customer
        StartCoroutine(CustomerHandler.Instance.GenerateVillager());
        StartCoroutine(CustomerHandler.Instance.GenerateCustomer());
    }

    IEnumerator EndDayTimeGamePlay()
    {
        //instantiate dayTimeOutro and get its animator
        Animator dayTimeOutro_anim = instantiateAssetHandler.StartDayTimeOutro();

        //set daytimeGameplay to true
        dayTimeGameplay = false;

        //clear all customer

        //Start loading next scene
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        operation.allowSceneActivation = false;

        //loop if day time intro havent finish
        while (dayTimeOutro_anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1)
        {
            yield return null;
        }

        ResultManager.Instance.AssignFinalCustomerLevelArray(customerTypeToday);
        yield return StartCoroutine(ResultManager.Instance.StartResult());

        //enable change scene
        operation.allowSceneActivation = true;
    }
}
