using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook_ForObstacle : MonoBehaviour
{
    public Hook hook;

    public bool stopAtBottom = false;
    public bool stopAtTop = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "hook_obstacle_up" || col.tag == "hook_obstacle_down")
        {
            hook.StopHookVelocity();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "hook_obstacle_down")
        {
            stopAtBottom = false;
        }
        else if (col.tag == "hook_obstacle_up")
        {
            stopAtTop = false;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.tag == "hook_obstacle_down")
        {
            stopAtBottom = true;
        }
        else if(col.tag == "hook_obstacle_up")
        {
            stopAtTop = true;
        }
    }
}
