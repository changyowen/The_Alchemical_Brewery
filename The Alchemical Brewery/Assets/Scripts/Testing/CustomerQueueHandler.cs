using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerQueueHandler : MonoBehaviour
{
    public static CustomerQueueHandler Instance { get; private set; }

    public List<Vector3>[] queuePositionList;
    public GameObject queueHolder;
    public int unlockedQueue = 1;
    public int queueLength = 6;
    public Vector3 startLocation;
    public float queueLocationSpace = 1f;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        GenerateQueueLocation();
    }

    void GenerateQueueLocation()
    {
        //declare queuePositionList array
        queuePositionList = new List<Vector3>[unlockedQueue];

        //spawn queue holder
        //assign queue position list
        for (int i = 0; i < unlockedQueue; i++)
        {
            GameObject newQueueHolder = Instantiate(queueHolder, startLocation, Quaternion.identity) as GameObject;
            newQueueHolder.transform.SetParent(this.gameObject.transform, false);
            queuePositionList[i] = new List<Vector3>();
            for (int j = 0; j < newQueueHolder.transform.childCount; j++)
            {
                Vector3 pos = newQueueHolder.transform.GetChild(j).transform.position;
                queuePositionList[i].Add(pos);
            }
        }
    }

    public void CounterInteraction(int queueIndex)
    {
        List<CustomerClass> currentQueue = CustomerHandler.Instance.customerClassInQueueList[queueIndex];

        /////////////////////
        

    }
}
