using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerInformationHandler : MonoBehaviour
{
    public CustomerClass customerClass = null;

    public SpriteRenderer sr;

    public GameObject _customerCustomer;

    void Start()
    {
        sr.sprite = customerClass.customerSprite;
    }

    private void Update()
    {
        if(customerClass != null)
        {
            if(customerClass.reachedQueuePos)
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
