using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CodeMonkey.Utils;

public class UI_Inventory : MonoBehaviour
{
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    private Inventory inventory;
    private Player player;

    private void Awake()
    {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemChanged;
        RefreshInventoryItems();
    }

    private void Inventory_OnItemChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        foreach(Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotCellSize = 50f;

        foreach (Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
            {
                //Use Item
                inventory.UseItem(item);
                if(inventory.equipment.itemList[item.itemData.type] == item)
                {
                    RectTransform background = (RectTransform)itemSlotRectTransform.Find("background");
                    Image backgroundImage = background.gameObject.GetComponent<Image>();
                    backgroundImage.color = new Color32(150, 150, 150, 255);
                }
                else
                {
                    Image background = (Image)itemSlotRectTransform.Find("background").GetComponent<Image>();
                    Image backgroundImage = background.gameObject.GetComponent<Image>();
                    backgroundImage.color = new Color32(90, 90, 90, 255);
                }
            };
            itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () =>
            {
                //Drop Item
                Item duplicateItem = new Item (item.itemData);
                inventory.RemoveItem(item);
                inventory.equipment.UnequipItem(item);
                ItemWorld itemWorld = ItemWorld.DropItem(player.GetPosition(), duplicateItem);
                player.tile.AddItem(itemWorld);
                player.uiGroundItems.RefreshGroundItems(player.GetItemsGround());
            };

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
            image.sprite = item.itemData.artwork;

            TextMeshProUGUI uiText = itemSlotRectTransform.Find("text").GetComponent<TextMeshProUGUI>();
            if(item.amount > 1)
            {
                uiText.SetText(item.amount.ToString());
            }
            else
            {
                uiText.SetText("");
            }

            x++;
            if (x > 3)
            {
                x = 0;
                y--;
            }
        }
    }

}
