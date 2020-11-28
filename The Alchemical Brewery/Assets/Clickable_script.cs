using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Clickable_script : MonoBehaviour
{
    public GameObject player;
    AIDestinationSetter aiDestinationSetter;

    public Transform[] MovePoint;

    void Start()
    {
        aiDestinationSetter = player.GetComponent<AIDestinationSetter>();
    }

    public void DetectClick(string index)
    {
        switch(index)
        {
            case "a1":
                {
                    aiDestinationSetter.target = MovePoint[1];
                    break;
                }
            case "a2":
                {
                    aiDestinationSetter.target = MovePoint[3];
                    break;
                }
            case "a3":
                {
                    aiDestinationSetter.target = MovePoint[5];
                    break;
                }
            case "b1":
                {
                    aiDestinationSetter.target = MovePoint[7];
                    break;
                }
            case "b2":
                {
                    aiDestinationSetter.target = MovePoint[8];
                    break;
                }
            case "b3":
                {
                    aiDestinationSetter.target = MovePoint[9];
                    break;
                }
            case "b4":
                {
                    aiDestinationSetter.target = MovePoint[10];
                    break;
                }
            case "b5":
                {
                    aiDestinationSetter.target = MovePoint[11];
                    break;
                }
            case "b6":
                {
                    aiDestinationSetter.target = MovePoint[12];
                    break;
                }
            case "c1":
                {
                    aiDestinationSetter.target = MovePoint[13];
                    break;
                }
            case "c2":
                {
                    aiDestinationSetter.target = MovePoint[14];
                    break;
                }
            case "c3":
                {
                    aiDestinationSetter.target = MovePoint[15];
                    break;
                }
            case "c4":
                {
                    aiDestinationSetter.target = MovePoint[16];
                    break;
                }
            case "c5":
                {
                    aiDestinationSetter.target = MovePoint[17];
                    break;
                }
            case "c6":
                {
                    aiDestinationSetter.target = MovePoint[18];
                    break;
                }

        }
    }
}
