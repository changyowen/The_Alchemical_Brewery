using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfInteraction : MonoBehaviour
{
    public int shelfIndex = 0;
    public int ingrdientIndex = 0;
    public float shelfReopenTime = 5f;
    public IngredientDrop ingredientDrop;

    bool shelfOpen = true;
    float shelfTimer = 0;

    void Update()
    {
        if(!shelfOpen)
        {
            shelfTimer += Time.deltaTime;
            if(shelfTimer >= shelfReopenTime)
            {
                shelfTimer = 0f;
                shelfOpen = true;
            }
        }
    }

    void OnMouseDown()
    {
        Debug.Log("interact");
        //if player still far from shelf
        float dist = Vector3.Distance(PlayerInfoHandler.Instance.playerPosition, transform.position);
        if (dist > 3f)
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
                ingredientDrop.IngredientSpawn(shelfIndex, ingrdientIndex);
            }
            
            //close shelf
            shelfOpen = false;
        }
    }
}
