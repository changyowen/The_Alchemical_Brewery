using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerQueueHandler : MonoBehaviour
{
    public static CustomerQueueHandler Instance { get; private set; }

    public List<Vector3>[] queuePositionList;
    public GameObject queueHolder;
    public int unlockedQueue = 1;
    public int queueLength = 6;
    public Vector3 startLocation;
    public float queueLocationSpace = 1f;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }

    public void AssignQueueLocation(GameObject counterPrefab, int queueIndex)
    {
        //declare queue position list
        queuePositionList[queueIndex] = new List<Vector3>();
        //assign location into list
        for (int i = 0; i < counterPrefab.transform.GetChild(0).childCount; i++)
        {
            Vector3 pos = counterPrefab.transform.GetChild(0).GetChild(i).transform.position;
            queuePositionList[queueIndex].Add(pos);
        }
    }

    public void GenerateQueueLocation()
    {
        //declare queuePositionList array
        queuePositionList = new List<Vector3>[unlockedQueue];

        //spawn queue holder
        //assign queue position list
        for (int i = 0; i < unlockedQueue; i++)
        {
            //instantiate queue prefab
            GameObject newQueueHolder = Instantiate(queueHolder, startLocation, Quaternion.identity) as GameObject;
            //set queue prefab parent
            newQueueHolder.transform.SetParent(this.gameObject.transform, false);
            //set queue index into QueueInteraction
            newQueueHolder.GetComponent<QueueInteraction>().queueIndex = i;

            //get queue position
            queuePositionList[i] = new List<Vector3>();
            for (int j = 0; j < newQueueHolder.transform.childCount; j++)
            {
                Vector3 pos = newQueueHolder.transform.GetChild(j).transform.position;
                queuePositionList[i].Add(pos);
            }
        }
    }

    public void CounterInteraction(int queueIndex)
    {
        List<CustomerClass> currentQueue = CustomerHandler.Instance.customerClassInQueueList[queueIndex];

        /////////////////////
        //if player holding a potion
        if(PlayerInfoHandler.Instance.playerPotionHolderList.Count != 0)
        {
            Debug.Log("Serve");

            //if holding customer prefer potion
            int _temp = 0;
            int holderCount = PlayerInfoHandler.Instance.playerPotionHolderList.Count;
            for (int i = 0; i < PlayerInfoHandler.Instance.playerPotionHolderList.Count; i++)
            {
                if (currentQueue[0].preferPotion == PlayerInfoHandler.Instance.playerPotionHolderList[i])
                {
                    ServeCustomer(currentQueue, i);
                    _temp = 0;
                    i = 3; //quit this loop
                }
                else
                {
                    _temp++;
                }
            }
            
            if(_temp == holderCount)
            {
                NotificationSystem.Instance.SendPopOutNotification("Your didn't have ordered potion!");
            }
            //refresh customer's destination in queue
            CustomerHandler.Instance.RefreshCustomerQueuePos(currentQueue, queueIndex);
        }
        else //if player did not holding potion
        {

        }
    }

    public void ServeCustomer(List<CustomerClass> currentQueue, int soldPotionIndex)
    {
        ///GET POTION DATA
        PotionData _currentPotionData = StageManager.potionListToday[PlayerInfoHandler.Instance.playerPotionHolderList[soldPotionIndex]];

        ///RESET PLAYER POTION HOLDER
        PlayerInfoHandler.Instance.playerPotionHolderList.RemoveAt(soldPotionIndex);
        
        ///GET CUSTOMER CLASS
        //get customer class
        CustomerClass currentCustomer = currentQueue[0];

        ///ADD CASH
        if (ElementMeterPanel.Instance.elementSkillRemaining[3] > 0) //if aer skill able to active
        {
            ///ADD CASH x2
            PlayerProfile.cashTotal += _currentPotionData.potionPrice * 2;
            ElementMeterPanel.Instance.elementSkillRemaining[3]--;
        }
        else
        {
            ///ADD CASH 
            PlayerProfile.cashTotal += _currentPotionData.potionPrice;
        }

        ///ADD XP
        if (ElementMeterPanel.Instance.elementSkillRemaining[3] > 0) //if aer skill able to active
        {
            ///CUSTOMER ADD XP
            CustomerAddXP(currentCustomer, 2);
            ElementMeterPanel.Instance.elementSkillRemaining[3]--;
        }
        else
        {
            ///CUSTOMER ADD XP
            CustomerAddXP(currentCustomer, 1);
        }

        ///REMOVE CUSTOMER 
        //remove customer from global customer class list
        CustomerHandler.Instance.customerClassList.Remove(currentCustomer);
        //remove customer from queue
        currentQueue.RemoveAt(0);
        //customer total - 1;
        CustomerHandler.Instance.currentCustomerTotal--;

        ///SET CUSTOMER CLASS TO DELETATION
        //set isCustomer to false
        currentCustomer.isCustomer = false;
        //set customer destination to customer deletation
        int temp = Random.Range(0, 2);
        currentCustomer.NewDestination(CustomerHandler.Instance.customerDeletation[temp].position);
    }

    public void CustomerAddXP(CustomerClass customerClass, int _multiplier)
    {
        ///GET CUSTOMER DATA
        CustomerData customerData = customerClass.customerData;
        ///GET CUSTOMER PROFILE
        CustomerProfile currentCustomerProfile = PlayerProfile.customerProfile[customerData.customerIndex];

        //get customer level
        int customerLevel = currentCustomerProfile.customerLevel;
        if (customerLevel < 6) //if customer havent max level
        {
            ///ADD XP
            currentCustomerProfile.customerExperience += 1 * _multiplier;

            ///CHECK LEVEL UP
            if (currentCustomerProfile.customerExperience >= customerData.levelingExperience[customerLevel - 1])
            {
                CustomerLevelUp(currentCustomerProfile, customerData);
            }
        }
    }

    public void CustomerLevelUp(CustomerProfile currentCustomerProfile, CustomerData customerData)
    {
        //subtract leveling up experience
        currentCustomerProfile.customerExperience -= (int)customerData.levelingExperience[currentCustomerProfile.customerLevel - 1];
        //leveling up
        currentCustomerProfile.customerLevel += 1;

        ///UNLOCK ELEMENT
        customerData.LevelUpUnlock(currentCustomerProfile.customerLevel);
    }
}
