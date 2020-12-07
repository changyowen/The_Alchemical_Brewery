using UnityEngine;

public class Item
{
    public enum itemType
    {
        Flower1,
        Flower2,
        Flower3,

        Potion1,
        Potion2,
        Potion3,
    }

    public itemType itemtype;
    public int amount;
    private IItemHolder itemHolder;

    public void SetItemHolder(IItemHolder itemHolder)
    {
        this.itemHolder = itemHolder;
    }

    public IItemHolder GetItemHolder()
    {
        return itemHolder;
    }

    public void RemoveFromItemHolder()
    {
        if (itemHolder != null)
        {
            // Remove from current Item Holder
            itemHolder.RemoveItem(this);
        }
    }

    public void MoveToAnotherItemHolder(IItemHolder newItemHolder)
    {
        RemoveFromItemHolder();
        // Add to new Item Holder
        newItemHolder.AddItem(this);
    }

    public Sprite GetSprite()
    {
        return GetSprite(itemtype);
    }
    public static Sprite GetSprite(itemType itemtype)
    {
        switch(itemtype)
        {
            default:
            case itemType.Flower1: return ItemAssets.Instance.Flower1Sprite;
            case itemType.Flower2: return ItemAssets.Instance.Flower2Sprite;
            case itemType.Flower3: return ItemAssets.Instance.Flower3Sprite;
            case itemType.Potion1: return ItemAssets.Instance.Potion1Sprite;
            case itemType.Potion2: return ItemAssets.Instance.Potion2Sprite;
            case itemType.Potion3: return ItemAssets.Instance.Potion3Sprite;
        }
    }
    public bool IsStackable()
    {
        return IsStackable(itemtype);
    }
    public static bool IsStackable(itemType itemtype)
    {
        switch (itemtype)
        {
            default:
            case itemType.Flower1:
            case itemType.Flower2:
            case itemType.Flower3:
                return true;
            case itemType.Potion1:
            case itemType.Potion2:
            case itemType.Potion3:
                return false;
        }
    }

}
