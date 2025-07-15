using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeDetailsUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image _recipeIcon;
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _instructionsText;
    [SerializeField] private Button _backButton;

    private void Awake()
    {
        // Subscribe to the event BEFORE the object is disabled.
        UIEvents.OnShowRecipeDetailsRequested += Show;

        _backButton.onClick.AddListener(Hide);
        gameObject.SetActive(false); // Start hidden
    }
    
    private void OnDestroy()
    {
        // IMPORTANT: Unsubscribe from static events when the object is destroyed
        // to prevent memory leaks.
        UIEvents.OnShowRecipeDetailsRequested -= Show;
    }

    private void Show(Recipe recipe)
    {
        gameObject.SetActive(true);
        Display(recipe);
    }
    
    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Display(Recipe recipe)
    {
        _recipeIcon.sprite = recipe.Icon;
        _recipeIcon.enabled = recipe.Icon != null;
        _titleText.text = recipe.Name;
        _descriptionText.text = recipe.Description;
        _instructionsText.text = recipe.Instructions;
    }
} 