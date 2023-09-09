using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingScript : Interaction
{
    Item item;
    Inventory inventory;
    public List<CraftingRecipe> recipes = new List<CraftingRecipe>();

    void Start()
    {
        targetPos = transform.position + new Vector3(0.0f, 0.0f, -0.25f);
    }

    public override void interact()
    {
        occupied = true;
        inventory = character.GetComponent<Inventory>();
        int matDropped = 0;
        foreach (CraftingRecipe recipe in recipes)
        {
            if (recipe.result == item)
            {
                foreach (Item mat in recipe.materials)
                {
                    if (inventory.ContainsItem(mat))
                    {
                        matDropped++;
                    } else
                    {
                        break;
                    }
                }
                if (matDropped == recipe.materials.Count)
                {
                    character.GetComponent<Animator>().SetBool("Crafting", true);
                    StartCoroutine(WaitForAnim(recipe));
                    
                } else
                {
                    Debug.Log("Not enough materials.");
                }
                break;
            }
        }
        occupied = false;
    }

    IEnumerator WaitForAnim(CraftingRecipe recipe)
    {
        yield return new WaitForSeconds(1.5f);
        character.GetComponent<Animator>().SetBool("Crafting", false);
        foreach (Item mat in recipe.materials)
        {
            inventory.DropItem(mat);
        }
        inventory.AddItem(recipe.result);
    }

    public void SetItem(Item item)
    {
        this.item = item;
    }
}
