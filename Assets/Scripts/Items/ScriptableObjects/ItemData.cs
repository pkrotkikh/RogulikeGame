using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemData : ScriptableObject
{
    [Header("Necessary for all Items")]
    public Item.ItemCategory category;
    public Item.ItemType type;
    public new string name;
    public string description;
    public Sprite artwork;
    public int amount = 1;

    public int strength;
    public int dexterity;
    public int intelligence;

    [Header("For Weapon")]
    public int minDamage;
    public int maxDamage;
    public bool isRanged;
    public bool isTwoHanded;

    [Header("For Armour")]
    public int armour;
}
