using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour{

    public event EventHandler OnItemListChanged;

    public List<ItemData> startingEquipment;
    public Equipment equipment;
    private List<Item> itemList;
    private Action<Item> useItemAction;

    private void Awake()
    {
        itemList = new List<Item>();
        AddStartingItems();
        equipment = gameObject.GetComponent<Equipment>();
    }

    public void AddStartingItems()
    {
        foreach (ItemData equip in startingEquipment)
        {
            AddItem(new Item(equip));
        }
    }

    public void AddItem(Item item)
    {
        if (item.IsStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach(Item inventoryItem in itemList)
            {
                if(inventoryItem.itemData.category == item.itemData.category)
                {
                    inventoryItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }
            if (!itemAlreadyInInventory)
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

    public void RemoveItem(Item item)
    {
        if (item.IsStackable())
        {
            Item itemInInventory = null;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemData == item.itemData)
                {
                    inventoryItem.amount -= item.amount;
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.amount <= 0)
            {
                itemList.Remove(itemInInventory);
            }
        } else {
            itemList.Remove(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void UseItem(Item item)
    {
        //Если предмет, на который мы нажимаем надет в данный момент - снять
        if(equipment.itemList[item.itemData.type] == item)
        {
            equipment.UnequipItem(item);
        }
        else //Если предмет на который мы нажимаем не надет - надеть
        {
            equipment.EquipItem(item);
        }
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }

}
