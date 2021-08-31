using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientGravity : MonoBehaviour
{
    Rigidbody rb;
    public Quaternion rollRotation;

    public enum TypeOfSpawnForce
    {
        FromShelf,
        FromPlayer,
        FromPot
    }

    public Vector3 cameraRotation = new Vector3(75, 0, 0);
    public TypeOfSpawnForce ingredientSpawnForce;
    public Vector2 mapCentre;
    public float gravityForce = 12f;
    public float maxGravityVelocity = 8f;

    bool attractEnabled = false;
    bool touchedGround = false;
    bool isAttracted = false;
    public bool released = false;
    public bool inPlayerRange = false;
    public float enableGravityTime = .5f;
    public float disableReleasedTime = 10f;

    float gravityTimer = 0f;
    float releasedTimer = 0f;
    Vector3 velocity;
    public Vector3 attractPosition;

    void Awake()
    {
        //get rigidbody
        rb = GetComponent<Rigidbody>();
        //assign camera rotation
        rollRotation = Quaternion.Euler(cameraRotation);
        //disable gravity
        GravityEnable(false);
    }

    void Update()
    {
        //only enable attract when start dropping after spawn
        if (!attractEnabled)
        {
            gravityTimer += Time.deltaTime;
            if (gravityTimer > enableGravityTime) //if start dropping
            {
                attractEnabled = true;
                gravityTimer = 0;
            }
        }

        //if item just released
        if (released)
        {
            releasedTimer += Time.deltaTime;
            if (releasedTimer > disableReleasedTime)
            {
                released = false;
                releasedTimer = 0;
            }
        }
    }

    void FixedUpdate()
    {
        //if attracting condition satisfied (player ingredient holder, in player range, attractEnabled, not released)
        if (PlayerInfoHandler.Instance.playerIngredientHolder.Count != 4 && attractEnabled && inPlayerRange && !released)
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
        Vector3 useGravity = rollRotation * -Vector3.up;
        if (rb.velocity.y > useGravity.y * maxGravityVelocity && rb.velocity.z > useGravity.z * maxGravityVelocity)
        {
            rb.velocity += useGravity * gravityForce * Time.deltaTime;
        }
    }

    public void SpawnForce()
    {
        //get rigidbody
        rb = GetComponent<Rigidbody>();
        //get so_Holder
        ScriptableObjectHolder so_Holder = StageManager.Instance.so_Holder;
        //get Ingredient spawn force scriptable object
        IngredientSpawnForce ingSpawnForceSO = so_Holder.ingredientSpawnForceSO[(int)ingredientSpawnForce];
        float[] upBurstForce = ingSpawnForceSO.upBurstForce;
        float[] sideBurstForce = ingSpawnForceSO.sideBurstForce;
        float[] forwardBurstForce = ingSpawnForceSO.forwardBurstForce;

        rb.velocity = rollRotation * Vector3.up * Random.Range(upBurstForce[0], upBurstForce[upBurstForce.Length - 1]);

        //determine sideburst direction
        if (transform.position.x < mapCentre[0])
        {
            BurstForce(Vector3.right, +1, sideBurstForce);
        }
        else
        {
            BurstForce(Vector3.right, -1, sideBurstForce);
        }

        if (transform.position.z < mapCentre[1])
        {
            BurstForce(Vector3.forward, +1, forwardBurstForce);
        }
        else
        {
            BurstForce(Vector3.forward, -1, forwardBurstForce);
        }
    }

    void BurstForce(Vector3 direction, int whichSide, float[] BurstForce)
    {
        rb.velocity += direction * whichSide * Random.Range(BurstForce[0], BurstForce[BurstForce.Length - 1]);
    }

    void checkGround()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.1f);
        bool isGrounded_ = false;
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.tag == "Ground")
            {
                isGrounded_ = true;
            }
        }
        touchedGround = isGrounded_;
    }

    void AttractMotion()
    {
        //check distance
        Vector3 attractPoint = attractPosition;
        float dist = Vector3.Distance(attractPoint, transform.position);

        //if havent reach attract point
        if (dist > 0.3f)
        {
            Vector3 Dir = (attractPoint - transform.position).normalized;
            Vector3 attractVelocity = Dir * 14f * Time.deltaTime;
            rb.MovePosition(transform.position + attractVelocity);
        }
        else //if reached attract point
        {
            //check again player ingredient holder is not full
            if (PlayerInfoHandler.Instance.playerIngredientHolder.Count != 4)
            {
                IngredientItemHandler ingredientItemHandler = transform.GetComponent<IngredientItemHandler>();
                ingredientItemHandler.ReceiveIngredient();
                Destroy(this.gameObject);
            }
            else //if player ingredient holder already full
            {
                isAttracted = false; //stop attracting
            }
        }
    }

    void GravityEnable(bool enabled)
    {
        if (enabled)
        {
            rb.useGravity = true;
            rb.isKinematic = true;
        }
        else
        {
            rb.useGravity = false;
            rb.isKinematic = false;
        }
    }
}
