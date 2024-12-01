using System.Linq;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    private StoreStatus _storeStatus;

    public delegate void StoreStatusUpdate(StoreStatus storeStatus);

    public event StoreStatusUpdate OnStoreStatusUpdate;

    public static StoreManager Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;

            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        _storeStatus = StoreStorage.Instance.LoadStoreStatus();
    }

    public int Currency { get { return _storeStatus.Currency; } }

    public void ObtainCurrency(int amount)
    {
        UpdateCurrency(amount);
    }

    public void SpendCurrency(int amount)
    {
        UpdateCurrency(-amount);
    }

    public bool DidSell(string storeItemId)
    {
        return _storeStatus.UnitaryItemsBought.Any(itemId => itemId == storeItemId);
    }

    private void UpdateCurrency(int amount)
    {
        _storeStatus.Currency = Mathf.Max(_storeStatus.Currency + amount, 0);

        OnStoreStatusUpdate(_storeStatus);
    }
}
