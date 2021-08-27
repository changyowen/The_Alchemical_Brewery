using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    public static ResultManager Instance { get; private set; }

    public CustomerResultPanel customerResultPanel;

    public GameObject resultPanel_obj;
    public GameObject customerResultPanel_obj;
    public GameObject unlockStuffPanel_obj;

    CustomerLevelStoring[] initialCustomerLevelArray;
    CustomerLevelStoring[] finalCustomerLevelArray;

    public List<TodayUnlockStuff> todayUnlockedStuff = new List<TodayUnlockStuff>();
    bool showingCustomerContent = false;

    private void Awake()
    {
        Instance = this;
    }

    public void AssignInitialCustomerLevelArray(List<CustomerData> _customerTypeToday)
    {
        ///DECLARE CUSTOMER STORING ARRAY 
        initialCustomerLevelArray = new CustomerLevelStoring[_customerTypeToday.Count];

        ///ASSIGN DATA FOR CUSTOMER STORING ARRAY
        for (int i = 0; i < initialCustomerLevelArray.Length; i++)
        {
            //get corresponding customer profile
            CustomerProfile currentCustomerProfile = PlayerProfile.customerProfile[_customerTypeToday[i].customerIndex];
            //assign data
            CustomerLevelStoring newCustomerLevelStoring = new CustomerLevelStoring();
            newCustomerLevelStoring.customerSprite = _customerTypeToday[i].customerSprite;
            newCustomerLevelStoring.customerName = _customerTypeToday[i].customerName;
            newCustomerLevelStoring.customerLevel = currentCustomerProfile.customerLevel;
            if (newCustomerLevelStoring.customerLevel != 6)
            {
                newCustomerLevelStoring.customerExperiencePercentage = currentCustomerProfile.customerExperience / (float)_customerTypeToday[i].levelingExperience[currentCustomerProfile.customerLevel - 1];
            }            
            //assign into customer storing array
            initialCustomerLevelArray[i] = newCustomerLevelStoring;
        }
    }

    public void AssignFinalCustomerLevelArray(List<CustomerData> _customerTypeToday)
    {
        ///DECLARE CUSTOMER STORING ARRAY 
        finalCustomerLevelArray = new CustomerLevelStoring[_customerTypeToday.Count];
        ///ASSIGN DATA FOR CUSTOMER STORING ARRAY
        for (int i = 0; i < initialCustomerLevelArray.Length; i++)
        {
            //get corresponding customer profile
            CustomerProfile currentCustomerProfile = PlayerProfile.customerProfile[_customerTypeToday[i].customerIndex];
            //assign data
            CustomerLevelStoring newCustomerLevelStoring = new CustomerLevelStoring();
            newCustomerLevelStoring.customerSprite = _customerTypeToday[i].customerSprite;
            newCustomerLevelStoring.customerName = _customerTypeToday[i].customerName;
            newCustomerLevelStoring.customerLevel = currentCustomerProfile.customerLevel;
            if(newCustomerLevelStoring.customerLevel != 6)
            {
                newCustomerLevelStoring.customerExperiencePercentage = currentCustomerProfile.customerExperience / (float)_customerTypeToday[i].levelingExperience[currentCustomerProfile.customerLevel - 1];
            }
            //assign into customer storing array
            finalCustomerLevelArray[i] = newCustomerLevelStoring;
            Debug.Log(currentCustomerProfile.customerExperience);
            Debug.Log((float)_customerTypeToday[i].levelingExperience[currentCustomerProfile.customerLevel - 1]);
        }
    }

    public IEnumerator StartResult()
    {
        ///SHOW CUSTOMER LEVELING RESULT
        yield return StartCoroutine(StartCustomerResultPanel());

        ///START UNLOCKED STUFF PANEL ANIMATION
        yield return StartCoroutine(StartUnlockStuffPanel());

        yield return new WaitForSeconds(1f);
    }

    IEnumerator StartCustomerResultPanel()
    {
        ///ACTIVATE CUSTOMER RESULT PANEL
        customerResultPanel_obj.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        ///START CUSTOMER CONTENT ANIMATION
        showingCustomerContent = true;
        yield return StartCoroutine(customerResultPanel.StartCustomerContent(initialCustomerLevelArray, finalCustomerLevelArray));
        showingCustomerContent = false;

        ///WAIT TILL PLAYER CLICK
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        ///DEACTIVATE CUSTOMER RESULT PANEL
        customerResultPanel_obj.SetActive(false);
    }

    IEnumerator StartUnlockStuffPanel()
    {
        ///START UNLOCKED STUFF PANEL ANIMATION
        if (todayUnlockedStuff.Count > 0)
        {
            for (int i = 0; i < todayUnlockedStuff.Count; i++)
            {
                //instantiate unlocked Stuff Panel
                GameObject newUnlockStuffPanel = Instantiate(unlockStuffPanel_obj, Vector3.zero, Quaternion.identity) as GameObject;
                newUnlockStuffPanel.transform.SetParent(resultPanel_obj.transform, false);
                //assign data
                UnlockedStuffPanel currentUnlockStuffPanel = newUnlockStuffPanel.GetComponent<UnlockedStuffPanel>();
                currentUnlockStuffPanel.AssignData(todayUnlockedStuff[i]);

                //wait until enabled skip
                while (!currentUnlockStuffPanel.enabledSkip)
                {
                    yield return null;
                }
                //wait until player click to skip
                while (currentUnlockStuffPanel.enabledSkip)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        Debug.Log("test");
                        newUnlockStuffPanel.GetComponent<Animator>().SetTrigger("enabledExit");
                        currentUnlockStuffPanel.enabledSkip = false;
                    }
                    else
                    {
                        yield return null;
                    }
                }
                //wait till finish animation
                yield return new WaitUntil(() => newUnlockStuffPanel.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("closing"));
                //destroy current unlock stuff panel
                Destroy(newUnlockStuffPanel);
            }
        }
    }
}

public class CustomerLevelStoring
{
    public Sprite customerSprite; //customer sprite
    public string customerName; //customer name
    public int customerLevel; //customer current level
    public float customerExperiencePercentage = 1; //customer current experience bar percentage
}

public class TodayUnlockStuff
{
    public UnlockType unlockType;
    public int unlockedIndex;
}
