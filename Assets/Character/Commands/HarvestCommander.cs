using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestCommander : SecondaryCommander
{
    public void harvest()
    {
        parent.GetComponent<CommandPrompter>().commandSelected = true;
        inputGathered = false;
        interactWith = true;
        StartCoroutine(WaitHarvest());
    }

    IEnumerator WaitHarvest()
    {
        yield return new WaitUntil(() => inputGathered == true);
        Harvestable harvestable = hit.collider.GetComponent<Harvestable>();
        if (harvestable != null)
        {
            character.GetComponent<CharacterController>().setTarget(harvestable);
            harvestable.setAsTarget(character);
            if (interaction != null)
            {
                interaction.interrupt();
            }
            interaction = harvestable;
        }
        inputGathered = false;
        interactWith = false;
        gameObject.SetActive(false);
    }

    public void harvestTree()
    {
        parent.GetComponent<CommandPrompter>().commandSelected = true;

        TreeScript[] trees = (TreeScript[])FindObjectsByType(typeof(TreeScript), FindObjectsSortMode.None);

        TreeScript nearest = null;
        float nearestDist = Mathf.Infinity;
        Vector3 location = character.transform.position;
        foreach (TreeScript tree in trees)
        {
            float dist = Vector3.Distance(tree.gameObject.transform.position, location);
            if (dist < nearestDist)
            {
                nearest = tree;
                nearestDist = dist;
            }
        }

        character.GetComponent<CharacterController>().setTarget(nearest);
        nearest.setAsTarget(character);
        if (interaction != null)
        {
            interaction.interrupt();
        }
        interaction = nearest;

        gameObject.SetActive(false);
    }

    public void harvestFiberPlant()
    {
        parent.GetComponent<CommandPrompter>().commandSelected = true;

        FiberPlantScript[] plants = (FiberPlantScript[])FindObjectsByType(typeof(FiberPlantScript), FindObjectsSortMode.None);

        FiberPlantScript nearest = null;
        float nearestDist = Mathf.Infinity;
        Vector3 location = character.transform.position;
        foreach (FiberPlantScript plant in plants)
        {
            float dist = Vector3.Distance(plant.gameObject.transform.position, location);
            if (dist < nearestDist)
            {
                nearest = plant;
                nearestDist = dist;
            }
        }

        character.GetComponent<CharacterController>().setTarget(nearest);
        nearest.setAsTarget(character);
        if (interaction != null)
        {
            interaction.interrupt();
        }
        interaction = nearest;

        gameObject.SetActive(false);
    }
}
