using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage Asset Instantiate", menuName = "StageAssetInstantiate")]
public class StageAssetInstantiate : ScriptableObject
{
    public GameObject player_prefab;
    public GameObject pot_prefab;
    public GameObject ingredientSlot_prefab;
    public GameObject[] refinementStation_prefab;

    public Vector3[] potLocation;
    public Vector3[] ingredientSlotLocation;
    public Vector3[] refinementStationLocation;
}
