using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingCommander : SecondaryCommander
{
    public void craft(Item item)
    {
        parent.GetComponent<CommandPrompter>().commandSelected = true;

        CraftingScript[] workstations = (CraftingScript[])FindObjectsByType(typeof(CraftingScript), FindObjectsSortMode.None);

        CraftingScript nearest = null;
        float nearestDist = Mathf.Infinity;
        Vector3 location = character.transform.position;
        foreach (CraftingScript station in workstations)
        {
            float dist = Vector3.Distance(station.gameObject.transform.position, location);
            if (dist < nearestDist)
            {
                nearest = station;
                nearestDist = dist;
            }
        }

        character.GetComponent<CharacterController>().setTarget(nearest);
        nearest.setAsTarget(character);
        nearest.SetItem(item);
        if (interaction != null)
        {
            interaction.interrupt();
        }
        interaction = nearest;

        gameObject.SetActive(false);
    }
}
