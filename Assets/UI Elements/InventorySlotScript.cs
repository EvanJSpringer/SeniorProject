using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotScript : MonoBehaviour
{
    public Image icon;
    Item item;

    public void AddIcon(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void RemoveIcon()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }
}
