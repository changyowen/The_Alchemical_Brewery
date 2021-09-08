using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientItemHandler : MonoBehaviour
{
    [Header("Reference")]
    public ScriptableObjectHolder SO_holder;
    public SpriteRenderer ingredientSR;

    [Header("InternalData")]
    public int shelfIndex = 0;
    public int ingredientIndex = 0;

    public void UpdateIngedientSprite()
    {
        ingredientSR.sprite = SO_holder.ingredientSO[ingredientIndex].ingredientSprite;
    }

    public void ReceiveIngredient()
    {
        //receive ingredient
        PlayerInfoHandler.Instance.playerIngredientHolder.Add(ingredientIndex);
    }
}
