using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerInformationHandler : MonoBehaviour
{
    public CustomerClass customerClass = null;

    public SpriteRenderer sr;

    void Start()
    {
        sr.sprite = customerClass.customerSprite;
    }
}
