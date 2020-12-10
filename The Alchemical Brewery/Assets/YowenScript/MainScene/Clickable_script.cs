using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Clickable_script : MonoBehaviour
{
    public GameObject player;
    AIDestinationSetter aiDestinationSetter;
    PlayerManager playerManager;

    public GameObject[] MovePoint;
    public string destinationIndex;

    void Start()
    {
        aiDestinationSetter = player.GetComponent<AIDestinationSetter>();
        playerManager = player.GetComponent<PlayerManager>();
    }

    public void DetectClick(string index)
    {
        for (int i = 1; i < MovePoint.Length; i++)
        {
            MovePoint[i].GetComponent<Collider2D>().enabled = false;
        }

        destinationIndex = index;
        switch(index)
        {
            case "a1":
                {
                    aiDestinationSetter.target = MovePoint[1].transform;
                    MovePoint[1].GetComponent<Collider2D>().enabled = true;
                    break;
                }
            case "a2":
                {
                    aiDestinationSetter.target = MovePoint[3].transform;
                    MovePoint[3].GetComponent<Collider2D>().enabled = true;
                    break;
                }
            case "a3":
                {
                    aiDestinationSetter.target = MovePoint[5].transform;
                    MovePoint[5].GetComponent<Collider2D>().enabled = true;
                    break;
                }
            case "b1":
                {
                    aiDestinationSetter.target = MovePoint[7].transform;
                    MovePoint[7].GetComponent<Collider2D>().enabled = true;
                    break;
                }
            case "b2":
                {
                    aiDestinationSetter.target = MovePoint[8].transform;
                    MovePoint[8].GetComponent<Collider2D>().enabled = true;
                    break;
                }
            case "b3":
                {
                    aiDestinationSetter.target = MovePoint[9].transform;
                    MovePoint[9].GetComponent<Collider2D>().enabled = true;
                    break;
                }
            case "b4":
                {
                    aiDestinationSetter.target = MovePoint[10].transform;
                    MovePoint[10].GetComponent<Collider2D>().enabled = true;
                    break;
                }
            case "b5":
                {
                    aiDestinationSetter.target = MovePoint[11].transform;
                    MovePoint[11].GetComponent<Collider2D>().enabled = true;
                    break;
                }
            case "b6":
                {
                    aiDestinationSetter.target = MovePoint[12].transform;
                    MovePoint[12].GetComponent<Collider2D>().enabled = true;
                    break;
                }
            case "c1":
                {
                    aiDestinationSetter.target = MovePoint[13].transform;
                    MovePoint[13].GetComponent<Collider2D>().enabled = true;
                    break;
                }
            case "c2":
                {
                    aiDestinationSetter.target = MovePoint[14].transform;
                    MovePoint[14].GetComponent<Collider2D>().enabled = true;
                    break;
                }
            case "c3":
                {
                    aiDestinationSetter.target = MovePoint[15].transform;
                    MovePoint[15].GetComponent<Collider2D>().enabled = true;
                    break;
                }
            case "c4":
                {
                    aiDestinationSetter.target = MovePoint[16].transform;
                    MovePoint[16].GetComponent<Collider2D>().enabled = true;
                    break;
                }
            case "c5":
                {
                    aiDestinationSetter.target = MovePoint[17].transform;
                    MovePoint[17].GetComponent<Collider2D>().enabled = true;
                    break;
                }
            case "c6":
                {
                    aiDestinationSetter.target = MovePoint[18].transform;
                    MovePoint[18].GetComponent<Collider2D>().enabled = true;
                    break;
                }
            case "e1":
                {
                    aiDestinationSetter.target = MovePoint[19].transform;
                    MovePoint[19].GetComponent<Collider2D>().enabled = true;
                    break;
                }
            case "e2":
                {
                    aiDestinationSetter.target = MovePoint[20].transform;
                    MovePoint[20].GetComponent<Collider2D>().enabled = true;
                    break;
                }
            case "e3":
                {
                    aiDestinationSetter.target = MovePoint[21].transform;
                    MovePoint[21].GetComponent<Collider2D>().enabled = true;
                    break;
                }
            case "e4":
                {
                    aiDestinationSetter.target = MovePoint[22].transform;
                    MovePoint[22].GetComponent<Collider2D>().enabled = true;
                    break;
                }
            case "e5":
                {
                    aiDestinationSetter.target = MovePoint[23].transform;
                    MovePoint[23].GetComponent<Collider2D>().enabled = true;
                    break;
                }
        }

        if(player.transform.position == aiDestinationSetter.target.position)
        {
            playerManager.reachDestination = true;
        }
        else
        {
            playerManager.reachDestination = false;
        }

    }
}
