using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandPrompter : MonoBehaviour
{
    [SerializeField] GameObject character;
    Inventory inventory;
    [SerializeField] public Camera cam;
    [SerializeField] Vector3 UiOffset;

    Plane world = new Plane(Vector3.up, 0);

    GameObject secondary;
    [SerializeField] GameObject harvestCom;
    [SerializeField] GameObject gatherCom;
    [SerializeField] GameObject craftCom;
    [SerializeField] GameObject storeCom;
    [SerializeField] GameObject getCom;
    bool secondaryActive = false;

    [SerializeField] bool inputGathered = false;
    Ray ray;
    RaycastHit hit;
    float distance;

    public bool commandSelected = false;
    bool goTo = false;
    bool interactWith = false;

    Interaction interaction = null;

    void OnEnable()
    {
        commandSelected = false;
        Vector3 position = cam.WorldToScreenPoint(character.transform.position + UiOffset);
        transform.position = position;
        inventory = character.GetComponent<Inventory>();
    }

    void OnDisable()
    {
        if (secondaryActive)
        {
            secondary.SetActive(false);
        }
    }

    void Update()
    {
        Vector3 position = cam.WorldToScreenPoint(character.transform.position + UiOffset);
        if (transform.position != position)
        {
            transform.position = position;
        }

        ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(1))
        {
            if (world.Raycast(ray, out distance) && goTo)
            {
                inputGathered = true;
            }
            else if (Physics.Raycast(ray, out hit) && interactWith)
            {
                inputGathered = true;
            }
        }
    }

    public void endInteraction()
    {
        interaction = null;
    }

    public void GoTo()
    {
        if (secondaryActive)
        {
            secondary.SetActive(false);
            gameObject.SetActive(true);
        }
        commandSelected = true;
        inputGathered = false;
        goTo = true;
        StartCoroutine(WaitGoTo());
    }

    IEnumerator WaitGoTo()
    {
        yield return new WaitUntil(() => inputGathered == true);
        character.GetComponent<CharacterController>().setDestination(ray.GetPoint(distance));
        inputGathered = false;
        goTo = false;
        gameObject.SetActive(false);
        if (interaction != null)
        {
            interaction.interrupt();
        }
    }

    public void secondaryCommander(GameObject sec)
    {
        if (secondaryActive)
        {
            secondary.SetActive(false);
            gameObject.SetActive(true);
        }
        secondary = sec;
        secondary.SetActive(true);
        secondaryActive = true;
    }
}
