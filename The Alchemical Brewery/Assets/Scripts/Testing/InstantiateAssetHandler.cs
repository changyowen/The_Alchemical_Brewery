using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InstantiateAssetHandler : MonoBehaviour
{
    public ScriptableObjectHolder so_Holder;
    public StageAssetInstantiate stageAssetInstantiate;

    public GameObject dayTimeIntro_obj;

    [Header("Testing Use")]
    public bool testing = false;
    public List<int> ingOrder_test = new List<int>();

    public void InstantiateAssetPrefab(int stageIndex, List<int> ingredientOrderToday)
    {
        //spawn stage asset position
        GameObject stageAssetPos = Instantiate(stageAssetInstantiate.stageAsset[stageIndex], Vector3.zero, Quaternion.identity) as GameObject;

        //get each position transform
        Transform[] counter_Pos = stageAssetPos.transform.GetChild(1).Cast<Transform>().ToArray();
        Transform[] magicChest_pos = stageAssetPos.transform.GetChild(2).Cast<Transform>().ToArray();
        Transform[] pot_pos = stageAssetPos.transform.GetChild(3).Cast<Transform>().ToArray();
        Transform grinder_pos = stageAssetPos.transform.GetChild(4).GetChild(0).transform;
        Transform distillStand_pos = stageAssetPos.transform.GetChild(4).GetChild(1).transform;

        //spawn each prefab
        List<GameObject> counterPrefabList = InstantiateEachAsset(stageAssetInstantiate.counter_prefab, counter_Pos);
        List<GameObject> magicChestPrefabList = InstantiateEachAsset(stageAssetInstantiate.magicChest_prefab, magicChest_pos);
        List<GameObject> potPrefabList = InstantiateEachAsset(stageAssetInstantiate.pot_prefab, pot_pos);
        GameObject grinderPrefab = Instantiate(stageAssetInstantiate.grinder_prefab, grinder_pos.position, Quaternion.identity) as GameObject;
        GameObject distillStandPrefab = Instantiate(stageAssetInstantiate.distillStand_prefab, distillStand_pos.position, Quaternion.identity) as GameObject;

        //assign parent
        AssignParent(counterPrefabList, stageAssetPos.transform.GetChild(5));
        AssignParent(magicChestPrefabList, stageAssetPos.transform.GetChild(5));
        AssignParent(potPrefabList, stageAssetPos.transform.GetChild(5));
        grinderPrefab.transform.SetParent(stageAssetPos.transform.GetChild(5), false);
        distillStandPrefab.transform.SetParent(stageAssetPos.transform.GetChild(5), false);

        //assign counter data
        AssignCounterData(counterPrefabList);
        //assign magicChest data
        AssignMagicChestData(magicChestPrefabList, ingredientOrderToday);
        //assign pot data
        AssignPotData(potPrefabList);
    }

    List<GameObject> InstantiateEachAsset(GameObject _prefab, Transform[] instantiatePos)
    {
        //declare a empty list for prefab list
        List<GameObject> prefabList = new List<GameObject>();

        //start spawning and assign into prefab list
        for (int i = 0; i < instantiatePos.Length; i++)
        {
            GameObject spawnedPrefab = Instantiate(_prefab, instantiatePos[i].position, Quaternion.identity) as GameObject;
            prefabList.Add(spawnedPrefab);
        }

        //return prefab list
        return prefabList;
    }

    void AssignParent(List<GameObject> _prefab, Transform parent_transform)
    {
        for (int i = 0; i < _prefab.Count; i++)
        {
            _prefab[i].transform.SetParent(parent_transform, false);
        }
    }

    void AssignCounterData(List<GameObject> _counterPrefabList)
    {
        //declare queuePositionList array size
        CustomerQueueHandler.Instance.queuePositionList = new List<Vector3>[_counterPrefabList.Count];

        CustomerHandler.Instance.DeclareCustomerClass(_counterPrefabList.Count, _counterPrefabList[0].transform.GetChild(0).childCount);

        for (int i = 0; i < _counterPrefabList.Count; i++)
        {
            //get queue interaction script
            QueueInteraction queueInteraction = _counterPrefabList[i].GetComponent<QueueInteraction>();
            //assign queue Index
            queueInteraction.queueIndex = i;
            //assign queue location into queuePositionList
            CustomerQueueHandler.Instance.AssignQueueLocation(_counterPrefabList[i], i);
        }
    }

    void AssignMagicChestData(List<GameObject> _magicChestPrefabList, List<int> _ingredientOrderToday)
    {
        for (int i = 0; i < _magicChestPrefabList.Count; i++)
        {
            //get magic chest ShelfInteraction script
            ShelfInteraction shelfInteraction = _magicChestPrefabList[i].GetComponent<ShelfInteraction>();
            //set shelfIndex
            shelfInteraction.shelfIndex = i;
            //set ingredient
            shelfInteraction.ingredientIndex = ingOrder_test[i];
            //set shelf refresh Time
            shelfInteraction.shelfReopenTime = 3f;
        }
    }

    void AssignPotData(List<GameObject> _potPrefabList)
    {
        for (int i = 0; i < _potPrefabList.Count; i++)
        {
            //get PotInformationHandler script
            PotInformationHandler potInformationHandler = _potPrefabList[i].GetComponent<PotInformationHandler>();
            //assign pot Index
            potInformationHandler.potIndex = i;
        }
    }

    public Animator StartDayTimeIntro()
    {
        Animator anim = dayTimeIntro_obj.GetComponent<Animator>();
        anim.SetTrigger("StartIntro");

        return anim;
    }

    public void DisableDayTimeIntro()
    {
        dayTimeIntro_obj.SetActive(false);
    }
}
