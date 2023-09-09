using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : Harvestable
{
    private Animator animator;
    private Coroutine interCo;

    void Start()
    {
        targetPos = transform.position + new Vector3(-0.425f, -0.4f, -0.6f);
        animator = gameObject.GetComponent<Animator>();
    }

    public override void interact()
    {
        if (character.GetComponent<Inventory>().ContainsItem("Stone Axe"))
        {
            occupied = true;
            character.GetComponent<Animator>().SetBool("Chopping", true);
            interCo = StartCoroutine(WaitForAnim());
        } else
        {
            Debug.Log("Axe needed.");
            character.GetComponent<CharacterController>().getCommandPrompter().endInteraction();
        }
    }

    public override void interrupt()
    {
        occupied = false;
        character.GetComponent<Animator>().SetBool("Chopping", false);
        character.GetComponent<CharacterController>().getCommandPrompter().endInteraction();
        if (interCo != null)
        {
            StopCoroutine(interCo);
        }
    }

    IEnumerator WaitForAnim()
    {
        yield return new WaitForSeconds(3.2f);
        if (character.GetComponent<Animator>().GetBool("Chopping"))
        {
            character.GetComponent<Animator>().SetBool("Chopping", false);
            character.GetComponent<CharacterController>().getCommandPrompter().endInteraction();
            animator.SetBool("Fall", true);
            StartCoroutine(WaitForTreeFall());
        }
    }

    IEnumerator WaitForTreeFall()
    {
        yield return new WaitForSeconds(1.25f);
        Destroy(gameObject);
        Vector3 logPos = transform.position + new Vector3(0.5f, -0.95f, -0.3f);
        GameObject logItem = Instantiate(drop, logPos, transform.rotation);
        logItem.SetActive(true);
        character.GetComponent<CharacterController>().setTarget(logItem.GetComponent<ItemInteraction>());
        logItem.GetComponent<ItemInteraction>().setAsTarget(character);
    }
}
