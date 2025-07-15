using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeListItemUI : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Button _selectButton;

    private Recipe _recipe;

    private void Awake()
    {
        _selectButton.onClick.AddListener(OnSelected);
    }

    public void Setup(Recipe recipe)
    {
        _recipe = recipe;
        if (recipe == null)
        {
            gameObject.SetActive(false);
            return;
        }
        
        _iconImage.sprite = recipe.Icon;
        _iconImage.enabled = recipe.Icon != null; 
        
        _titleText.text = recipe.Name;
        _descriptionText.text = recipe.Description;
    }

    private void OnSelected()
    {
        Debug.Log("Test RecipeListItemUI");
        // UIManager.Instance.ShowRecipeDetailsPanel(_recipe);
        UIEvents.RequestShowRecipeDetails(_recipe);
    }
} 