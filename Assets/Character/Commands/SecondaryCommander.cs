using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryCommander : MonoBehaviour
{
    [SerializeField] public GameObject character;
    public Inventory inventory;
    public Camera cam;
    [SerializeField] public Vector3 UiOffset;

    public Plane world = new Plane(Vector3.up, 0);

    [SerializeField] public GameObject parent;

    public bool inputGathered = false;
    public Ray ray;
    public RaycastHit hit;

    public bool interactWith = false;
    public Interaction interaction = null;

    void Start()
    {
        cam = parent.GetComponent<CommandPrompter>().cam;
        Vector3 position = cam.WorldToScreenPoint(character.transform.position + UiOffset);
        transform.position = position;
        inventory = character.GetComponent<Inventory>();
    }

    void OnDisable()
    {
        parent.SetActive(false);
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
            if (Physics.Raycast(ray, out hit) && interactWith)
            {
                inputGathered = true;
            }
        }
    }

    public void endInteraction()
    {
        interaction = null;
    }
}
