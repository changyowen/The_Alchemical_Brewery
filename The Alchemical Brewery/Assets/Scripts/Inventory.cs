using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{

    public event EventHandler OnItemListChanged;
  
    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();
        AddItem(new Item { itemtype = Item.itemType.Flower1, amount = 1 });
        AddItem(new Item { itemtype = Item.itemType.Flower2, amount = 5 });
        AddItem(new Item { itemtype = Item.itemType.Flower3, amount = 5 });


        Debug.Log(itemList.Count);
    }



    public void AddItem(Item item)
    {
        if(item.IsStackable())
        {
            bool itemininventory = false;
            foreach(Item inventoryItem in itemList)
            {
                if(inventoryItem.itemtype == item.itemtype )
                {
                    inventoryItem.amount += item.amount;
                    itemininventory = true;
                }
            }
            if(!itemininventory)
            {
                itemList.Add(item);
            }
        }
        else
        {
            itemList.Add(item);
        }

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
