using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public FishingMiniGame fishingMiniGame;
    public Hook_ForObstacle hook_ForObstacle;
    public GameObject hook2;
    public Transform progressBarCenter;
    public Transform obstacle_topPivot;
    public Transform obstacle_bottomPivot;

    public bool collideObstacle = false;
    public float hookVelocity2 = 0;
    
    void Update()
    {
        if(fishingMiniGame.startMiniGame)
        {
            HookMotion();
        }
        else
        {
            hook2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    void FixedUpdate()
    {
        if (fishingMiniGame.startMiniGame)
        {
            if (Input.GetMouseButton(0))
            {
                InputFunction();
            }
        }
    }

    void HookMotion()
    {
        if (!hook_ForObstacle.stopAtBottom)
        {
            if (hookVelocity2 <= -14f)
            {
                hookVelocity2 = -14f;
            }
            else
            {
                hookVelocity2 -= 13f * Time.deltaTime;
            }

            if (hook_ForObstacle.stopAtTop)
            {
                hookVelocity2 = Mathf.Clamp(hookVelocity2, -14f, 0f);
            }
        }
        else if (hook_ForObstacle.stopAtBottom)
        {
            hookVelocity2 = Mathf.Clamp(hookVelocity2, 0f, 45f);
        }

        hook2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, hookVelocity2);
    }

    void InputFunction()
    {
        if (hookVelocity2 >= 45f)
        {
            hookVelocity2 = 45f;
        }
        else
        {
            hookVelocity2 += 40f * Time.deltaTime;
        }
    }

    public void StopHookVelocity()
    {
        hookVelocity2 = 0f;
    }

    public void ResetHookLocation()
    {
        //reset hook position
        Transform hookTransform = hook2.GetComponent<Transform>();
        hookTransform.position = (obstacle_topPivot.position + obstacle_bottomPivot.position) * .5f;
        //reset collideObstacle bool
        collideObstacle = false;
        //reset hookVelocity
        hookVelocity2 = 0;
    }
}
