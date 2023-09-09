using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //[SerializeField] List<GameObject> characters;
    public GameObject character;

    [SerializeField] float speed = 5.0f;
    [SerializeField] bool characterCamera;

    void Start()
    {
        characterCamera = false;
    }

    void Update()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.01f || Mathf.Abs(Input.GetAxis("Vertical")) > 0.01f)
        {
            characterCamera = false;
        }

        if (characterCamera)
        {
            //transform.position = character.transform.GetChild(0).gameObject.transform.position;
            transform.position = character.transform.position;
        }
        else
        {
            transform.position += transform.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime;
            transform.position += transform.up * Input.GetAxis("Vertical") * speed * Time.deltaTime;
        }
    }

    public void moveCameraToCharacter(GameObject chara)
    {
        /*if (characterCamera)
        {
            character.transform.GetChild(0).GetComponent<CharacterController>().defocus();
            int nextIndex = characters.IndexOf(character) + 1;
            if (nextIndex >= characters.Count)
            {
                nextIndex = 0;
            }
            character = characters[nextIndex];
            character.transform.GetChild(0).gameObject.GetComponent<CharacterController>().focus();
        } else
        {
            GameObject nearestCharacter = null;
            float nearestDist = Mathf.Infinity;
            foreach (GameObject chara in characters)
            {
                float dist = Vector3.Distance(transform.position, chara.transform.GetChild(0).gameObject.transform.position);
                if (dist < nearestDist)
                {
                    nearestCharacter = chara;
                    nearestDist = dist;
                }
            }
            character = nearestCharacter;
            character.transform.GetChild(0).gameObject.GetComponent<CharacterController>().focus();
            characterCamera = true;
        }*/
        characterCamera = true;
        character = chara;
    }

    public bool following()
    {
        return characterCamera;
    }
}
