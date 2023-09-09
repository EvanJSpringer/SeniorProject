using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintScript : Interaction
{
    public BuildCommander buildCommand;
    public bool placed = true;

    [SerializeField] GameObject building;
    [SerializeField] List<Item> materials;

    Inventory inventory;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Ground")
        {
            //Debug.Log("enter" + other.gameObject.name);
            buildCommand.canPlace = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        //Debug.Log("exit");
        buildCommand.canPlace = true;
    }

    public void Place()
    {
        targetPos = transform.position + new Vector3(0.0f, 0.0f, -0.5f);//building.GetComponent<Interaction>().targetPos;
    }

    public override void interact()
    {
        occupied = true;
        inventory = character.GetComponent<Inventory>();
        for (int i = 0; i < materials.Count; i++)
        {
            if (inventory.ContainsItem(materials[i]))
            {
                inventory.DropItem(materials[i]);
                materials.Remove(materials[i]);
                i--;
            }
        }
        if (materials.Count > 0)
        {
            StorageScript[] stores = (StorageScript[])FindObjectsByType(typeof(StorageScript), FindObjectsSortMode.None);
            List<StorageScript> chosenStores = new List<StorageScript>();
            foreach (StorageScript store in stores)
            {
                foreach (Item mat in materials)
                {
                    if (!chosenStores.Contains(store) && store.items.Contains(mat))
                    {
                        chosenStores.Add(store);
                    }
                }
            }
            StorageScript nearest = null;
            float nearestDist = Mathf.Infinity;
            Vector3 location = character.transform.position;
            foreach (StorageScript store in chosenStores)
            {
                float dist = Vector3.Distance(store.gameObject.transform.position, location);
                if (dist < nearestDist)
                {
                    nearest = store;
                    nearestDist = dist;
                }
            }
            if (nearest != null)
            {
                character.GetComponent<CharacterController>().setTarget(nearest);
                nearest.setAsTarget(character);
                nearest.SetIntent(this, materials);
            }
        }
        else
        {
            GameObject finishedBuilding = Instantiate(building, gameObject.transform.position, building.transform.rotation);
            Destroy(gameObject);
        }
        occupied = false;
    }
}
