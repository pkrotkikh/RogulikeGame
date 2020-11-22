using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Item {

    public enum ItemCategory
    {
        Weapon,
        Armour,
        Potion,
        Gold,
    }

    public enum ItemType
    {
        Helmet,
        Chest,
        Cloak,
        Pants,
        Gloves,
        Boots,
        LeftHand,
        RightHand,

        rightRing,
        leftRing,
        jewelry,
    }

    public enum WeaponType
    {
        None,
        ShortBlade,
        LongBlade,
        Flail,

        Bow,
        Sling,
        Crossbow
    }

    public enum Stats
    {
        Strength,
        Dexterity,
        Intelligence,
        Armour,
        MinDamage,
        MaxDamage,
    }

    public ItemData itemData;
    public int amount;

    public Dictionary<Item.Stats, int> stats;

    public Item(ItemData itemData)
    {
        this.itemData = itemData;
        stats = new Dictionary<Item.Stats, int>();

        StatsInitialize();
    }

    private void StatsInitialize()
    {
        stats.Add(Stats.Strength, itemData.strength);
        stats.Add(Stats.Dexterity, itemData.dexterity);
        stats.Add(Stats.Intelligence, itemData.intelligence);
        stats.Add(Stats.Armour, itemData.armour);
        stats.Add(Stats.MinDamage, itemData.minDamage);
        stats.Add(Stats.MaxDamage, itemData.maxDamage);
    }

    public bool IsStackable()
    {
        switch (itemData.category)
        {
            case ItemCategory.Potion:
            case ItemCategory.Gold:
                return true;

            default:
            case ItemCategory.Weapon:
            case ItemCategory.Armour:
                return false;
        }
    }
}
