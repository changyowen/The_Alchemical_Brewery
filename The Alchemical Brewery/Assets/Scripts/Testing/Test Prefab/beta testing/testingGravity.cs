using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testingGravity : MonoBehaviour
{
    Rigidbody rb;
    public Quaternion rollRotation;

    public Vector3 cameraRotation = new Vector3(75, 0, 0);
    public float upBrust = 4.5f;
    public float[] sideBrust = new float[2];
    public float gravityForce = 12f;

    Vector3 velocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //assign camera rotation
        rollRotation = Quaternion.Euler(cameraRotation);
        rb.velocity = rollRotation * Vector3.up * 4f;
    }

    void Update()
    {
        Vector3 useGravity = rollRotation * -Vector3.up;
        if(rb.velocity.y > useGravity.y * 10 && rb.velocity.z > useGravity.z * 10)
        {
            rb.velocity += useGravity * 6f * Time.deltaTime;
        }
    }
}
