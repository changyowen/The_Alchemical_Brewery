using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptable Object Holder", menuName = "SO_Holder")]
public class ScriptableObjectHolder: ScriptableObject
{
    public Sprite transparentSprite;
    public IngredientData[] ingredientSO;
    public IngredientSpawnForce[] ingredientSpawnForceSO;
    public CustomerData[] customerDataSO;

    public StageDataAssign[] stageDataSO;
}
