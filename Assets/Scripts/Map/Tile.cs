using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class Tile : MonoBehaviour
{

    [Header("Set Dynamically")]
    public GameObject creature;
    public List<ItemWorld> items;

    public void RemoveItem(Item item)
    {
        foreach (ItemWorld itemWorld in items)
        {
            if(itemWorld.GetItem() == item)
            {
                items.Remove(itemWorld);
                Destroy(itemWorld);
                break;
            }
        }
    }

    public void AddItem(ItemWorld item)
    {
        items.Add(item);
    }
}
