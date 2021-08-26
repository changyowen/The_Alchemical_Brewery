using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    public static ResultManager Instance { get; private set; }

    public CustomerResultPanel customerResultPanel;

    public GameObject customerResultPanel_obj;

    CustomerLevelStoring[] initialCustomerLevelArray;
    CustomerLevelStoring[] finalCustomerLevelArray;

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
            newCustomerLevelStoring.customerName = _customerTypeToday[i].customerName;
            newCustomerLevelStoring.customerLevel = currentCustomerProfile.customerLevel;
            if(newCustomerLevelStoring.customerLevel != 6)
            {
                newCustomerLevelStoring.customerExperiencePercentage = currentCustomerProfile.customerExperience / (float)_customerTypeToday[i].levelingExperience[currentCustomerProfile.customerLevel - 1];
            }
            //assign into customer storing array
            finalCustomerLevelArray[i] = newCustomerLevelStoring;
        }
    }

    public IEnumerator StartResult()
    {
        ///SHOW CUSTOMER LEVELING RESULT
        StartCoroutine(StartCustomerResultPanel());

        //show each unlocked stuff
        yield return null;
        
    }

    public IEnumerator StartCustomerResultPanel()
    {
        bool skip = false;
        ///ACTIVATE CUSTOMER RESULT PANEL
        customerResultPanel_obj.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        ///START CUSTOMER CONTENT ANIMATION
        showingCustomerContent = true;
        yield return StartCoroutine(customerResultPanel.StartCustomerContent(initialCustomerLevelArray, finalCustomerLevelArray));
        showingCustomerContent = false;

        ///WAIT TILL PLAYER CLICK
        while (!skip)
        {
            if (Input.GetMouseButtonDown(0))
                skip = true;
            else
                yield return null;
        }
        ///DEACTIVATE CUSTOMER RESULT PANEL
        customerResultPanel_obj.SetActive(false);
    }
}

public class CustomerLevelStoring
{
    public Sprite customerSprite; //customer sprite
    public string customerName; //customer name
    public int customerLevel; //customer current level
    public float customerExperiencePercentage = 1; //customer current experience bar percentage
}
