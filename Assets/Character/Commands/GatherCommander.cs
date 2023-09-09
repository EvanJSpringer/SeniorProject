using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherCommander : SecondaryCommander
{
    public void gather()
    {
        parent.GetComponent<CommandPrompter>().commandSelected = true;
        inputGathered = false;
        interactWith = true;
        StartCoroutine(WaitGather());
    }

    IEnumerator WaitGather()
    {
        yield return new WaitUntil(() => inputGathered == true);
        ItemInteraction item = hit.collider.GetComponent<ItemInteraction>();
        if (item != null)
        {
            character.GetComponent<CharacterController>().setTarget(item);
            item.setAsTarget(character);
            if (interaction != null)
            {
                interaction.interrupt();
            }
            interaction = item;
        }
        inputGathered = false;
        interactWith = false;
        gameObject.SetActive(false);
    }

    public void gatherItem(Item item)
    {
        parent.GetComponent<CommandPrompter>().commandSelected = true;

        ItemInteraction[] itemSet = (ItemInteraction[])FindObjectsByType(typeof(ItemInteraction), FindObjectsSortMode.None);
        List<ItemInteraction> itemList = new List<ItemInteraction>();

        foreach (ItemInteraction element in itemSet)
        {
            if (element.item.type == item.type)
            {
                itemList.Add(element);
            }
        }

        ItemInteraction nearest = null;
        float nearestDist = Mathf.Infinity;
        Vector3 location = character.transform.position;
        foreach (ItemInteraction thing in itemList)
        {
            float dist = Vector3.Distance(thing.gameObject.transform.position, location);
            if (dist < nearestDist)
            {
                nearest = thing;
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
