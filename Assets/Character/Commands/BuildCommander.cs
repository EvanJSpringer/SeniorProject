using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildCommander : SecondaryCommander
{
    Vector3 mousePos;
    float distance;

    public bool canPlace = true;
    bool finish = false;

    GameObject selectedBuilding = null;

    void Update()
    {
        Vector3 position = cam.WorldToScreenPoint(character.transform.position + UiOffset);
        if (transform.position != position)
        {
            transform.position = position;
        }

        ray = cam.ScreenPointToRay(Input.mousePosition);
        if (selectedBuilding != null)
        {
            selectedBuilding.transform.position = new Vector3(mousePos.x, (float)(mousePos.y + 0.235), mousePos.z);
            if (Input.GetMouseButtonDown(1) && canPlace)
            {
                inputGathered = true;
            }
        }
        else if (finish)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (Physics.Raycast(ray, out hit) && interactWith)
                {
                    inputGathered = true;
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (world.Raycast(ray, out distance))
        {
            mousePos = ray.GetPoint(distance);
        }
    }

    public void finishBuild()
    {
        parent.GetComponent<CommandPrompter>().commandSelected = true;
        inputGathered = false;
        interactWith = true;
        finish = true;
        StartCoroutine(WaitFinishBuild());
    }

    IEnumerator WaitFinishBuild()
    {
        yield return new WaitUntil(() => inputGathered == true);
        BlueprintScript blueprint = hit.collider.GetComponent<BlueprintScript>();
        if (blueprint != null)
        {
            character.GetComponent<CharacterController>().setTarget(blueprint);
            blueprint.setAsTarget(character);
            if (interaction != null)
            {
                interaction.interrupt();
            }
            interaction = blueprint;
        }
        inputGathered = false;
        interactWith = false;
        gameObject.SetActive(false);
    }

    public void build(GameObject building)
    {
        parent.GetComponent<CommandPrompter>().commandSelected = true;
        inputGathered = false;
        interactWith = true;
        canPlace = true;
        selectedBuilding = Instantiate(building, mousePos, building.transform.rotation);
        selectedBuilding.GetComponent<BlueprintScript>().buildCommand = this;
        StartCoroutine(WaitBuild());
    }

    IEnumerator WaitBuild()
    {
        yield return new WaitUntil(() => inputGathered == true);
        selectedBuilding.GetComponent<BlueprintScript>().Place();
        character.GetComponent<CharacterController>().setTarget(selectedBuilding.GetComponent<BlueprintScript>());
        selectedBuilding.GetComponent<BlueprintScript>().setAsTarget(character);
        selectedBuilding = null;
        inputGathered = false;
        gameObject.SetActive(false);
    }
}
