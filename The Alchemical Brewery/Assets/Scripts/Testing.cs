using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{

    [SerializeField] private UI_Inventory uiInventory;
    private Inventory inventory;

    private void Awake()
    {
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);

        CraftingSystem craftingSystem = new CraftingSystem();

    }
}
