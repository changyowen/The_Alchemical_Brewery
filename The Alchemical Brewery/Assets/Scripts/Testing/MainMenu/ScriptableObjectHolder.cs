using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Object Holder", menuName = "SO_Holder")]
public class ScriptableObjectHolder: ScriptableObject
{
    public IngredientData[] ingredientSO;
    public IngredientSpawnForce[] ingredientSpawnForceSO;
    public CustomerData[] customerDataSO;

    public StageDataAssign[] stageDataSO;

    [Header("shop asset sprites")]
    public Sprite magicChestSprite;
    public Sprite potSprite;
    public Sprite counterSprite;
    public Sprite[] refinementStationSprite;
    public Sprite[] regionSprite;
    public Sprite customerAppearRateSprite;

    [Header("refinement sprites")]
    public Sprite transparentSprite;
    public Sprite crushedLogoSprite;
    public Sprite extractLogoSprite;

    [Header("element sprites")]
    public Sprite ignisLogoSprite;
    public Sprite aquaLogoSprite;
    public Sprite ordoLogoSprite;
    public Sprite terraLogoSprite;
    public Sprite aerLogoSprite;

    [Header("customer's favor sprites")]
    public Sprite loveLogoSprite;
    public Sprite okayLogoSprite;
    public Sprite boredLogoSprite;
    public Sprite questionMarkLogoSprite;

    [Header("Potion icon choices")]
    public Sprite[] potionIconList;
}
