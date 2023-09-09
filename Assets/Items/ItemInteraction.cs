using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : Interaction
{
    public Item item;

    void Awake()
    {
        targetPos = transform.position + new Vector3(-0.5f, 0.0f, 0.0f);
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
        Destroy(gameObject);
        character.GetComponent<Inventory>().AddItem(item);
    }
}
