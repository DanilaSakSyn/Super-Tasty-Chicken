using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

// DTO classes for JSON deserialization
[System.Serializable]
public class RecipeData
{
    public string name;
    public string description;
    public string iconPath; // Add iconPath
    public List<string> requiredIngredients;
    public string instructions;
}

[System.Serializable]
public class RecipeCollection
{
    public List<RecipeData> recipes;
}

public class RecipeLoader
{
    private readonly IngredientDatabase _ingredientDatabase;
    private readonly string _recipeFilePath;

    public RecipeLoader(IngredientDatabase ingredientDatabase)
    {
        _ingredientDatabase = ingredientDatabase;
        _recipeFilePath = Path.Combine(Application.streamingAssetsPath, "recipes.json");
    }

    public List<Recipe> LoadRecipes()
    {
        if (!File.Exists(_recipeFilePath))
        {
            Debug.LogError($"Recipe file not found at: {_recipeFilePath}");
            return new List<Recipe>();
        }

        string json = File.ReadAllText(_recipeFilePath);
        RecipeCollection recipeCollection = JsonConvert.DeserializeObject<RecipeCollection>(json);

        List<Recipe> loadedRecipes = new List<Recipe>();
        foreach (var recipeData in recipeCollection.recipes)
        {
            Recipe recipe = new Recipe(); // Changed from ScriptableObject.CreateInstance
            recipe.Name = recipeData.name;
            recipe.Description = recipeData.description;
            recipe.Instructions = recipeData.instructions;

            // Load the icon from Resources
            if (!string.IsNullOrEmpty(recipeData.iconPath))
            {
                recipe.Icon = Resources.Load<Sprite>(recipeData.iconPath);
            }
            
            recipe.RequiredIngredients = new List<Ingredient>();
            foreach (var ingredientName in recipeData.requiredIngredients)
            {
                Ingredient ingredient = _ingredientDatabase.FindIngredientByName(ingredientName);
                if (ingredient != null)
                {
                    recipe.RequiredIngredients.Add(ingredient);
                }
                else
                {
                    Debug.LogWarning($"Ingredient '{ingredientName}' not found in the database for recipe '{recipeData.name}'.");
                }
            }
            loadedRecipes.Add(recipe);
        }

        Debug.Log($"Successfully loaded {loadedRecipes.Count} recipes from JSON.");
        return loadedRecipes;
    }
} 