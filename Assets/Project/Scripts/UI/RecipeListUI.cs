using System.Collections.Generic;
using UnityEngine;

public class RecipeListUI : MonoBehaviour
{
    [SerializeField] private GameObject _recipeItemPrefab;
    [SerializeField] private Transform _contentParent;

    private void Start()
    {
        // Subscribe to inventory changes to automatically update the recipe list
        PlayerInventory.Instance.OnInventoryChanged += HandleInventoryChange;
        
        // Initial population of the recipe list
        HandleInventoryChange();
    }
    
    private void OnDestroy()
    {
        if (PlayerInventory.Instance != null)
        {
            PlayerInventory.Instance.OnInventoryChanged -= HandleInventoryChange;
        }
    }

    private void HandleInventoryChange()
    {
        var availableIngredients = PlayerInventory.Instance.GetIngredientList();
        var availableRecipes = RecipeManager.Instance.FindAvailableRecipes(availableIngredients);
        PopulateList(availableRecipes);
    }
    
    // You could call this method to update the list, for example, with filtered results.
    public void PopulateList(List<Recipe> recipes)
    {
        // Clear previous items
        foreach (Transform child in _contentParent)
        {
            Destroy(child.gameObject);
        }

        // Create new items
        if (_recipeItemPrefab == null)
        {
            Debug.LogError("RecipeItemPrefab is not set in the inspector!");
            return;
        }

        foreach (var recipe in recipes)
        {
            var itemGO = Instantiate(_recipeItemPrefab, _contentParent);
            itemGO.GetComponent<RecipeListItemUI>().Setup(recipe);
        }
    }
} 