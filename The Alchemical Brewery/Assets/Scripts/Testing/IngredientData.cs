using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element
{
    Ignis = -2, Aqua = -1, Ordo = 0, Terra = 1, Aer = 2, Null = 3
}

[CreateAssetMenu]
public class IngredientData : ScriptableObject
{
    public int ingredientIndex;
    public string ingredientName;
    public Sprite ingredientSprite;
    [TextArea(3, 10)]
    public string ingredientDescription;


    public int priceVariable;
    public int effectiveVariable;
    public int qualityVariable;
    public Element elementType;

    [Header("Put index of each refine stage")]
    public int[] refineStage;
}