using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotSystem_Daytime : MonoBehaviour
{
    public static PotSystem_Daytime Instance { get; private set; }

    public int unlockedPot = 3; //total unlocked pot
    public List<Pot> potList = new List<Pot>(); //List for pot class

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //declare pot class
        for (int i = 0; i < unlockedPot; i++)
        {
            Pot newPot = new Pot();
            newPot.ResetPotHolder();
            potList.Add(newPot);
        }
    }

    public void PotInteraction(int potIndex)
    {
        //get pot class
        Pot thisPot = potList[potIndex];

        //if pot is not boiling
        if(!thisPot.potBoiling)
        {
            //if pot holding potion
            if (thisPot.potPotionHolder != 0)
            {
                //if player didnt holding potion
                if (PlayerInfoHandler.Instance.playerPotionHolder == 0)
                {
                    //if pot holding potion
                    if (thisPot.potPotionHolder != 0)
                    {
                        thisPot.TakePotion();
                    }
                }
            }
            else //if pot not holding potion
            {
                //if pot ingredient holder is not full
                if (thisPot.potIngredientHolderList.Count < 4)
                {
                    Debug.Log("HII");
                    //get player ingredient holder
                    List<int> playerIngredientHolder = PlayerInfoHandler.Instance.playerIngredientHolder;

                    //if player holding ingredient
                    if (playerIngredientHolder.Count != 0)
                    {
                        int putIngredientIndex = playerIngredientHolder[0];
                        playerIngredientHolder.RemoveAt(0);
                        thisPot.PutIngredient(putIngredientIndex);
                    }
                }
            }
        }
        else //if pot is boiling
        {

        }
    }

    public void StartCraftPotion(int potIndex)
    {
        Pot thispot = potList[potIndex];
        StartCoroutine(thispot.CraftingPotion());
    }
}
