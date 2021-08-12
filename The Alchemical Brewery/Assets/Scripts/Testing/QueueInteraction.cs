using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueInteraction : MonoBehaviour
{
    public int queueIndex = 0;
    public float queueRefreshTime = 1f;
    public Vector3 counterPositionOffset;

    bool queueInteractionEnable = false;
    float queueTimer = 0;

    void Update()
    {
        QueueTimerHandler(); //update queue timer
        QueueInteractionEnableChack(); //update queue enabled
    }

    void OnMouseDown()
    {
        //if player still far from counter
        Vector3 counterPosition = transform.position + counterPositionOffset;
        Debug.Log(counterPosition);
        float dist = Vector3.Distance(PlayerInfoHandler.Instance.playerPosition, counterPosition);
        if (dist > 3f)
        {
            //go to the nearest point toward collider
            Collider col = GetComponent<Collider>();
            Vector3 closestPoint = col.ClosestPoint(PlayerInfoHandler.Instance.playerPosition);
            RayCastMovement.Instance.NewDestination(counterPosition);
        }
        else //if player already near the counter
        {
            //stop moving
            RayCastMovement.Instance.NewDestination(PlayerInfoHandler.Instance.playerPosition);

            //check queue is enabled
            if(queueInteractionEnable)
            {
                //Start interaction
                CustomerQueueHandler.Instance.CounterInteraction(queueIndex);
                queueTimer = 0;
            }
        }
    }

    void QueueTimerHandler()
    {
        if(queueTimer < queueRefreshTime)
        {
            queueTimer += Time.deltaTime;
        }
    }

    void QueueInteractionEnableChack()
    {
        //check if first customer in queue reach position
        if (CustomerHandler.Instance.customerClassInQueueList[queueIndex].Count != 0)
        {
            CustomerClass firstCustomer = CustomerHandler.Instance.customerClassInQueueList[queueIndex][0];

            if (firstCustomer.reachedQueuePos && queueTimer >= queueRefreshTime)
            {
                queueInteractionEnable = true;
            }
            else
            {
                queueInteractionEnable = false;
            }
        }
    }
}
