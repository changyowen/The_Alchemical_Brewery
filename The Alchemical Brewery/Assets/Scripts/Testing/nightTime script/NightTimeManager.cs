using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum RoomStatus
{
    Default,
    PotRoom
}

public class NightTimeManager : MonoBehaviour
{
    public static NightTimeManager Instance { get; private set; }

    //public static int stageIndex = 1;

    public GameObject nightTimeIntro_obj;
    public GameObject nightTimeOutro_obj;

    public RoomStatus currentRoomStatus = default;

    [Header("Testing Use")]
    public bool testing = false;

    void Awake()
    {
        Instance = this;

        if(testing)
        {
            PlayerProfile.GM_TestingUse();
            //SaveManager.Save();
            //SaveManager.Load();
        }
    }

    void Start()
    {
        StartCoroutine(StartSceneIntro());
        IngredientPanel.Instance.AssignButtonData();
    }

    public void NextScene()
    {
        if(PlayerProfile.acquiredPotion.Count == 0)
        {
            NotificationSystem.Instance.SendPopOutNotification("Craft at least one potion before open your shop!");
        }
        else
        {
            GlobalSoundAudio _globalSoundAudio = FindObjectOfType<GlobalSoundAudio>();
            if (_globalSoundAudio != null)
            {
                _globalSoundAudio.ChangeVolume(0.3f);
            }
            AudioSourceNTS.nts_AudioSource.PlayOneShot(SoundManager.nextDay, 0.6f);
            //clear pot and returning ingredient
            CraftPotionManager.Instance.ResetPotIngredientList(true);
            //start outro
            StartCoroutine(StartSceneOutro());
        }
    }

    IEnumerator StartSceneIntro()
    {
        //get animator of night time intro
        Animator anim = nightTimeIntro_obj.GetComponent<Animator>();
        //stay this coroutine if animation havent finish
        float tempTime = 0;
        while(anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1)
        {
            yield return null;
        }

        //deactivate night time intro
        nightTimeIntro_obj.SetActive(false);
    }

    IEnumerator StartSceneOutro()
    {
        //set active night time outro
        nightTimeOutro_obj.SetActive(true);

        //Start loading next scene
        LoadingScreenScript.nextSceneIndex = 3;
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        operation.allowSceneActivation = false;

        //get animator of night time outro
        Animator anim = nightTimeIntro_obj.GetComponent<Animator>();

        float temp = 0;
        while(temp < 3f)
        {
            temp += Time.deltaTime;
            yield return null;
        }
        GlobalSoundAudio _globalSoundAudio = FindObjectOfType<GlobalSoundAudio>();
        if (_globalSoundAudio != null)
        {
            _globalSoundAudio.ChangeVolume(0f);
        }

        //enable change scene
        operation.allowSceneActivation = true;
    }

    public void OpenPotRoom()
    {
        currentRoomStatus = RoomStatus.PotRoom;
    }

    public void ReturnLobby()
    {
        currentRoomStatus = RoomStatus.Default;
    }
}
