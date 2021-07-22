using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientAttractHandler : MonoBehaviour
{
    void OnTriggerStay(Collider col)
    {
        if(col.tag == "Ingredient")
        {
            IngredientGravity ingredientGravity = col.GetComponent<IngredientGravity>();
            ingredientGravity.inPlayerRange = true;
            ingredientGravity.attractPosition = transform.position;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Ingredient")
        {
            IngredientGravity ingredientGravity = col.GetComponent<IngredientGravity>();
            ingredientGravity.inPlayerRange = false;
        }
    }
}
