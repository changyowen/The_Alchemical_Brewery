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
    public Vector3[] customerSpawnPoint;
    public float customerSpawnRandomZ = 5f;

    int maximumCustomerTotal;
    int currentCustomerTotal = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        DeclareCustomerClass();
        StartCoroutine(GenerateVillager());
        StartCoroutine(GenerateCustomer());
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
    void DeclareCustomerClass()
    {
        //get total unlocked queue
        int unlockedQueue = CustomerQueueHandler.Instance.unlockedQueue;
        int queueLength = CustomerQueueHandler.Instance.queueLength;

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

            //random choose spawn point index
            int choosenSpawnPoint = Random.Range(0, 2);
            //get spawn point from random z point
            float randomZ = Random.Range(-(customerSpawnRandomZ), customerSpawnRandomZ);
            Vector3 newSpawnPoint = customerSpawnPoint[choosenSpawnPoint] + new Vector3(0, 0, randomZ);
            //spawn customer at new spawn point
            GameObject newCustomer = Instantiate(customer_obj, newSpawnPoint, Quaternion.identity) as GameObject;
            //set parent
            Transform customerHolder = this.gameObject.transform.GetChild(0).gameObject.transform;
            newCustomer.transform.SetParent(customerHolder, false);

            //declare new customer class
            CustomerClass newCustomerClass = new CustomerClass();
            //FIXXXXX
            newCustomerClass.CustomerDeclaration(null, newCustomer);
            newCustomerClass.CustomerDefine(true);
            //assign into customer class list
            customerClassList.Add(newCustomerClass);

            //get customer navmesh agent
            NavMeshAgent customerNavMesh = newCustomerClass.customerNavMeshAgent;
            //set destination
            switch (choosenSpawnPoint)
            {
                case 0: //if spawn at left
                    {
                        Vector3 newDestination = newSpawnPoint + new Vector3(120, 0, 0);
                        //customerNavMesh.SetDestination(newDestination);
                        //customerNavMesh.destination = newDestination;
                        //customerNavMesh.Resume();
                        newCustomerClass.NewDestination(newDestination);
                        break;
                    }
                case 1: //if spawn at right
                    {
                        Vector3 newDestination = newSpawnPoint + new Vector3(-120, 0, 0);
                        //customerNavMesh.SetDestination(newDestination);
                        //customerNavMesh.destination = newDestination;
                        //customerNavMesh.Resume();
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
            Vector3 newSpawnPoint = customerSpawnPoint[choosenSpawnPoint] + new Vector3(0, 0, randomZ);
            //spawn customer at new spawn point
            GameObject newCustomer = Instantiate(customer_obj, newSpawnPoint, Quaternion.identity) as GameObject;
            //set parent
            Transform villagerHolder = this.gameObject.transform.GetChild(1).gameObject.transform;
            newCustomer.transform.SetParent(villagerHolder, false);

            //declare new customer class
            CustomerClass newCustomerClass = new CustomerClass();
            //FIXXXXX
            newCustomerClass.CustomerDeclaration(null, newCustomer);
            newCustomerClass.CustomerDefine(false);
            //assign into customer class list
            customerClassList.Add(newCustomerClass);
            
            //get customer navmesh agent
            NavMeshAgent customerNavMesh = newCustomerClass.customerNavMeshAgent;
            //set destination
            switch (choosenSpawnPoint)
            {
                case 0: //if spawn at left
                    {
                        Vector3 newDestination = newSpawnPoint + new Vector3(120, 0, 0);
                        //customerNavMesh.SetDestination(newDestination);
                        //customerNavMesh.destination = newDestination;
                        //customerNavMesh.Resume();
                        newCustomerClass.NewDestination(newDestination);
                        break;
                    }
                case 1: //if spawn at right
                    {
                        Vector3 newDestination = newSpawnPoint + new Vector3(-120, 0, 0);
                        //customerNavMesh.SetDestination(newDestination);
                        //customerNavMesh.destination = newDestination;
                        //customerNavMesh.Resume();
                        newCustomerClass.NewDestination(newDestination);
                        break;
                    }
            }
            yield return new WaitForSeconds(villagerSpawnTime);
            yield return null;
        }
    }

    void AssigningNewCustomerToQueue(GameObject customer_gameObj)
    {
        //get the exactly customer class by checking its game object
        int getCustomerClassIndex = 0;
        for (int i = 0; i < customerClassList.Count; i++)
        {
            if(customerClassList[i].customer_gameObj == customer_gameObj)
            {
                getCustomerClassIndex = i;
                i = customerClassList.Count; //stop the for loop
            }
        }
        CustomerClass customerClass = customerClassList[getCustomerClassIndex];

        if(customerClass.isCustomer)
        {
            if(!customerClass.joinedQueue)
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
                //customer join queue
                customerClass.JoinQueue(queuePosition);
            }
        }
        else
        {
        }
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
            AssigningNewCustomerToQueue(col.gameObject);
        }
    }
}
