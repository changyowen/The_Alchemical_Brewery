using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform template;
    private Transform slots;

    private void Awake()
    {
        slots = transform.Find("ItemSlots");
        template = slots.Find("ItemTemplate");
    }
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshItems();
    }

    private void RefreshItems()
    {
        //foreach (Transform child in slots)
        //{
        //    if (child == template) continue;
        //    Destroy(child.gameObject);
        //}
        int x = 0;
        int y = 0;
        float itemSlotSize = 30f;
        foreach(Item item in inventory.GetItemList())
        {
            RectTransform slotRectTransform = Instantiate(template,slots).GetComponent<RectTransform>();
            slotRectTransform.gameObject.SetActive(true);

            slotRectTransform.anchoredPosition = new Vector2(38 + x * itemSlotSize, -102 - y * itemSlotSize);
            Image image = slotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            TextMeshProUGUI uiText = slotRectTransform.Find("text").GetComponent<TextMeshProUGUI>();
            if(item.amount > 1)
            {
                uiText.SetText(item.amount.ToString());
            }
            else
            {
                uiText.SetText("");
            }
            

            x++;
            if(x>2)
            {
                x = 0;
                y++;
            }
        }
    }
}
