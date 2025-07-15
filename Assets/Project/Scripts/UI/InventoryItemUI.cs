using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemUI : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _countText;
    [SerializeField] private Button _removeButton;

    private Ingredient _ingredient;

    public void Setup(PlayerIngredient playerIngredient)
    {
        _ingredient = playerIngredient.Ingredient;
        
        _iconImage.sprite = _ingredient.Icon;
        _iconImage.enabled = _ingredient.Icon != null;
        _nameText.text = _ingredient.Name;
        _countText.text = $"x{playerIngredient.Count}";
        
        _removeButton.onClick.RemoveAllListeners();
        _removeButton.onClick.AddListener(OnRemoveClicked);
    }

    private void OnRemoveClicked()
    {
        PlayerInventory.Instance.RemoveIngredient(_ingredient);
    }
} 