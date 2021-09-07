using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationText_handler : MonoBehaviour
{
    public void OnDestroyThis()
    {
        Destroy(this.gameObject);
    }
}
