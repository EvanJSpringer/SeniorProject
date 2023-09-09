using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    //public GameObject character;
    public Transform UIParent;
    [SerializeField] Inventory inventory;
    [SerializeField] InventorySlotScript[] slots;

    void Start()
    {
        //inventory = character.GetComponent<Inventory>();
        //inventory.update += UpdateInventory;
        slots = UIParent.GetComponentsInChildren<InventorySlotScript>();
    }

    public void SetCharacter(Inventory inven)
    {
        //this.character = character;
        inventory = inven;
        inventory.update += UpdateInventory;
    }

    void UpdateInventory(Inventory inven)
    {
        if (inven == inventory)
        {
            //Debug.Log("before?" + slots.Length);
            for (int i = 0; i < slots.Length; i++)
            {
                if (i < inventory.items.Count)
                {
                    slots[i].AddIcon(inventory.items[i]);
                }
                else
                {
                    slots[i].RemoveIcon();
                }
            }
        }
    }
}
