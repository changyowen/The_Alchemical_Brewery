using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testingIngBounce : MonoBehaviour
{
    public Quaternion rollRotation;
    Rigidbody rb;

    public Vector3 cameraRotation = new Vector3(75, 0, 0);
    public float upBrust = 4.5f;
    public float[] sideBrust = new float[2];
    public float gravityForce = 12f;

    bool attractEnabled = false;
    bool touchedGround = false;
    bool isAttracted = false;
    public bool inPlayerRange = false;
    Vector3 velocity;

    void Start()
    {
        //get rigidbody
        rb = GetComponent<Rigidbody>();
        //assign camera rotation
        rollRotation = Quaternion.Euler(cameraRotation);

        //apply force when spawn
        SpawnForce();

        //disable gravity
        GravityEnable(false);
    }

    void FixedUpdate()
    {
        //only enable attract when start dropping after spawn
        if(!attractEnabled)
        {
            if (rb.velocity.y < .1f) //if start dropping
            {
                attractEnabled = true;
            }
        }

        //if attracting condition satisfied (player ingredient holder, in player range, attractEnabled)
        if (PlayerInfoHandler.Instance.playerIngredientHolder.Count != 4 && attractEnabled && inPlayerRange)
        {
            isAttracted = true;
        }
        else //attract condition not satisfied
        {
            isAttracted = false;
        }

        //is not attracted by player
        if (!isAttracted)
        {
            checkGround();
            if (touchedGround)
            {
                GravityEnable(true);
                rb.velocity = Vector3.zero;
            }
            else
            {
                GravityEnable(false);
                GravityDrop();
            }
        }
        else //is attracted by player
        {
            //disable gravity
            GravityEnable(false);
            //attracting
            AttractMotion();
        }
    }

    void GravityDrop()
    {
        rb.position += velocity * Time.deltaTime;
        velocity -= rollRotation * Vector3.up * 12f * Time.deltaTime;
    }

    void SpawnForce()
    {
        velocity = rollRotation * Vector3.up * upBrust;
        velocity += Vector3.right * Random.Range(sideBrust[0], sideBrust[1]);
    }

    void checkGround()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.1f);
        bool isGrounded_ = false;
        foreach (Collider hitCollider in hitColliders)
        {
            if(hitCollider.tag == "Ground")
            {
                isGrounded_ = true;
            }
        }
        touchedGround = isGrounded_;
    }

    void AttractMotion()
    {
        //check distance
        Transform attractPoint = GameObject.Find("attract").transform;
        float dist = Vector3.Distance(attractPoint.position, transform.position);

        //if havent reach attract point
        if (dist > 0.1)
        {
            Vector3 Dir = (attractPoint.position - transform.position).normalized;
            Vector3 attractVelocity = Dir * 14f * Time.deltaTime;
            rb.MovePosition(transform.position + attractVelocity);
        }
        else //if reached attract point
        {
            //check again player ingredient holder is not full
            if(PlayerInfoHandler.Instance.playerIngredientHolder.Count != 4)
            {

            }
            else //if player ingredient holder already full
            {
                isAttracted = false; //stop attracting
            }
        }
    }

    void GravityEnable(bool enabled)
    {
        if(enabled)
        {
            rb.useGravity = true;
            rb.isKinematic = true;
        }
        else
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }
    }
}
