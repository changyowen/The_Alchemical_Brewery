using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class CustomerHandler : MonoBehaviour
{
    public static CustomerHandler Instance { get; private set; }

    public List<CustomerClass> customerClassList = new List<CustomerClass>();
    public List<CustomerClass>[] customerClassInQueueList;

    public GameObject customer_obj;
    public bool spawnVillagerBool = true;
    public bool spawnCustomerBool = true;
    public float villagerSpawnTime = 3f;
    public float customerSpawnTime = 8f;
    public Transform[] customerSpawnPoint;
    public Transform[] customerDeletation;
    public float customerSpawnRandomZ = 5f;

    int maximumCustomerTotal;
    public int currentCustomerTotal = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //DeclareCustomerClass();
        if(StageManager.dayTimeGameplay)
        {
            StartCoroutine(GenerateVillager());
            StartCoroutine(GenerateCustomer());
        }
    }

    void Update()
    {
        if(currentCustomerTotal < maximumCustomerTotal)
        {
            spawnCustomerBool = true;
        }
        else
        {
            spawnCustomerBool = false;
        }

        CustomerReachedDestinationUpdate();
        if(customerClassList.Count > 0)
        {
        }
    }

    void CustomerReachedDestinationUpdate()
    {
        for (int i = 0; i < customerClassInQueueList.Length; i++)
        {
            for (int j = 0; j < customerClassInQueueList[i].Count; j++)
            {
                CustomerClass currentCustomer = customerClassInQueueList[i][j];
                Vector3 customerPos = currentCustomer.customer_gameObj.transform.position;
                Vector3 customerDest = currentCustomer.customerDestination;

                float dist = Vector3.Distance(customerDest, customerPos);
                if (dist <= .1f)
                {
                    currentCustomer.reachedQueuePos = true;
                }
                else
                {
                    currentCustomer.reachedQueuePos = false;
                }
            }
        }
    }

    //for declaring customer queue list, maximum customer total
    public void DeclareCustomerClass(int unlockedQueue, int queueLength)
    {
        //declare customerClassInQueueList array
        customerClassInQueueList = new List<CustomerClass>[unlockedQueue];
        //declare customerClassList list
        for (int i = 0; i < unlockedQueue; i++)
        {
            customerClassInQueueList[i] = new List<CustomerClass>();
        }

        //assign maximum customer total
        maximumCustomerTotal = queueLength * unlockedQueue;
    }

    IEnumerator GenerateCustomer()
    {
        while(spawnCustomerBool)
        {
            //current customer total + 1
            currentCustomerTotal++;

            ///Spawn Customer Prefab
            //random choose spawn point index
            int choosenSpawnPoint = Random.Range(0, 2);
            //get spawn point from random z point
            float randomZ = Random.Range(-(customerSpawnRandomZ), customerSpawnRandomZ);
            Vector3 newSpawnPoint = customerSpawnPoint[choosenSpawnPoint].position + new Vector3(0, 0, randomZ);
            //spawn customer at new spawn point
            GameObject newCustomer = Instantiate(customer_obj, newSpawnPoint, Quaternion.identity) as GameObject;
            //set parent
            Transform customerHolder = this.gameObject.transform.GetChild(0).gameObject.transform;
            newCustomer.transform.SetParent(customerHolder, false);

            ///Get customer type based on appearance rate
            int getIndex = CustomerTypeWeightedRandom();
            CustomerData currentCustomerData = StageManager.Instance.customerTypeToday[getIndex];

            ///Declare Customer Class
            CustomerClass newCustomerClass = new CustomerClass();
            newCustomerClass.CustomerDeclaration(currentCustomerData, newCustomer);
            newCustomerClass.CustomerDefine(true);
            //assign customer class to prefab's information handler
            newCustomer.GetComponent<CustomerInformationHandler>().customerClass = newCustomerClass;

            //assign into customer class list
            customerClassList.Add(newCustomerClass);

            //assign prefer potion
            int temp = Random.Range(0, StageManager.potionListToday.Count);
            newCustomerClass.preferPotion = temp;

            //get customer navmesh agent
            NavMeshAgent customerNavMesh = newCustomerClass.customerNavMeshAgent;
            //set destination
            switch (choosenSpawnPoint)
            {
                case 0: //if spawn at left
                    {
                        Vector3 newDestination = customerDeletation[1].position + new Vector3(0, 0, randomZ);
                        newCustomerClass.NewDestination(newDestination);
                        break;
                    }
                case 1: //if spawn at right
                    {
                        Vector3 newDestination = customerDeletation[0].position + new Vector3(0, 0, randomZ);
                        newCustomerClass.NewDestination(newDestination);
                        break;
                    }
            }
            yield return new WaitForSeconds(customerSpawnTime);
            yield return null;
        }
    }

    IEnumerator GenerateVillager()
    {
        while (spawnVillagerBool)
        {
            //random choose spawn point index
            int choosenSpawnPoint = Random.Range(0, 2);
            //get spawn point from random z point
            float randomZ = Random.Range(-(customerSpawnRandomZ), customerSpawnRandomZ);
            Vector3 newSpawnPoint = customerSpawnPoint[choosenSpawnPoint].position + new Vector3(0, 0, randomZ);
            //spawn customer at new spawn point
            GameObject newCustomer = Instantiate(customer_obj, newSpawnPoint, Quaternion.identity) as GameObject;
            //set parent
            Transform villagerHolder = this.gameObject.transform.GetChild(1).gameObject.transform;
            newCustomer.transform.SetParent(villagerHolder, false);

            ///Get customer type based on appearance rate
            int getIndex = CustomerTypeWeightedRandom();
            CustomerData currentCustomerData = StageManager.Instance.customerTypeToday[getIndex];

            ///Declare Customer Class
            CustomerClass newCustomerClass = new CustomerClass();
            newCustomerClass.CustomerDeclaration(currentCustomerData, newCustomer);
            newCustomerClass.CustomerDefine(false);

            //assign customer class to prefab's information handler
            newCustomer.GetComponent<CustomerInformationHandler>().customerClass = newCustomerClass;
            //assign into customer class list
            customerClassList.Add(newCustomerClass);
            
            //get customer navmesh agent
            NavMeshAgent customerNavMesh = newCustomerClass.customerNavMeshAgent;
            //set destination
            switch (choosenSpawnPoint)
            {
                case 0: //if spawn at left
                    {
                        Vector3 newDestination = customerDeletation[1].position + new Vector3(0, 0, randomZ);
                        newCustomerClass.NewDestination(newDestination);
                        break;
                    }
                case 1: //if spawn at right
                    {
                        Vector3 newDestination = customerDeletation[0].position + new Vector3(0, 0, randomZ);
                        newCustomerClass.NewDestination(newDestination);
                        break;
                    }
            }
            yield return new WaitForSeconds(villagerSpawnTime);
            yield return null;
        }
    }

    int CustomerTypeWeightedRandom()
    {
        //get customerAppearRateList
        List<float> customerAppearRateList = StageManager.Instance.customerAppearRateList;
        //get total customer appear rate
        float totalAppearRate = StageManager.Instance.totalCustomerAppearRate;
        //get random weight
        float randomWeight = Random.Range(0, totalAppearRate);
        //loop all customer data and return one
        for (int i = 0; i < customerAppearRateList.Count; i++)
        {
            if (randomWeight < customerAppearRateList[i])
            {
                return i;
            }
            else
            {
                randomWeight -= customerAppearRateList[i];
            }
        }
        return 0;
    }

    void AssigningNewCustomerToQueue(CustomerClass customerClass)
    {
        if(customerClass.isCustomer)
        {
            if (!customerClass.joinedQueue)
            {
                //get each queue length
                List<int> eachQueueLength = new List<int>();
                for (int i = 0; i < customerClassInQueueList.Length; i++)
                {
                    eachQueueLength.Add(customerClassInQueueList[i].Count);
                }
                //get index of smallest queue length
                int minVal = eachQueueLength.Min();
                int leastQueueIndex = eachQueueLength.IndexOf(minVal);

                //assign customer class into smallest queue
                customerClassInQueueList[leastQueueIndex].Add(customerClass);

                //get queue position vector3
                Vector3 queuePosition = CustomerQueueHandler.Instance.queuePositionList[leastQueueIndex][minVal];
                Debug.Log(queuePosition);
                //customer join queue
                customerClass.JoinQueue(queuePosition);
            }
        }
        else
        {

        }
    }

    public void RefreshCustomerQueuePos(List<CustomerClass> currentQueue, int queueIndex)
    {
        for (int i = 0; i < currentQueue.Count; i++)
        {
            //get queue position vector3
            Vector3 queuePosition = CustomerQueueHandler.Instance.queuePositionList[queueIndex][i];
            currentQueue[i].JoinQueue(queuePosition);
        }
    }

    public void ClearAllCustomer()
    {
        
    }

    public void CustomerDelete(GameObject customer_obj_)
    {
        for (int i = 0; i < customerClassList.Count; i++)
        {
            if(customer_obj_ == customerClassList[i].customer_gameObj)
            {
                customerClassList.RemoveAt(i);
                Destroy(customer_obj_);
                i = customerClassList.Count;
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Customer")
        {
            CustomerClass customerClass = col.gameObject.GetComponent<CustomerInformationHandler>().customerClass;
            AssigningNewCustomerToQueue(customerClass);
        }
    }
}
