using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerInformationHandler_ChoosePotionScene : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    public CustomerData customerDataSO;

    public GameObject customerCanvas;

    bool reachedDestination = false;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        CheckDestinationDistance();
        CustomerCanvasUpdate();
    }

    void CheckDestinationDistance()
    {
        if (Vector3.Distance(transform.position, navMeshAgent.destination) <= 0.1)
        {
            reachedDestination = true;
        }
        else
        {
            reachedDestination = false;
        }
    }

    void CustomerCanvasUpdate()
    {
        if(!reachedDestination) //if havent reach destination, disable customer canvas
        {
            customerCanvas.SetActive(false);
        }
        else //if reached destination, activate customer canvas
        {
            customerCanvas.SetActive(true);
        }
    }
}
