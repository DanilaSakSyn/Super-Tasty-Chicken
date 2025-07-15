using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject _inventoryItemPrefab;
    [SerializeField] private Transform _contentParent;
    [SerializeField] private UnityEngine.UI.Button _openAddPanelButton;
    [SerializeField] private AddIngredientUI _addIngredientPanel;

    private void Start()
    {
        PlayerInventory.Instance.OnInventoryChanged += RefreshInventoryList;
        _openAddPanelButton.onClick.AddListener(OnOpenAddPanelClicked);
        RefreshInventoryList();
    }

    private void OnDestroy()
    {
        if (PlayerInventory.Instance != null)
        {
            PlayerInventory.Instance.OnInventoryChanged -= RefreshInventoryList;
        }
    }

    private void RefreshInventoryList()
    {
        foreach (Transform child in _contentParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var playerIngredient in PlayerInventory.Instance.PlayerIngredients)
        {
            var itemGO = Instantiate(_inventoryItemPrefab, _contentParent);
            itemGO.GetComponent<InventoryItemUI>().Setup(playerIngredient);
        }
    }

    private void OnOpenAddPanelClicked()
    {
        // UIManager.Instance.ShowAddIngredientPanel();
        _addIngredientPanel.Show();
    }
} 