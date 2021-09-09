using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerInformationHandler : MonoBehaviour
{
    NavMeshAgent agent;

    public CustomerClass customerClass = null;

    public SpriteRenderer sr;

    public GameObject _customerCustomer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        sr.sprite = customerClass.customerSprite;
    }

    private void Update()
    {
        if(customerClass != null)
        {
            if(agent.remainingDistance <= 0.2f)
            {
                _customerCustomer.SetActive(true);
            }
            else
            {
                _customerCustomer.SetActive(false);
            }
        }
    }
}
