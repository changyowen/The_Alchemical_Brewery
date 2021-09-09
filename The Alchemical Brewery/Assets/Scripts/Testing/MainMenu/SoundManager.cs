using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager: MonoBehaviour 
{
    //main menu
    public static AudioClip mainMenuBGM;

    //NTS
    public static AudioClip nextDay, nTS_lobby, nTS_SHop;

    //Choose Potion Scene
    public static AudioClip choosePotionBGM;

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

        choosePotionBGM = Resources.Load<AudioClip>("ChoosePotionBGM"); ;

        stage1BGM = Resources.Load<AudioClip>("Stage 1 Plains");
        stage2BGM = Resources.Load<AudioClip>("Stage 2 Desert");
        stage3BGM = Resources.Load<AudioClip>("Stage 3 Tundra");

        brewPot = Resources.Load<AudioClip>("BrewPot");
        chestPop = Resources.Load<AudioClip>("ChestPop");
        coins = Resources.Load<AudioClip>("Coins");
        teleport = Resources.Load<AudioClip>("Teleport");
        drinkPotion = Resources.Load<AudioClip>("DrinkPotion");
        nPC_Hey = Resources.Load<AudioClip>("NPCHey");
        dayStart = Resources.Load<AudioClip>("DayStart");
        dayEnd = Resources.Load<AudioClip>("DayEnd");

        creditsBGM = Resources.Load<AudioClip>("Credit");
    }
}
