using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageScript : Interaction
{

    [SerializeField] int slots = 10;
    public List<Item> items = new List<Item>();
    private bool intent;
    private Item item;

    private BlueprintScript building = null;
    private List<Item> desiredItems = null;
    Inventory inventory;

    void Start()
    {
        targetPos = transform.position + new Vector3(0.0f, 0.0f, -0.5f);
    }

    public override void interact()
    {
        inventory = character.GetComponent<Inventory>();
        if (building != null)
        {
            RemoveItems(desiredItems);
            character.GetComponent<CharacterController>().setTarget(building);
            building.setAsTarget(character);
        } else
        {
            if (intent)
            {
                AddItem(item);
            }
            else
            {
                RemoveItem(item);
            }
        }
    }

    public void AddItem(Item item)
    {
        items.Add(item);
        inventory.DropItem(item);
    }

    public void RemoveItem(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            inventory.AddItem(item);
        }
        else
        {
            Debug.Log("Item not present.");
        }
    }

    public void RemoveItems(List<Item> itemList)
    {
        foreach (Item item in itemList)
        {
            if (items.Contains(item))
            {
                items.Remove(item);
                inventory.AddItem(item);
            }
        }
    }

    public void SetIntent(bool intent, Item item)
    {
        this.intent = intent;
        this.item = item;
    }

    public void SetIntent(BlueprintScript building, List<Item> items)
    {
        this.building = building;
        this.desiredItems = items;
    }
}
