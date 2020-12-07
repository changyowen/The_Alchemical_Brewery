using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem
{
    public const int recipeSize = 4;
    private Item[] itemArray;

    public CraftingSystem()
    {
        itemArray = new Item[recipeSize];
    }

    private bool isEmpty(int x)
    {
        return itemArray[x] == null;
    }

    private Item GetItem(int x)
    {
        return itemArray[x];
    }

    private void SetItem(Item item, int x)
    {
        itemArray[x] = item;
    }

    private void IncreaseItem(int x)
    {
        GetItem(x).amount++;
    }

    private void DecreaseItem(int x)
    {
        GetItem(x).amount--;
    }

    private void RemoveItem(int x)
    {
        SetItem(null, x);
    }

    private bool tryAddItem(Item item, int x)
    {
        if (isEmpty(x))
        {
            SetItem(item, x);
            return true;
        }
        else
        {
            if (item.itemtype == GetItem(x).itemtype)
            {
                IncreaseItem(x);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
