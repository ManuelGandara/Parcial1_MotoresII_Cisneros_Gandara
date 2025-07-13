using UnityEngine;
using UnityEngine.UI;

public class CurrencyDisplay : MonoBehaviour
{
    [SerializeField] private Text _currencyText;

    void Start()
    {
        StoreManager.Instance.OnStoreStatusUpdate += DisplayNextCurrency;

        _currencyText.text = "$ " + StoreManager.Instance.Currency;
    }

    void OnDestroy()
    {
        StoreManager.Instance.OnStoreStatusUpdate -= DisplayNextCurrency;
    }

    void DisplayNextCurrency(StoreStatus storeStatus)
    {
        _currencyText.text = "$ " + storeStatus.Currency;
    }
}
