using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testingspawn : MonoBehaviour
{
    public GameObject gb;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject oj = Instantiate(gb, transform.position, Quaternion.identity) as GameObject;
        }
    }
}
