using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDrivenHandler : MonoBehaviour
{
    public static StateDrivenHandler Instance { get; private set; }

    Animator anim;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        UpdateCameraState();
    }

    private void UpdateCameraState()
    {
        RoomStatus currentRoomStatus = NightTimeManager.Instance.currentRoomStatus;

        switch(currentRoomStatus)
        {
            case RoomStatus.Default:
                {
                    anim.Play("mainRoom");
                    break;
                }
            case RoomStatus.PotRoom:
                {
                    bool potFull = CraftPotionManager.Instance.potFull;

                    if(potFull)
                    {
                        anim.Play("potRoom_potFull");
                    }
                    else
                    {
                        anim.Play("potRoom_default");
                    }
                    break;
                }
        }
    }
}
