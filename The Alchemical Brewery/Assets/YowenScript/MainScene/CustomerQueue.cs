using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerQueue : MonoBehaviour
{
    public GameObject DailySystem_gameObject;
    DailyStart dailyStart;

    List<Vector3> positionList = new List<Vector3>();
    public List<GameObject> CustomerList = new List<GameObject>();
    public Transform[] QueuePosition;
    public Transform[] Queue2Position;

    public GameObject CustomerGameObject;
    public Customer[] customerScriptableObject;
    public GameObject CustomerParent;
    public Transform SpawnedLocation;
    public Transform DeletePoint;

    bool generatingCustomer = false;
    bool StartingGame = true;

    void Start()
    {
        dailyStart = DailySystem_gameObject.GetComponent<DailyStart>();

        for(int i = 0; i < 5; i++)
        {
            positionList.Add(QueuePosition[i].position);
        }
    }

    void Update()
    { 
        if(StartingGame == true)
        {
            if (CustomerList.Count < 5)
            {
                if (generatingCustomer == false)
                {
                    generatingCustomer = true;
                    StartCoroutine(AddCustomer(true, 0));
                }
            }
            else
            {
                StartingGame = false;
            }
        }
    }

    IEnumerator AddCustomer(bool startGame, int index)
    {
        GameObject spawnedCustomer = Instantiate(CustomerGameObject, SpawnedLocation.position, Quaternion.identity) as GameObject;
        spawnedCustomer.transform.parent = CustomerParent.transform;
        CustomerList.Add(spawnedCustomer);

        int randomNum = Random.Range(0, 3);
        SetCustomerType(spawnedCustomer, randomNum);

        spawnedCustomer.GetComponent<CustomerAttribute>().customerIndex = CustomerList.IndexOf(spawnedCustomer);
        spawnedCustomer.GetComponent<CustomerAttribute>().destination = Queue2Position[CustomerList.IndexOf(spawnedCustomer)].position;
        spawnedCustomer.GetComponent<CustomerAttribute>().QueuePoint = QueuePosition[CustomerList.IndexOf(spawnedCustomer)].position;
        spawnedCustomer.GetComponent<CustomerAttribute>().QueuePoint2 = Queue2Position[CustomerList.IndexOf(spawnedCustomer)].position;
        spawnedCustomer.GetComponent<CustomerAttribute>().deletePoint = DeletePoint.position;
        yield return new WaitForSeconds(15f);
        generatingCustomer = false;
    }

    void RestockingCustomer(int index)
    {
        GameObject spawnedCustomer = Instantiate(CustomerGameObject, SpawnedLocation.position, Quaternion.identity) as GameObject;
        spawnedCustomer.transform.parent = CustomerParent.transform;
        CustomerList.Insert(index, spawnedCustomer);

        int randomNum = Random.Range(0, 3);
        SetCustomerType(spawnedCustomer, randomNum);

        spawnedCustomer.GetComponent<CustomerAttribute>().customerIndex = CustomerList.IndexOf(spawnedCustomer);
        spawnedCustomer.GetComponent<CustomerAttribute>().destination = Queue2Position[CustomerList.IndexOf(spawnedCustomer)].position;
        spawnedCustomer.GetComponent<CustomerAttribute>().QueuePoint = QueuePosition[CustomerList.IndexOf(spawnedCustomer)].position;
        spawnedCustomer.GetComponent<CustomerAttribute>().QueuePoint2 = Queue2Position[CustomerList.IndexOf(spawnedCustomer)].position;
        spawnedCustomer.GetComponent<CustomerAttribute>().deletePoint = DeletePoint.position;
        generatingCustomer = false;
    }

    public void RemovingCustomer(int index)
    {
        CustomerList.RemoveAt(index);
        RestockingCustomer(index);
    }

    void SetCustomerType(GameObject spawnedCustomer, int r)
    {
        spawnedCustomer.name = customerScriptableObject[r].Name;
        spawnedCustomer.GetComponent<CustomerAttribute>().preferablePotion = customerScriptableObject[r].preferablePotion;
        spawnedCustomer.GetComponent<CustomerAttribute>().waitingTime = customerScriptableObject[r].waitingTime;
        GameObject childObject = Instantiate(customerScriptableObject[r].customertype) as GameObject;
        childObject.transform.parent = spawnedCustomer.transform;
        childObject.transform.position = spawnedCustomer.transform.position;
    }
}
