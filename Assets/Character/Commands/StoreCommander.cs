using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreCommander : SecondaryCommander
{
    [SerializeField] GameObject commandButton;

    void OnEnable()
    {
        inventory = character.GetComponent<Inventory>();
        if (inventory.items.Count == 0)
        {
            gameObject.SetActive(false);
            parent.SetActive(true);
        }
        List<Item> buttonItems = new List<Item>();
        for (int i = 0; i < inventory.items.Count; i++)
        {
            if (!buttonItems.Contains(inventory.items[i]))
            {
                buttonItems.Add(inventory.items[i]);
                int slot = i;
                GameObject newButton = Instantiate(commandButton);
                newButton.GetComponent<Button>().onClick.AddListener(() => store(slot));
                newButton.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = " Store " + inventory.items[i].type;
                newButton.transform.parent = gameObject.transform.GetChild(1);
            }
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

    public void store(int slot)
    {
        parent.GetComponent<CommandPrompter>().commandSelected = true;
        inputGathered = false;
        interactWith = true;
        StartCoroutine(WaitStore(slot));
    }

    IEnumerator WaitStore(int slot)
    {
        yield return new WaitUntil(() => inputGathered == true);
        StorageScript storage = hit.collider.GetComponent<StorageScript>();
        if (storage != null)
        {
            character.GetComponent<CharacterController>().setTarget(storage);
            storage.setAsTarget(character);
            storage.SetIntent(true, inventory.items[slot]);
            if (interaction != null)
            {
                interaction.interrupt();
            }
            interaction = storage;
        }
        inputGathered = false;
        interactWith = false;
        gameObject.SetActive(false);
    }
}
