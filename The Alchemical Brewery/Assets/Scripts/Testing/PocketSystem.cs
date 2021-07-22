using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PocketSystem : MonoBehaviour
{
    public Image potionHolderImage;
    public Image[] ingredientHolderImage;
    public GameObject spawnIngredient_obj;
    public Vector3 spawnPosOffset;

    void Update()
    {
        IngredientHolderUpdate();
    }

    //update potion holder sprite
    void PotionHolderUpdate()
    {
        //get player potion holder
        int playerPotionHolder = PlayerInfoHandler.Instance.playerPotionHolder;

        switch(playerPotionHolder)
        {
            case 0: //if no potion hold
                {
                    potionHolderImage.sprite = null;
                    break;
                }
            default: //if holded potion
                {
                    potionHolderImage.sprite = null;
                    break;
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
                Sprite ingSprite = SO_Holder.Instance.ingredientSO[playerIngredientHolder[i]].ingredientSprite;
                ingredientHolderImage[i].sprite = ingSprite;
            }

            //fill emply ingredient slot with transparent sprite
            for (int j = ingredientHolderImage.Length - 1; j >= playerIngredientHolder.Count; j--)
            {
                Sprite transSprite = SO_Holder.Instance.transparentSprite;
                ingredientHolderImage[j].sprite = transSprite;
            }
        }
        else //if player ingredient holder is empty
        {
            //fill all emply ingredient slot with transparent sprite
            for (int i = 0; i < ingredientHolderImage.Length; i++)
            {
                Sprite transSprite = SO_Holder.Instance.transparentSprite;
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
