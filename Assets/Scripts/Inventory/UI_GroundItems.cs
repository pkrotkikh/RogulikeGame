using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Specialized;

public class UI_GroundItems : MonoBehaviour
{
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;
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

    public void RefreshGroundItems(List<ItemWorld> items)
    {
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotCellSize = 50f;

        foreach (ItemWorld itemWorld in items)
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
            {
                //LeftClick on item
                player.inventory.AddItem(itemWorld.GetItem());
                player.tile.RemoveItem(itemWorld.GetItem());
                Destroy(itemSlotRectTransform.gameObject);
                Destroy(itemWorld.gameObject);
                RefreshGroundItems(player.tile.items);
            };


            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
            image.sprite = itemWorld.GetItem().itemData.artwork;

            TextMeshProUGUI uiText = itemSlotRectTransform.Find("text").GetComponent<TextMeshProUGUI>();
            if (itemWorld.GetItem().amount > 1)
            {
                uiText.SetText(itemWorld.GetItem().amount.ToString());
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
