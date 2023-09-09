using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetCommander : SecondaryCommander
{
    [SerializeField] GameObject commandButton;

    void OnEnable()
    {
        StorageScript[] stores = (StorageScript[])FindObjectsByType(typeof(StorageScript), FindObjectsSortMode.None);
        List<Item> buttonItems = new List<Item>();
        foreach (StorageScript store in stores)
        {
            foreach (Item thing in store.items)
            {
                if (!buttonItems.Contains(thing))
                {
                    buttonItems.Add(thing);
                    GameObject newButton = Instantiate(commandButton);
                    newButton.GetComponent<Button>().onClick.AddListener(() => get(thing));
                    newButton.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = " Get " + thing.type;
                    newButton.transform.parent = gameObject.transform.GetChild(1);
                }
            }
        }
        if (buttonItems.Count == 0)
        {
            gameObject.SetActive(false);
            parent.SetActive(true);
        }
    }

    void OnDisable()
    {
        var commandList = gameObject.transform.GetChild(1);
        for (int i = 0; i < commandList.childCount; i++)
        {
            Destroy(commandList.GetChild(i).gameObject);
        }
        parent.SetActive(false);
    }

    public void get(Item item)
    {
        parent.GetComponent<CommandPrompter>().commandSelected = true;

        StorageScript[] stores = (StorageScript[])FindObjectsByType(typeof(StorageScript), FindObjectsSortMode.None);

        StorageScript nearest = null;
        float nearestDist = Mathf.Infinity;
        Vector3 location = character.transform.position;
        foreach (StorageScript store in stores)
        {
            if (store.items.Contains(item))
            {
                float dist = Vector3.Distance(store.gameObject.transform.position, location);
                if (dist < nearestDist)
                {
                    nearest = store;
                    nearestDist = dist;
                }
            }
        }

        character.GetComponent<CharacterController>().setTarget(nearest);
        nearest.setAsTarget(character);
        nearest.SetIntent(false, item);
        if (interaction != null)
        {
            interaction.interrupt();
        }
        interaction = nearest;

        gameObject.SetActive(false);
    }
}
