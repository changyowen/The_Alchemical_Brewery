using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerQueue : MonoBehaviour
{
    List<Vector3> positionList = new List<Vector3>();
    List<GameObject> CustomerList = new List<GameObject>();
    public Transform[] QueuePosition;
    public Transform[] Queue2Position;

    public GameObject CustomerGameObject;
    public GameObject CustomerParent;
    public Transform SpawnedLocation;
    public Transform DeletePoint;

    bool generatingCustomer = false;
    bool StartingGame = true;

    void Start()
    {
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
        spawnedCustomer.GetComponent<CustomerAttribute>().customerIndex = CustomerList.IndexOf(spawnedCustomer);
        spawnedCustomer.GetComponent<CustomerAttribute>().destination = Queue2Position[CustomerList.IndexOf(spawnedCustomer)].position;
        spawnedCustomer.GetComponent<CustomerAttribute>().QueuePoint = QueuePosition[CustomerList.IndexOf(spawnedCustomer)].position;
        spawnedCustomer.GetComponent<CustomerAttribute>().QueuePoint2 = Queue2Position[CustomerList.IndexOf(spawnedCustomer)].position;
        spawnedCustomer.GetComponent<CustomerAttribute>().deletePoint = DeletePoint.position;
        yield return new WaitForSeconds(3f);
        generatingCustomer = false;
    }

    void RestockingCustomer(int index)
    {
        GameObject spawnedCustomer = Instantiate(CustomerGameObject, SpawnedLocation.position, Quaternion.identity) as GameObject;
        spawnedCustomer.transform.parent = CustomerParent.transform;
        CustomerList.Insert(index, spawnedCustomer);
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
}
