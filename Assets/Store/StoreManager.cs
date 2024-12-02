using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    private List<StoreItem> _storeItems;
    private StoreStatus _storeStatus;

    [Header("Default Values")]
    [SerializeField] private int _defaultCurrency = 100;
    [SerializeField] private List<UnitaryStoreItem> _defaultUnitaryItemsBought = new();
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
            UnitaryItemsBought = _defaultUnitaryItemsBought.Select(item => item.Id).ToList(),
        };

        _storeStatus = StoreStorage.Instance.LoadStoreStatus(_defaultStoreStatus);

        _storeItems = new List<StoreItem>()
            {
                new CurrencyItem("10 Stamina", "10 St", 20, Color.yellow, 10),
                new CurrencyItem("50 Stamina", "50 St", 100, Color.yellow, 50),
                new CurrencyItem("100 Stamina", "100 St", 250, Color.yellow, 100),
                new BladeColorItem("green_blade_color", "Green Blade Color", "<==|-", 30, Color.green, Color.green),
                new BladeColorItem("red_blade_color", "Red Blade Color", "<==|-", 30, Color.red, Color.red),
                new EyeSkinItem("yellow_eye_skin", "Yellow Eye Skin", "ò_ó", 30, Color.yellow, Color.yellow),
                new EyeSkinItem("cyan_eye_skin", "Cyan Eye Skin", "ò_ó", 30, Color.cyan, Color.cyan)
            };

        OnStoreStatusUpdate(_storeStatus);
    }

    public StoreStatus StoreStatus { get { return _storeStatus; } }

    public List<StoreItem> StoreItems { get { return _storeItems; } }

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

    public void PurchaseItem(StoreItem storeItem)
    {
        SpendCurrency(storeItem.Cost);
    }

    public void PurchaseUnitaryItem(UnitaryStoreItem unitaryStoreItem)
    {
        _storeStatus.UnitaryItemsBought.Add(unitaryStoreItem.Id);

        PurchaseItem(unitaryStoreItem);
    }

    private void UpdateCurrency(int amount)
    {
        _storeStatus.Currency = Mathf.Max(_storeStatus.Currency + amount, 0);

        OnStoreStatusUpdate(_storeStatus);
    }
}
