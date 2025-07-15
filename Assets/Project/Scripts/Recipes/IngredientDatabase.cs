using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

// DTO for ingredients.json
[System.Serializable]
public class IngredientData
{
    public string name;
    public string iconPath;
}

[System.Serializable]
public class IngredientCollection
{
    public List<IngredientData> ingredients;
}


public class IngredientDatabase
{
    private readonly Dictionary<string, Ingredient> _ingredientMap;

    public IngredientDatabase()
    {
        _ingredientMap = new Dictionary<string, Ingredient>();
        string filePath = Path.Combine(Application.streamingAssetsPath, "ingredients.json");
        
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            var ingredientCollection = JsonConvert.DeserializeObject<IngredientCollection>(json);

            foreach (var ingredientData in ingredientCollection.ingredients)
            {
                if (ingredientData != null && !string.IsNullOrEmpty(ingredientData.name) && !_ingredientMap.ContainsKey(ingredientData.name))
                {
                    var newIngredient = new Ingredient { Name = ingredientData.name };
                    if (!string.IsNullOrEmpty(ingredientData.iconPath))
                    {
                        newIngredient.Icon = Resources.Load<Sprite>(ingredientData.iconPath);
                    }
                    _ingredientMap.Add(newIngredient.Name, newIngredient);
                }
            }
        }
        else
        {
            Debug.LogError($"Ingredient file not found at: {filePath}");
        }
        
        Debug.Log($"IngredientDatabase initialized with {_ingredientMap.Count} ingredients.");
    }

    public Ingredient FindIngredientByName(string ingredientName)
    {
        _ingredientMap.TryGetValue(ingredientName, out var ingredient);
        return ingredient;
    }

    public List<Ingredient> GetAllIngredients()
    {
        return _ingredientMap.Values.ToList();
    }
} 