using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{

    public Dictionary<Item.ItemType, Item> itemList;
    private Player player;

    private void Start()
    {
        itemList = new Dictionary<Item.ItemType, Item>();
        player = gameObject.GetComponent<Player>();
        EquipmentInitialize();
    }

    private void EquipmentInitialize()
    {
        itemList.Add(Item.ItemType.Helmet, null);
        itemList.Add(Item.ItemType.Chest, null);
        itemList.Add(Item.ItemType.Pants, null);
        itemList.Add(Item.ItemType.Boots, null);
        itemList.Add(Item.ItemType.Gloves, null);
        itemList.Add(Item.ItemType.Cloak, null);
        itemList.Add(Item.ItemType.RightHand, null);
        itemList.Add(Item.ItemType.LeftHand, null);
        itemList.Add(Item.ItemType.rightRing, null);
        itemList.Add(Item.ItemType.leftRing, null);
        itemList.Add(Item.ItemType.jewelry, null);
    }

    public void UnequipItem(Item item)
    {
        if(itemList[item.itemData.type] == item)
        {
            itemList[item.itemData.type] = null;
        }

        player.RefreshStats();
    }

    public void EquipItem(Item item)
    {
        itemList[item.itemData.type] = item;

        player.RefreshStats();
    }
}
