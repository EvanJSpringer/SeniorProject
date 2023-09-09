using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocatorScript : MonoBehaviour
{
    [SerializeField] GameObject cam;
    CameraController camControl;

    [SerializeField] List<GameObject> characters;
    private GameObject characterParent;
    [SerializeField] GameObject character;

    void Start()
    {
        camControl = cam.GetComponent<CameraController>();
    }

    public void clicked()
    {
        //cam.GetComponent<CameraController>().moveCameraToCharacter();

        character = camControl.character;

        if (camControl.following())
        {
            character.GetComponent<CharacterController>().defocus();
            int nextIndex = characters.IndexOf(character.transform.parent.gameObject) + 1;
            if (nextIndex >= characters.Count)
            {
                nextIndex = 0;
            }
            character = characters[nextIndex].transform.GetChild(0).gameObject;
            character.GetComponent<CharacterController>().focus();
        } else
        {
            GameObject nearestCharacter = null;
            float nearestDist = Mathf.Infinity;
            foreach (GameObject chara in characters)
            {
                float dist = Vector3.Distance(transform.position, chara.transform.GetChild(0).gameObject.transform.position);
                if (dist < nearestDist)
                {
                    nearestCharacter = chara.transform.GetChild(0).gameObject;
                    nearestDist = dist;
                }
            }
            character = nearestCharacter;
            character.GetComponent<CharacterController>().focus();
        }
        camControl.moveCameraToCharacter(character);
    }
}
