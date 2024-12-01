using System.Linq;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    [Header("Store Status")]
    [SerializeField] private StoreItem[] _storeItems = new StoreItem[0];
    private StoreStatus _storeStatus;

    [Header("Default Values")]
    [SerializeField] private int _defaultCurrency = 100;
    [SerializeField] private UnitaryStoreItem[] _defaultUnitaryItemsBought = new UnitaryStoreItem[] { };
    StoreStatus _defaultStoreStatus;

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

    void Start()
    {
        _defaultStoreStatus = new StoreStatus()
        {
            Currency = _defaultCurrency,
            UnitaryItemsBought = _defaultUnitaryItemsBought.Select(item => item.Id).ToArray()
        };

        _storeStatus = StoreStorage.Instance.LoadStoreStatus(_defaultStoreStatus);

        OnStoreStatusUpdate(_storeStatus);
    }

    public StoreStatus StoreStatus { get { return _storeStatus; } }

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
