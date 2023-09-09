using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;
    public Transform transform;

    [SerializeField] Transform target;
    [SerializeField] CommandPrompter commander;
    [SerializeField] GameObject inventoryUIHolder;
    InventoryScript inventoryUI;

    [SerializeField] GameObject camObject;
    Camera cam;

    private Interaction goal = null;
    private bool focused = false;

    float x;
    float z;

    Plane world = new Plane(Vector3.up, 0);

    void Start()
    {
        agent.updateUpAxis = false;
        cam = camObject.transform.GetChild(0).gameObject.GetComponent<Camera>();
        inventoryUI = inventoryUIHolder.GetComponent<InventoryScript>();
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (gameObject.GetComponent<BoxCollider>() == hit.collider)
                {
                    commander.gameObject.SetActive(true);
                    focus();
                }
                else if (!commander.commandSelected)
                {
                    commander.gameObject.SetActive(false);
                }
            }
        }
    }

    void FixedUpdate()
    {
        agent.SetDestination(target.position);

        x = agent.velocity.x;
        z = agent.velocity.z;

        if (x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        } else if (x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (Mathf.Abs(x) < 0.01f && Mathf.Abs(z) < 0.01f)
        {
            animator.SetBool("Walking", false);
            if (goal != null && transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            animator.SetBool("Walking", true);
        }

        if (Mathf.Abs(x) > Mathf.Abs(z))
        {
            animator.SetBool("Walking_Side", true);
        } else
        {
            animator.SetBool("Walking_Side", false);
        }

        if (z > 0.01f)
        {
            animator.SetBool("Walking_Forward", false);
        } else
        {
            animator.SetBool("Walking_Forward", true);
        }
    }

    public void setTarget(Interaction targetObject)
    {
        goal = targetObject;
        target.position = targetObject.targetPos;
    }

    public void setDestination(Vector3 targetPos)
    {
        target.position = targetPos;
    }

    public void focus()
    {
        focused = true;
        inventoryUI.gameObject.SetActive(true);
        inventoryUI.SetCharacter(gameObject.GetComponent<Inventory>());
        gameObject.GetComponent<Inventory>().FocusInv();
        camObject.GetComponent<CameraController>().moveCameraToCharacter(this.gameObject);
    }

    public void defocus()
    {
        focused = false;
        inventoryUI.gameObject.SetActive(false);
    }

    public CommandPrompter getCommandPrompter()
    {
        return commander;
    }
}
