using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager: MonoBehaviour 
{
    //main menu
    public static AudioClip mainMenuBGM;

    //NTS
    public static AudioClip nextDay, nTS_lobby, nTS_SHop;

    //DTS
    public static AudioClip stage1BGM, stage2BGM, stage3BGM, brewPot, chestPop, coins, teleport, drinkPotion, nPC_Hey, dayStart, dayEnd;

    //Credits
    public static AudioClip creditsBGM;

    void Awake()
    {
        mainMenuBGM = Resources.Load<AudioClip>("Main Menu");
        nextDay = Resources.Load<AudioClip>("NextDay");
        nTS_lobby = Resources.Load<AudioClip>("NTS Lobby");
        nTS_SHop = Resources.Load<AudioClip>("NTS Shop");
    }
}
