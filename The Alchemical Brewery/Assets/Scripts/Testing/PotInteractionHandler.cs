using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PotInteractionHandler : MonoBehaviour
{
    int potIndex;

    PotInformationHandler potInformationHandler;

    void Start()
    {
        //get pot information handler
        potInformationHandler = GetComponent<PotInformationHandler>();
        //get pot index
        potIndex = potInformationHandler.potIndex;
    }

    void OnMouseDown()
    {
        //check if not mouse over UI
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            //if player still far from pot
            float dist = Vector3.Distance(PlayerInfoHandler.Instance.playerPosition, transform.position);
            if (dist > 4f)
            {
                //go to the nearest point toward collider
                Collider col = GetComponent<Collider>();
                Vector3 closestPoint = col.ClosestPoint(PlayerInfoHandler.Instance.playerPosition);
                RayCastMovement.Instance.NewDestination(closestPoint);
            }
            else //if player already near the pot
            {
                //stop moving
                RayCastMovement.Instance.NewDestination(PlayerInfoHandler.Instance.playerPosition);
                //Start interaction
                PotInteraction();
                //PotSystem_Daytime.Instance.PotInteraction(potIndex);
            }
        }
        
    }

    void PotInteraction()
    {
        //if pot is not boiling
        if(!potInformationHandler.potBoiling)
        {
            //if pot is holding potion
            if (potInformationHandler.potPotionHolder != 0)
            {
                //if player didnt holding potion
                if (PlayerInfoHandler.Instance.playerPotionHolder == 0)
                {
                    //take potion
                    TakePotion();
                }
            }
            else //if pot not holding any potion
            {
                //if pot ingredient holder is not full
                if (potInformationHandler.potIngredientHolderList.Count < 4)
                {
                    //if player holding ingredient
                    if (PlayerInfoHandler.Instance.playerIngredientHolder.Count != 0)
                    {
                        //put ingredient into pot holder
                        PutIngredient();
                    }
                }
            }
        }
        else //if pot is boiling
        {

        }
    }

    void TakePotion()
    {
        int takenPotionIndex = potInformationHandler.potPotionHolder;
        //player get potion
        PlayerInfoHandler.Instance.playerPotionHolder = takenPotionIndex;
        //reset pot potion holder
        potInformationHandler.potPotionHolder = 0;
    }

    void PutIngredient()
    {
        //get player ingredient holder
        List<int> playerIngredientHolder = PlayerInfoHandler.Instance.playerIngredientHolder;

        //get ingredient in player holder
        int putIngredientIndex = playerIngredientHolder[0];
        //remove ingredient from player holder
        playerIngredientHolder.RemoveAt(0);

        //add ingredient into pot holder
        potInformationHandler.potIngredientHolderList.Add(putIngredientIndex);
    }

    public void PopOutIngredient(int holderIndex)
    {
        //check if clicked clot not empty
        if(potInformationHandler.potIngredientHolderList.Count > holderIndex)
        {
            //spawn ingredient
            GameObject spawnedIngredient = Instantiate(potInformationHandler.spawnIngredient_obj, potInformationHandler.ingredientSpawn.position, Quaternion.identity) as GameObject;

            //set ingredient data into handler
            IngredientItemHandler ingredientItemHandler = spawnedIngredient.GetComponent<IngredientItemHandler>();
            ingredientItemHandler.shelfIndex = 0;
            ingredientItemHandler.ingredientIndex = potInformationHandler.potIngredientHolderList[holderIndex];

            //set spawn force
            spawnedIngredient.GetComponent<IngredientGravity>().ingredientSpawnForce = IngredientGravity.TypeOfSpawnForce.FromPot;
            spawnedIngredient.GetComponent<IngredientGravity>().SpawnForce();
            //remove ingredient from pot holder
            potInformationHandler.potIngredientHolderList.RemoveAt(holderIndex);
        }
    }

    public void StartCraftPotion()
    {
        //reset pot potion holder
        potInformationHandler.ResetPotHolder();
        //get boiling time needed
        float boilingTime = 5f;
        //start crafting potion
        StartCoroutine(CraftPotion(boilingTime));
    }

    IEnumerator CraftPotion(float boilingTime)
    {
        //wait for boiling time
        yield return new WaitForSeconds(boilingTime);
        //complete potion
        potInformationHandler.potPotionHolder = 1;
    }
}
