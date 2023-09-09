using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int slots = 4;
    public List<Item> items = new List<Item>();

    public delegate void InventoryUpdate(Inventory inven);
    public InventoryUpdate update;

    public void AddItem(Item item)
    {
        items.Add(item);
        update.Invoke(this);
    }

    public void DropItem(Item item)
    {
        items.Remove(item);
        update.Invoke(this);
    }

    public void FocusInv()
    {
        update.Invoke(this);
    }

    public bool ContainsItem(string item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].type.Equals(item))
            {
                return true;
            }
        }
        return false;
    }

    public bool ContainsItem(Item item)
    {
        return items.Contains(item);
    }
}
