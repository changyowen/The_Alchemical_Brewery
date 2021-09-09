using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class PocketSystem : MonoBehaviour
{
    public static PocketSystem Instance { get; private set; }

    public ScriptableObjectHolder SO_holder;

    public Image[] potionHolderImage;
    public Image[] ingredientHolderImage;
    public GameObject spawnIngredient_obj;
    public Vector3 spawnPosOffset;

    private void Awake()
    {
        Instance = this;
    }

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

    public IEnumerator DrinkPotion(int holderIndex)
    {
        //get player potion holder
        List<int> playerPotionHolder = PlayerInfoHandler.Instance.playerPotionHolderList;
        
        if (PlayerInfoHandler.Instance != null)
        {
            if (holderIndex < playerPotionHolder.Count && !PlayerInfoHandler.Instance.drinkingPotionState) //if potion exist in this slot
            {
                //get Potion Data
                PotionData _potionData = StageManager.potionListToday[holderIndex];

                //remove choosen ingredient
                playerPotionHolder.RemoveAt(holderIndex);

                //turn drinkingPotion state
                PlayerInfoHandler.Instance.drinkingPotionState = true;
                //stop moving
                NavMeshAgent _navMeshAgent = PlayerInfoHandler.Instance.gameObject.GetComponent<NavMeshAgent>();
                if (_navMeshAgent != null)
                {
                    _navMeshAgent.SetDestination(PlayerInfoHandler.Instance.transform.position);
                }
                //start drinking potion
                Animator _anim = PlayerInfoHandler.Instance.playerAnim;
                if(_anim != null)
                {
                    _anim.SetTrigger("drinkingPotion");
                }

                //wait animation
                yield return new WaitForSeconds(2.4f);

                //deactivate drinkingPotion state
                PlayerInfoHandler.Instance.drinkingPotionState = false;

                //increase element for skill
                IncreaseElementForSkill(_potionData);
            }
        }
    }

    void IncreaseElementForSkill(PotionData _potionData)
    {
        for (int i = 0; i < _potionData.potionElement.Count; i++)
        {
            switch(_potionData.potionElement[i])
            {
                case Element.Ignis:
                    {
                        ElementMeterPanel.Instance.elementMana[0] += 4;
                        break;
                    }
                case Element.Aqua:
                    {
                        ElementMeterPanel.Instance.elementMana[1] += 4;
                        break;
                    }
                case Element.Terra:
                    {
                        ElementMeterPanel.Instance.elementMana[2] += 4;
                        break;
                    }
                case Element.Aer:
                    {
                        ElementMeterPanel.Instance.elementMana[3] += 4;
                        break;
                    }
                case Element.Ordo:
                    {
                        ElementMeterPanel.Instance.elementMana[4] += 4;
                        break;
                    }
            }
        }
    }
}
