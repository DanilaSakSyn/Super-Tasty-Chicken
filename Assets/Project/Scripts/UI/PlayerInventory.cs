using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }
    
    public event Action OnInventoryChanged;

    public List<PlayerIngredient> PlayerIngredients { get; } = new List<PlayerIngredient>();

    private const string InventorySaveKey = "PlayerInventory";

    // Serializable helper classes for saving/loading
    [System.Serializable]
    private class SavedIngredient
    {
        public string Name;
        public int Count;
    }

    [System.Serializable]
    private class SavedInventoryData
    {
        public List<SavedIngredient> SavedIngredients = new List<SavedIngredient>();
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        LoadInventory();
    }

    public void AddIngredient(Ingredient ingredient, int amount = 1)
    {
        if (ingredient == null || amount <= 0) return;

        PlayerIngredient existing = PlayerIngredients.FirstOrDefault(i => i.Ingredient.Equals(ingredient));
        if (existing != null)
        {
            existing.Count += amount;
        }
        else
        {
            PlayerIngredients.Add(new PlayerIngredient { Ingredient = ingredient, Count = amount });
        }
        
        OnInventoryChanged?.Invoke();
        SaveInventory();
    }

    public void RemoveIngredient(Ingredient ingredient)
    {
        if (ingredient == null) return;
        
        PlayerIngredient existing = PlayerIngredients.FirstOrDefault(i => i.Ingredient.Equals(ingredient));
        if (existing != null)
        {
            PlayerIngredients.Remove(existing);
            OnInventoryChanged?.Invoke();
            SaveInventory();
        }
    }
    
    private void SaveInventory()
    {
        SavedInventoryData dataToSave = new SavedInventoryData();
        foreach (var playerIngredient in PlayerIngredients)
        {
            dataToSave.SavedIngredients.Add(new SavedIngredient { Name = playerIngredient.Ingredient.Name, Count = playerIngredient.Count });
        }

        string json = JsonConvert.SerializeObject(dataToSave);
        PlayerPrefs.SetString(InventorySaveKey, json);
        PlayerPrefs.Save();
        Debug.Log("Player inventory saved.");
    }

    private void LoadInventory()
    {
        if (PlayerPrefs.HasKey(InventorySaveKey))
        {
            string json = PlayerPrefs.GetString(InventorySaveKey);
            SavedInventoryData savedData = JsonConvert.DeserializeObject<SavedInventoryData>(json);

            var ingredientDb = new IngredientDatabase();
            PlayerIngredients.Clear();

            foreach (var savedIngredient in savedData.SavedIngredients)
            {
                Ingredient ingredient = ingredientDb.FindIngredientByName(savedIngredient.Name);
                if (ingredient != null)
                {
                    PlayerIngredients.Add(new PlayerIngredient { Ingredient = ingredient, Count = savedIngredient.Count });
                }
            }
            Debug.Log("Player inventory loaded.");
        }
    }

    public List<Ingredient> GetIngredientList()
    {
        return PlayerIngredients.Select(pi => pi.Ingredient).ToList();
    }
} 