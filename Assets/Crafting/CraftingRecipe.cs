using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Crafting/Recipes")]
public class CraftingRecipe : ScriptableObject
{
    public Item result;
    public List<Item> materials = new List<Item>();
}
