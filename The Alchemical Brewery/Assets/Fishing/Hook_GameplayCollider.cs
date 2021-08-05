using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook_GameplayCollider : MonoBehaviour
{
    public FishingMiniGame fishingMiniGame;

    void OnTriggerStay2D(Collider2D col)
    {
        if(fishingMiniGame.startMiniGame)
        {
            if (col.tag == "fish")
            {
                //fishingMiniGame.progress -= 15f * Time.deltaTime;
                fishingMiniGame.inHook = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (fishingMiniGame.startMiniGame)
        {
            if (col.tag == "fish")
            {
                fishingMiniGame.inHook = false;
            }
        }
    }
}
