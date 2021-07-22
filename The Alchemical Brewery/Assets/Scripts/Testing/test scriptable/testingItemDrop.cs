using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testingItemDrop : MonoBehaviour
{
    public GameObject spawnIngredient_obj;
    bool hasCollected = false;
    public bool spawnLoot = false;
    Transform spawnPos;
    public Vector3 spawnPosOffset;

    void Start()
    {
        spawnPos = this.gameObject.transform;
    }

    void Update()
    {
        if(spawnLoot && !hasCollected)
        {
            spawnLoot = false;
            Loot();
        }
    }

    void Loot()
    {
        hasCollected = true;
        StartCoroutine(ShootIngredient());
    }

    IEnumerator ShootIngredient()
    {
        Vector3 spawningPosition = spawnPos.position;
        GameObject spawnedIngredient = Instantiate(spawnIngredient_obj, spawningPosition, Quaternion.identity) as GameObject;
        yield return new WaitForSeconds(.1f);
    }
}
