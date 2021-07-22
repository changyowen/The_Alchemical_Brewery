using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPhysic : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        checkPhysic();   
    }

    void checkPhysic()
    {
        GameObject physicUseParent = transform.parent.transform.parent.gameObject;

        if(physicUseParent != null)
        {
            if (physicUseParent.name == "physic use")
            {
                GetComponent<LookToCamera>().enabled = false;
                transform.localRotation = Quaternion.Euler(Vector3.zero);
            }
            else
            {
                GetComponent<LookToCamera>().enabled = true;
            }
        }
        else
        {
            GetComponent<LookToCamera>().enabled = true;
        }
    }
}
