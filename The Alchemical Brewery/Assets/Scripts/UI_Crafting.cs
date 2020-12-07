using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Crafting : MonoBehaviour
{
    [SerializeField] private Transform UI_Prefab;

    private Transform[] slotArray;
    public Transform outputSlot;
    private Transform itemContainer;
    private Transform outputContainer;

    private void Awake()
    {
        Transform recipeContainer = transform.Find("CraftGrid");
        itemContainer = transform.Find("itemContainer");
        outputContainer = transform.Find("OutputContainer");

        slotArray = new Transform[CraftingSystem.recipeSize];

        for(int x = 0; x < CraftingSystem.recipeSize; x++)
        {
            slotArray[x] = recipeContainer.Find("Grid" + x);
        }

        //outputSlot = transform.Find("OutputSlot");

        CreateItem(0, new Item { itemtype = Item.itemType.Flower1 });
        CreateItem(1, new Item { itemtype = Item.itemType.Flower1 });
        CreateOutput(new Item { itemtype = Item.itemType.Potion3 });
    }

    private void CreateItem(int x, Item item)
    {
        Transform itemTransform = Instantiate(UI_Prefab, itemContainer);
        RectTransform itemRectTransform = itemTransform.GetComponent<RectTransform>();
        itemRectTransform.anchoredPosition = slotArray[x].GetComponent<RectTransform>().anchoredPosition;
        itemTransform.GetComponent<UI_Item>().SetItem(item);
    }

    private void CreateOutput(Item item)
    {
        Transform itemTransform = Instantiate(UI_Prefab, outputContainer);
        RectTransform itemRectTransform = itemTransform.GetComponent<RectTransform>();
        itemRectTransform.anchoredPosition = outputSlot.GetComponent<RectTransform>().anchoredPosition;
        itemTransform.GetComponent<UI_Item>().SetItem(item);
    }
}
