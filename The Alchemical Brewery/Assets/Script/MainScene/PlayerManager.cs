using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PlayerManager : MonoBehaviour
{
    public float playerSpeed;
    public int objectHandling;
    public bool reachDestination = false;
    public int holdObject = 0;

    public GameObject clickableManager;
    AIPath aiPath;
    Clickable_script clickableScript;
    public GameObject customerQueue_gameobject;
    CustomerQueue customerQueue;

    // Start is called before the first frame update
    void Start()
    {
        aiPath = GetComponent<AIPath>();
        clickableScript = clickableManager.GetComponent<Clickable_script>();
        customerQueue = customerQueue_gameobject.GetComponent<CustomerQueue>();
    }

    // Update is called once per frame
    void Update()
    {

        //test
        Debug.Log(holdObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "a1":
                {
                    break;
                }
            case "a2":
                {
                    break;
                }
            case "a3":
                {
                    break;
                }
            case "b1":
                {
                    holdObject = 1;
                    col.enabled = false;
                    break;
                }
            case "b2":
                {
                    holdObject = 2;
                    col.enabled = false;
                    break;
                }
            case "b3":
                {
                    holdObject = 3;
                    col.enabled = false;
                    break;
                }
            case "b4":
                {
                    holdObject = 4;
                    col.enabled = false;
                    break;
                }
            case "b5":
                {
                    holdObject = 5;
                    col.enabled = false;
                    break;
                }
            case "b6":
                {
                    holdObject = 6;
                    col.enabled = false;
                    break;
                }
            case "c1":
                {
                    holdObject = 7;
                    col.enabled = false;
                    break;
                }
            case "c2":
                {
                    holdObject = 8;
                    col.enabled = false;
                    break;
                }
            case "c3":
                {
                    holdObject = 9;
                    col.enabled = false;
                    break;
                }
            case "c4":
                {
                    holdObject = 10;
                    col.enabled = false;
                    break;
                }
            case "c5":
                {
                    holdObject = 11;
                    col.enabled = false;
                    break;
                }
            case "c6":
                {
                    holdObject = 12;
                    col.enabled = false;
                    break;
                }
            case "e1":
                {
                    ServeCustomer(0);
                    col.enabled = false;
                    break;
                }
            case "e2":
                {
                    ServeCustomer(1);
                    col.enabled = false;
                    break;
                }
            case "e3":
                {
                    ServeCustomer(2);
                    col.enabled = false;
                    break;
                }
            case "e4":
                {
                    ServeCustomer(3);
                    col.enabled = false;
                    break;
                }
            case "e5":
                {
                    ServeCustomer(4);
                    col.enabled = false;
                    break;
                }
        }
    }

    void ServeCustomer(int index)
    {
        CustomerAttribute customerAttribute = customerQueue.CustomerList[index].GetComponent<CustomerAttribute>();
        int preferablePotion = customerAttribute.preferablePotion;
        if(customerAttribute.customerStatus == 2)
        {
            if (holdObject == preferablePotion)
            {
                customerAttribute.customerStatus++;
                ////
                DailyStart.dailyServedCustomer++;
                DailyStart.dailyEarnedMoney += 10;
                ////
                customerQueue.RemovingCustomer(index);
                holdObject = 0;
            }
        }
    }
}
