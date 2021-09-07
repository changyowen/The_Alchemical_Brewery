using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotificationSystem : MonoBehaviour
{
    public static NotificationSystem Instance { get; private set; }

    public GameObject notification_text;

    private void Awake()
    {
        Instance = this;
    }

    public void SendPopOutNotification(string _text)
    {
        GameObject newNotification = Instantiate(notification_text, Vector3.zero, Quaternion.identity);
        newNotification.transform.SetParent(this.transform, false);
        newNotification.GetComponent<TextMeshProUGUI>().text = _text;
    }
}
