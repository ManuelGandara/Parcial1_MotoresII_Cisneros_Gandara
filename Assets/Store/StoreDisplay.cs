using TMPro;
using UnityEngine;

public class StoreDisplay : MonoBehaviour
{
    [Header("Store Text")]
    [SerializeField] private TextMeshProUGUI _currencyText;
    [SerializeField] private TextMeshProUGUI _itemsBoughtText;

    void Awake()
    {
        StoreManager.Instance.OnStoreStatusUpdate += OnStoreStatusUpdate;
    }

    private void OnStoreStatusUpdate(StoreStatus storeStatus)
    {
        string itemsBoughtText = storeStatus.UnitaryItemsBought.Length > 0 ? string.Join(",", storeStatus.UnitaryItemsBought) : "N/A";

        _currencyText.text = $"Currency: ${storeStatus.Currency}";
        
        _itemsBoughtText.text = $"Items Bought: {itemsBoughtText}";
    }
}
