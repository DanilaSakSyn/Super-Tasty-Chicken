using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public static RecipeManager Instance { get; private set; }

    private List<Recipe> _allRecipes;
    public List<Recipe> AllRecipes => _allRecipes;
    
    private void Awake()
    {
        // Singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // The database is now created at runtime and loads all ingredients from Resources automatically
        IngredientDatabase ingredientDatabase = new IngredientDatabase();
        RecipeLoader loader = new RecipeLoader(ingredientDatabase);
        _allRecipes = loader.LoadRecipes();
    }

    /// <summary>
    /// Finds all recipes that can be cooked with the provided ingredients.
    /// </summary>
    /// <param name="availableIngredients">A list of ingredients that the player has.</param>
    /// <returns>A list of recipes that can be cooked.</returns>
    public List<Recipe> FindAvailableRecipes(List<Ingredient> availableIngredients)
    {
        List<Recipe> availableRecipes = new List<Recipe>();
        HashSet<Ingredient> availableIngredientsSet = new HashSet<Ingredient>(availableIngredients);

        foreach (var recipe in _allRecipes)
        {
            if (recipe.RequiredIngredients.Count > 0 && recipe.RequiredIngredients.All(requiredIngredient => availableIngredientsSet.Contains(requiredIngredient)))
            {
                availableRecipes.Add(recipe);
            }
        }

        return availableRecipes;
    }
} 