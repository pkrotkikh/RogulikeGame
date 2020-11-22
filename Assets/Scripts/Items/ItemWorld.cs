using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CodeMonkey.Utils;

public class ItemWorld : MonoBehaviour
{
    private Item item;
    private SpriteRenderer spriteRenderer;
    private TextMeshPro textMeshPro;

    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.ItemWorld, position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
    }

    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.itemData.artwork;

        if(item.amount > 1)
        {
            textMeshPro.SetText(item.amount.ToString());
        }else
        {
            textMeshPro.SetText("");
        }
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public static ItemWorld DropItem(Vector3 dropPosition, Item item)
    {
        Vector3 randomDir = UtilsClass.GetRandomDir();
        ItemWorld itemWorld = SpawnItemWorld(dropPosition, item );

        return itemWorld;
    }
}
