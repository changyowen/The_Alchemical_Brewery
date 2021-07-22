using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IngredientData : ScriptableObject
{
    public int ingredientIndex;
    public string ingredientName;
    public Sprite ingredientSprite;
    [TextArea(3, 10)]
    public string ingredientDescription;

    public int priceVariable;
    public int typeVariable;
    public int qualityVariable;
    public int refineStage;
}