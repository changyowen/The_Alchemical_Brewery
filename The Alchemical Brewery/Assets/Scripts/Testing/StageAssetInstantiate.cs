using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage Asset Instantiate", menuName = "StageAssetInstantiate")]
public class StageAssetInstantiate : ScriptableObject
{
    public GameObject player_prefab;

    [Header("prefab")]
    public GameObject dayTimeIntro_prefab;
    public GameObject counter_prefab;
    public GameObject magicChest_prefab;
    public GameObject pot_prefab;
    public GameObject grinder_prefab;
    public GameObject distillStand_prefab;

    [Header("Stage Asset Position")]
    public GameObject[] stageAsset;
}
