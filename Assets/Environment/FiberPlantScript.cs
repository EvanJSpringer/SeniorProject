using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiberPlantScript : Harvestable
{
    void Start()
    {
        targetPos = transform.position + new Vector3(-0.5f, 0.0f, -0.2f);
    }

    public override void interact()
    {
        if (character.GetComponent<Inventory>().items.Count < character.GetComponent<Inventory>().slots)
        {
            character.GetComponent<Animator>().SetBool("PickUp", true);
            StartCoroutine(WaitForAnim());
        } else
        {
            Debug.Log("Not Enough Space.");
        }
    }

    IEnumerator WaitForAnim()
    {
        yield return new WaitForSeconds(1.0f);
        character.GetComponent<Animator>().SetBool("PickUp", false);
        character.GetComponent<Inventory>().AddItem(drop.GetComponent<ItemInteraction>().item);
        Destroy(gameObject);      
    }
}
