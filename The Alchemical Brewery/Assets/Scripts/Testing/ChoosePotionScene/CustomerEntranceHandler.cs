using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerEntranceHandler : MonoBehaviour
{
    public static CustomerEntranceHandler Instance { get; private set; }

    public ScriptableObjectHolder SO_holder;

    public GameObject customer_obj;
    public Transform customerHolder_transform;
    public Transform spawnPoint_transform;
    public Transform queue_transform;

    //List<GameObject> customerObjList = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator SpawningCustomer()
    {
        ///manually assign customer type today (IF NULL)
        if(ChoosePotionSceneManager.Instance.customerTypeToday == null)
        {
            //get this round customer index
            List<int> customerIndexList = new List<int> { 0, 1, 2, 3, 4 };
            //assign each customer data
            ChoosePotionSceneManager.Instance.customerTypeToday = new List<CustomerData>();
            for (int i = 0; i < customerIndexList.Count; i++)
            {
                ChoosePotionSceneManager.Instance.customerTypeToday.Add(SO_holder.customerDataSO[customerIndexList[i]]);
            }
        }
       

        for (int i = 0; i < ChoosePotionSceneManager.Instance.customerTypeToday.Count; i++)
        {
            //spawn customer
            GameObject newCustomerObj = Instantiate(customer_obj, spawnPoint_transform.position, Quaternion.identity) as GameObject;
            //set parent
            newCustomerObj.transform.SetParent(customerHolder_transform, false);

            //assign SO holder
            CustomerInformationHandler_ChoosePotionScene currentCustomerInfomation = newCustomerObj.GetComponent<CustomerInformationHandler_ChoosePotionScene>();
            currentCustomerInfomation.customerDataSO = ChoosePotionSceneManager.Instance.customerTypeToday[i];
            //assign customer index(from customerTodayList)
            currentCustomerInfomation._customerIndex_fromlist = i;

            //set destination
            newCustomerObj.GetComponent<NavMeshAgent>().SetDestination(queue_transform.GetChild(i).position);

            yield return new WaitForSeconds(0.8f);
        }
    }
}
