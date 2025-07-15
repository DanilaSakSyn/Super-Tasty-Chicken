using System;

public static class UIEvents
{
    public static event Action<Recipe> OnShowRecipeDetailsRequested;

    public static void RequestShowRecipeDetails(Recipe recipe)
    {
        OnShowRecipeDetailsRequested?.Invoke(recipe);
    }
} 