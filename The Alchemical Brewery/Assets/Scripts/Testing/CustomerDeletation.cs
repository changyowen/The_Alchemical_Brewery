using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerDeletation : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Customer")
        {
            CustomerHandler.Instance.CustomerDelete(col.gameObject);
        }
    }
}
