using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDrivenHandler : MonoBehaviour
{
    public static StateDrivenHandler Instance { get; private set; }

    Animator stateDrivenCamera_anim;
    public Animator craftPanel_anim;
    public GameObject craftButton_obj;

    public float potFullAnimationDuration = 1f;

    private RoomStatus currentRoomStatus = RoomStatus.Default;
    private bool potFull = false;
    private bool runningPotFullCoroutine = false;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        stateDrivenCamera_anim = GetComponent<Animator>();
    }

    void Update()
    {
        UpdateCameraState();
    }

    private void UpdateCameraState()
    {
        ///CHECK ROOM STATUS
        RoomStatus roomStatus = NightTimeManager.Instance.currentRoomStatus;
        if(currentRoomStatus != roomStatus)
        {
            switch(roomStatus)
            {
                case RoomStatus.Default:
                    {
                        craftPanel_anim.SetBool("potRoom", false);
                        stateDrivenCamera_anim.Play("mainRoom");
                        break;
                    }
                case RoomStatus.PotRoom:
                    {
                        //craft panel set to "potroom"
                        craftPanel_anim.SetBool("potRoom", true);

                        if (potFull) //if pot full
                        {
                            stateDrivenCamera_anim.Play("potRoom_potFull");
                        }
                        else //if not pot full
                        {
                            stateDrivenCamera_anim.Play("potRoom_default");
                        }
                        break;
                    }
            }
            currentRoomStatus = roomStatus;
        }

        ///CHECK POT FULL
        if(potFull != CraftPotionManager.Instance.potFull)
        {
            if(runningPotFullCoroutine)
            {
                StopCoroutine("PotFullAnimationCoroutine");
                runningPotFullCoroutine = false;
            }

            if(!CraftPotionManager.Instance.potFull)
            {
                stateDrivenCamera_anim.Play("potRoom_default");
                StartCoroutine(PotFullAnimationCoroutine(false));
            }
            else
            {
                stateDrivenCamera_anim.Play("potRoom_potFull");
                StartCoroutine(PotFullAnimationCoroutine(true));
            }
            potFull = CraftPotionManager.Instance.potFull;
        }
    }

    IEnumerator PotFullAnimationCoroutine(bool _potfull)
    {
        runningPotFullCoroutine = true;

        float timeElapsed = 0;
        while(timeElapsed < potFullAnimationDuration)
        {
            if(_potfull)
            {
                float temp = Mathf.Lerp(0, 1, timeElapsed / potFullAnimationDuration);
                craftPanel_anim.SetFloat("potFull", temp); 
            }
            else
            {
                float temp = Mathf.Lerp(1, 0, timeElapsed / potFullAnimationDuration);
                craftPanel_anim.SetFloat("potFull", temp);
            }
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        if(_potfull)
        {
            craftPanel_anim.SetFloat("potFull", 1);
            craftButton_obj.SetActive(true);
        }
        else
        {
            craftPanel_anim.SetFloat("potFull", 0);
            craftButton_obj.SetActive(false);
        }

        runningPotFullCoroutine = false;
    }
}
