﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerAttribute : MonoBehaviour
{
    CustomerQueue customerQueue;
    DailyStart dailyStart;

    GameObject preferablePotion_object;

    public int customerIndex = 0;
    public int customerStatus = 0;
    public bool enableWalking = true;
    public Vector3 destination;
    public Vector3 QueuePoint, QueuePoint2;
    public Vector3 deletePoint;
    public float speed;
    private float QueueTimer = 0;
    public int preferablePotion;
    public float waitingTime;

    void Start()
    {
        GameObject customerQueue_object = GameObject.Find("CustomerQueue");
        GameObject dailySystem_object = GameObject.Find("DailySystem");
        customerQueue = customerQueue_object.GetComponent<CustomerQueue>();
        dailyStart = dailySystem_object.GetComponent<DailyStart>();
        preferablePotion_object = transform.GetChild(0).gameObject;
        preferablePotion_object.SetActive(false);
    }

    void Update()
    {
        switch(customerStatus)
        {
            case 0:
                {
                    if (transform.position == destination)
                    {
                        destination = QueuePoint;
                        customerStatus++;
                    }
                    break;
                }
            case 1:
                {
                    if(transform.position == destination)
                    {
                        enableWalking = false;
                        preferablePotion_object.SetActive(true);
                        customerStatus++;
                    }
                    break;
                }
            case 2:
                {
                    QueueTimer += Time.deltaTime;
                    if (QueueTimer > waitingTime)
                    {
                        customerQueue.RemovingCustomer(customerIndex);
                        destination = QueuePoint2;
                        /////
                        if(dailyStart.startEndScene == false)
                        {
                            DailyStart.dailyAngryCustomer++;
                        }
                        /////
                        customerStatus++;
                    }
                    break;
                }
            case 3:
                {
                    preferablePotion_object.SetActive(false);
                    enableWalking = true;
                    if (transform.position == destination)
                    {
                        destination = deletePoint;
                        customerStatus++;
                    }
                    break;
                }
            case 4:
                {
                    if (transform.position == destination)
                    {
                        Destroy(this.gameObject);
                    }
                    break;
                }
        }

        if (enableWalking == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        }
    }
}
