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
    public Element elementType;
    public PotionUsage ingredientUsage;

    public Color ingredientColor;

    [Header("Put index of each refine stage")]
    public RefinementStage refineStage;
    public IngredientData originalIngredient;

    [Header("Price from Fairy Shop")]
    public int ingredientPrice;
}