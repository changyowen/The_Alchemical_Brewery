using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerClass 
{
    public CustomerData customerData;
    public int customerIndex;
    public Sprite customerSprite;
    public GameObject customer_gameObj;
    public NavMeshAgent customerNavMeshAgent;

    public bool isCustomer = false;
    public bool joinedQueue = false;
    public bool reachedQueuePos = false;

    public Vector3 customerDestination;

    //declare customer data
    public void CustomerDeclaration(CustomerData customerData_, GameObject customer_gameObj_)
    {
        //get customer data scriptable object
        customerData = customerData_;

        //assign gameObj
        customer_gameObj = customer_gameObj_;

        //assign navMesh agent
        customerNavMeshAgent = customer_gameObj.GetComponent<NavMeshAgent>();
    }

    //define is customer or villager
    public void CustomerDefine(bool isCustomer_)
    {
        isCustomer = isCustomer_;
    }

    public void NewDestination(Vector3 newDestination)
    {
        customerDestination = newDestination;
        customerNavMeshAgent.destination = customerDestination;
        //customerNavMeshAgent.Resume();
    }

    public void JoinQueue(Vector3 newDestination)
    {
        joinedQueue = true;
        NewDestination(newDestination);
    }

}
