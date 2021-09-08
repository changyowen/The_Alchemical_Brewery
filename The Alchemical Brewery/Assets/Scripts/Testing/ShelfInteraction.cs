using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfInteraction : MonoBehaviour
{
    [Header("Get Component")]
    public ScriptableObjectHolder SO_holder;
    public SpriteRenderer chestSR;
    public SpriteRenderer orbSR;
    public SpriteRenderer ingredientSR;
    public IngredientDrop ingredientDrop;
    public GameObject terraParticleSystem;

    [Header("Sprite")]
    public Sprite closeChest;
    public Sprite openChest;

    [Header("Chest Data")]
    public int shelfIndex = 0;
    public int ingredientIndex = 0;
    public float shelfReopenTime = 5f;
   

    bool shelfOpen = true;
    float shelfTimer = 0;

    void Update()
    {
        if(!shelfOpen)
        {
            ChestSpriteHandler(false);

            shelfTimer += Time.deltaTime;
            if(shelfTimer >= shelfReopenTime)
            {
                shelfTimer = 0f;
                shelfOpen = true;
            }
        }
        else
        {
            ChestSpriteHandler(true);
        }
    }

    void ChestSpriteHandler(bool open)
    {
        ingredientSR.enabled = open;
        orbSR.enabled = open;

        if (open)
        {
            chestSR.sprite = openChest;
        }
        else
        {
            chestSR.sprite = closeChest;
        }
    }

    public void UpdateChestIngredientSprite()
    {
        ingredientSR.sprite = SO_holder.ingredientSO[ingredientIndex].ingredientSprite;
    }

    void OnMouseDown()
    {
        //if player still far from shelf
        float dist = Vector3.Distance(PlayerInfoHandler.Instance.playerPosition, transform.position);
        if (dist > 4f)
        {
            //go to the nearest point toward collider
            Collider col = GetComponent<Collider>();
            Vector3 closestPoint = col.ClosestPoint(PlayerInfoHandler.Instance.playerPosition);
            RayCastMovement.Instance.NewDestination(closestPoint);
        }
        else //if player already near the shelf
        {
            //stop moving
            RayCastMovement.Instance.NewDestination(PlayerInfoHandler.Instance.playerPosition);
            //Start interaction
            //ShelfSystem_Daytime.Instance.ShelfInteraction(shelfIndex);
            if(shelfOpen)
            {
                ingredientDrop.IngredientSpawn(shelfIndex, ingredientIndex);
            }
            
            //close shelf
            shelfOpen = false;
        }
    }

    public void DirectIngredientSpawn()
    {
        //reopen shelf shelf
        shelfOpen = true;

        //deactivate and activate particle system [for make sure particle system reopen]
        terraParticleSystem.SetActive(false);
        terraParticleSystem.SetActive(true);

        //spawn ingredient
        ingredientDrop.IngredientSpawn(shelfIndex, ingredientIndex);
    }
}
