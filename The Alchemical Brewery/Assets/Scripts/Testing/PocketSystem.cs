using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PocketSystem : MonoBehaviour
{
    public ScriptableObjectHolder SO_holder;

    public Image[] potionHolderImage;
    public Image[] ingredientHolderImage;
    public GameObject spawnIngredient_obj;
    public Vector3 spawnPosOffset;

    void Update()
    {
        PotionHolderUpdate();
        IngredientHolderUpdate();
    }

    //update potion holder sprite
    void PotionHolderUpdate()
    {
        //get player potion holder
        List<int> playerPotionHolderList = PlayerInfoHandler.Instance.playerPotionHolderList;

        if (StageManager.potionListToday != null)
        {
            for (int i = 0; i < 3; i++)
            {
                if(playerPotionHolderList.Count > i) //if potion exist in this slot
                {
                    PotionData potionData = StageManager.potionListToday[playerPotionHolderList[i]];
                    potionHolderImage[i].sprite = SO_holder.potionIconList[potionData.potionSpriteIndex];
                }
                else //if potion not exist in this slot
                {
                    potionHolderImage[i].sprite = SO_holder.transparentSprite;
                }
            }
        }
    }

    //update ingredient holder sprite
    void IngredientHolderUpdate()
    {
        //get player ingredient holder
        List<int> playerIngredientHolder = PlayerInfoHandler.Instance.playerIngredientHolder;
        
        //if player ingredient holder is not empty
        if(playerIngredientHolder.Count != 0)
        {
            //get empty ingredient slot total
            int emptyIngTotal = ingredientHolderImage.Length - playerIngredientHolder.Count;

            //update ingredient image
            //**image order is start from behind
            for (int i = 0; i < playerIngredientHolder.Count; i++)
            {
                //get so_Holder
                ScriptableObjectHolder so_Holder = StageManager.Instance.so_Holder;
                //get ingredient sprite
                Sprite ingSprite = so_Holder.ingredientSO[playerIngredientHolder[i]].ingredientSprite;
                ingredientHolderImage[i].sprite = ingSprite;
            }

            //fill emply ingredient slot with transparent sprite
            for (int j = ingredientHolderImage.Length - 1; j >= playerIngredientHolder.Count; j--)
            {
                //get so_Holder
                ScriptableObjectHolder so_Holder = StageManager.Instance.so_Holder;
                //get transparant sprite
                Sprite transSprite = so_Holder.transparentSprite;
                ingredientHolderImage[j].sprite = transSprite;
            }
        }
        else //if player ingredient holder is empty
        {
            //fill all emply ingredient slot with transparent sprite
            for (int i = 0; i < ingredientHolderImage.Length; i++)
            {
                //get so_Holder
                ScriptableObjectHolder so_Holder = StageManager.Instance.so_Holder;
                //get transparant sprite
                Sprite transSprite = so_Holder.transparentSprite;
                ingredientHolderImage[i].sprite = transSprite;
            }
        }
    }

    public void ReleaseIngredient(int holderIndex)
    {
        //get player ingredient holder
        List<int> playerIngredientHolder = PlayerInfoHandler.Instance.playerIngredientHolder;

        //only activate when ingredient exist in pocket[index]
        if(playerIngredientHolder.Count > holderIndex)
        {
            //Spawn ingredient object
            Vector3 spawningPosition = PlayerInfoHandler.Instance.playerPosition + spawnPosOffset;
            GameObject spawnedIngredient = Instantiate(spawnIngredient_obj, spawningPosition, Quaternion.identity) as GameObject;
            //release force & set released
            IngredientGravity ingredientGravity = spawnedIngredient.GetComponent<IngredientGravity>();
            ingredientGravity.ingredientSpawnForce = IngredientGravity.TypeOfSpawnForce.FromPlayer;
            ingredientGravity.SpawnForce();
            ingredientGravity.GetComponent<IngredientGravity>().released = true;
            //set ingredient data
            IngredientItemHandler ingredientItemHandler = spawnedIngredient.GetComponent<IngredientItemHandler>();
            ingredientItemHandler.ingredientIndex = playerIngredientHolder[holderIndex];

            //remove choosen ingredient
            playerIngredientHolder.RemoveAt(holderIndex);
        }
    }
}
