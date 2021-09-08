using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientDrop : MonoBehaviour
{
    public GameObject spawnIngredient_obj;
    
    public bool spawnIngredientBool = false;
    public Vector3 spawnPosOffset;
    public float spawnRefreshTime = 1f;

    float spawnTimer = 0f;

    void Update()
    {
        if(spawnTimer <= spawnRefreshTime)
        {
            spawnTimer += Time.deltaTime;
        }
    }

    public void IngredientSpawn(int shelfIndex, int ingredientIndex)
    {
        if(spawnTimer > spawnRefreshTime)
        {
            //spawn ingredient
            Vector3 spawningPosition = transform.position + spawnPosOffset;
            GameObject spawnedIngredient = Instantiate(spawnIngredient_obj, spawningPosition, Quaternion.identity) as GameObject;

            //set ingredient data into handler
            IngredientItemHandler ingredientItemHandler = spawnedIngredient.GetComponent<IngredientItemHandler>();
            ingredientItemHandler.shelfIndex = shelfIndex;
            ingredientItemHandler.ingredientIndex = ingredientIndex;
            ingredientItemHandler.UpdateIngedientSprite();

            //set spawn force
            spawnedIngredient.GetComponent<IngredientGravity>().ingredientSpawnForce = IngredientGravity.TypeOfSpawnForce.FromShelf;
            spawnedIngredient.GetComponent<IngredientGravity>().SpawnForce();

            spawnIngredientBool = false;
            spawnTimer = 0;
        }
    }
}
