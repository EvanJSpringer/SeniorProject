using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public List<GameObject> characters;
    public List<GameObject> queue;
    public GameObject character;
    public Vector3 targetPos;

    public int isATarget = 0;
    public bool occupied = false;

    public virtual void interact(){}
    public virtual void interrupt(){}

    void Update()
    {
        foreach (GameObject charas in characters)
        {
            if (occupied && Vector3.Distance(charas.transform.position, targetPos) <= 1.0f)
            {
                if (!queue.Contains(charas))
                {
                    queue.Add(charas);
                    charas.GetComponent<CharacterController>().setDestination(charas.transform.position);
                }
            }
            else if (isATarget > 0 && Vector3.Distance(charas.transform.position, targetPos) <= 0.5f)
            {
                character = charas;
                interact();
                isATarget--;
                characters.Remove(character);
                break;
            }
        }
        if (!occupied)
        {
            foreach (GameObject charas in queue)
            {
                charas.GetComponent<CharacterController>().setDestination(targetPos);
            }
        }
        
    }

    public void setAsTarget(GameObject character)
    {
        isATarget++;
        characters.Add(character);
    }

    public void removeTarget()
    {
        isATarget--;
    }
}
