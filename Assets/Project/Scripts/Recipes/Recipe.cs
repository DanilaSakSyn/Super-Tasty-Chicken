using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Recipe
{
    public string Name;
    public string Description;
    public Sprite Icon; // Field for the recipe's icon
    public List<Ingredient> RequiredIngredients;
    public string Instructions;
} 