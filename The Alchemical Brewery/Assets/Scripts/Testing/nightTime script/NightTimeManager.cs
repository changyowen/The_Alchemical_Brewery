﻿using System.Collections;
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

    public GameObject nightTimeIntro_obj;
    public GameObject nightTimeOutro_obj;

    public RoomStatus currentRoomStatus = default;

    [Header("Testing Use")]
    public bool testing = false;

    void Awake()
    {
        Instance = this;

        if(!testing)
        {
            SaveManager.Load();
        }
    }

    void Start()
    {
        StartCoroutine(StartSceneIntro());
    }

    void Update()
    {

    }

    public void NextScene()
    {
        StartCoroutine(StartSceneOutro());
    }

    IEnumerator StartSceneIntro()
    {
        //get animator of night time intro
        Animator anim = nightTimeIntro_obj.GetComponent<Animator>();
        //stay this coroutine if animation havent finish
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
        AsyncOperation operation = SceneManager.LoadSceneAsync(0);
        operation.allowSceneActivation = false;

        //get animator of night time outro
        Animator anim = nightTimeIntro_obj.GetComponent<Animator>();

        float temp = 0;
        while(temp < 3f)
        {
            temp += Time.deltaTime;
            yield return null;
        }

        //stay this coroutine if animation havent finish
        //while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1)
        //{
        //    yield return null;
        //}
        Debug.Log("Yes");
        //enable change scene
        operation.allowSceneActivation = true;
    }
}
