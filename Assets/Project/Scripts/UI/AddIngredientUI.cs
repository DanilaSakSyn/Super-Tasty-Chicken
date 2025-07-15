using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AddIngredientUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Button _addButton;
    [SerializeField] private Button _closeButton;

    [Header("Selected Ingredient Display")]
    [SerializeField] private Image _selectedIngredientIcon;
    [SerializeField] private TextMeshProUGUI _selectedIngredientName;
    
    [Header("Quantity Stepper")]
    [SerializeField] private Button _incrementButton;
    [SerializeField] private Button _decrementButton;
    [SerializeField] private TextMeshProUGUI _quantityText;

    [Header("Ingredient Grid")]
    [SerializeField] private GameObject _pickerItemPrefab;
    [SerializeField] private Transform _gridContentParent;

    private Ingredient _currentlySelected;
    private int _currentQuantity;

    private void Awake()
    {
        _addButton.onClick.AddListener(OnAddClicked);
        _closeButton.onClick.AddListener(Hide); // Back to direct call
        _incrementButton.onClick.AddListener(IncrementQuantity);
        _decrementButton.onClick.AddListener(DecrementQuantity);

        PopulateIngredientGrid();
        
        // We manage our own visibility again
        gameObject.SetActive(false); 
    }
    
    private void PopulateIngredientGrid()
    {
        var ingredientDb = new IngredientDatabase();
        var allIngredients = ingredientDb.GetAllIngredients();

        foreach (var ingredient in allIngredients)
        {
            var itemGO = Instantiate(_pickerItemPrefab, _gridContentParent);
            itemGO.GetComponent<IngredientPickerItemUI>().Setup(ingredient, HandleIngredientSelected);
        }
    }

    public void Show() // This method is no longer strictly needed but can be kept for clarity
    {
        gameObject.SetActive(true);
        UpdateSelectionDisplay(null);
        ResetQuantity();
    }

    public void Hide() // This method is no longer strictly needed
    {
        gameObject.SetActive(false);
    }
    
    private void HandleIngredientSelected(Ingredient selectedIngredient)
    {
        UpdateSelectionDisplay(selectedIngredient);
    }

    private void UpdateSelectionDisplay(Ingredient ingredient)
    {
        _currentlySelected = ingredient;
        if (_currentlySelected != null)
        {
            _selectedIngredientIcon.sprite = _currentlySelected.Icon;
            _selectedIngredientIcon.enabled = true;
            _selectedIngredientName.text = _currentlySelected.Name;
        }
        else
        {
            _selectedIngredientIcon.enabled = false;
            _selectedIngredientName.text = "Выберите ингредиент...";
        }
    }
    
    private void IncrementQuantity()
    {
        _currentQuantity++;
        UpdateQuantityDisplay();
    }

    private void DecrementQuantity()
    {
        if (_currentQuantity > 1)
        {
            _currentQuantity--;
            UpdateQuantityDisplay();
        }
    }

    private void ResetQuantity()
    {
        _currentQuantity = 1;
        UpdateQuantityDisplay();
    }
    
    private void UpdateQuantityDisplay()
    {
        _quantityText.text = _currentQuantity.ToString();
    }

    private void OnAddClicked()
    {
        if (_currentlySelected == null)
        {
            Debug.LogWarning("Please select an ingredient first.");
            return;
        }
        
        PlayerInventory.Instance.AddIngredient(_currentlySelected, _currentQuantity);
        Hide(); // Back to direct call
    }
} 