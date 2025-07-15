using System;
using UnityEngine;
using UnityEngine.UI;

public class IngredientPickerItemUI : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private Button _button;

    private Ingredient _ingredient;
    private Action<Ingredient> _onSelected;

    public void Setup(Ingredient ingredient, Action<Ingredient> onSelectedCallback)
    {
        _ingredient = ingredient;
        _onSelected = onSelectedCallback;

        _iconImage.sprite = _ingredient.Icon;
        _iconImage.enabled = _ingredient.Icon != null;
        
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(OnClicked);
    }

    private void OnClicked()
    {
        _onSelected?.Invoke(_ingredient);
    }
} 