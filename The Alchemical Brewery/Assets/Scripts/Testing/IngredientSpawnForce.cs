using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnForce1", menuName = "IngredientSpawnForce")]
public class IngredientSpawnForce : ScriptableObject
{
    public float[] upBurstForce;
    public float[] sideBurstForce;
    public float[] forwardBurstForce;
}
