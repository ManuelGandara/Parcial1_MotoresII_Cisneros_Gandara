using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    List<StoreItem> _storeItems;
    List<UnitaryStoreItem> _unitaryStoreItems;
    List<StoreItem> _rewardStoreItems;
    StoreStatus _storeStatus;

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

        ConfigureRewards();

        InitializeStoreItems();
    }

    public StoreStatus StoreStatus { get { return _storeStatus; } }

    public List<StoreItem> StoreItems { get { return _storeItems; } }

    public int Currency { get { return _storeStatus.Currency; } }

    public void ObtainCurrency(int amount)
    {
        if (GameManager.instance.currencyBool == true)
        {
            UpdateCurrency(amount);
            GameManager.instance.currencyBool = false;
        }
        
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
        storeItem.Obtain();

        SpendCurrency(storeItem.Cost);
    }

    public void PurchaseUnitaryItem(UnitaryStoreItem unitaryStoreItem)
    {
        _storeStatus.UnitaryItemsBought.Add(unitaryStoreItem.Id);

        PurchaseItem(unitaryStoreItem);
    }

    public void ResetStore()
    {
        StoreStorage.Instance.DeletedPersistedStoreStatus();

        InitializeStoreItems();
    }

    private void UpdateCurrency(int amount)
    {
        _storeStatus.Currency = Mathf.Max(_storeStatus.Currency + amount, 0);

        OnStoreStatusUpdate(_storeStatus);
    }
    
    private void ObtainReward()
    {

        StoreItem reward = _rewardStoreItems[Random.Range(0, _rewardStoreItems.Count)];

        if (StaminaManager.Instance.CurrentStamina.Amount == 0)
        {
            reward = _rewardStoreItems[0];
        }

        Debug.Log($"Obtuviste {reward.Name}");

        reward.Obtain();

    }

    private void InitializeStoreItems()
    {
        _storeStatus = StoreStorage.Instance.LoadStoreStatus(_defaultStoreStatus);

        _unitaryStoreItems = new ()
            {
                new BladeColorItem("green_blade_color", "Green Blade Color", "<==|-", 20, Color.green, Color.green),
                new BladeColorItem("red_blade_color", "Red Blade Color", "<==|-", 20, Color.red, Color.red),
                new EyeSkinItem("yellow_eye_skin", "Yellow Eye Skin", "�_�", 30, Color.yellow, Color.yellow),
                new EyeSkinItem("cyan_eye_skin", "Cyan Eye Skin", "�_�", 30, Color.cyan, Color.cyan)
            };

        foreach (UnitaryStoreItem unitaryStoreItem in _unitaryStoreItems)
        {
            unitaryStoreItem.TryObtain();
        }

        List<StoreItem> storeItems = new()
            {
                new StaminaItem("10 Stamina", "10 St", 20, Color.blue, 10),
                new StaminaItem("50 Stamina", "50 St", 100, Color.blue, 50),
                new StaminaItem("100 Stamina", "100 St", 300, Color.blue, 100),
            };

        _storeItems = storeItems.Concat(_unitaryStoreItems).ToList();

        OnStoreStatusUpdate(_storeStatus);
    }

    private void ConfigureRewards()
    {
        _rewardStoreItems = new()
            {
                new StaminaItem("10 Stamina", "10 St", 20, Color.blue, 10),
                new CurrencyItem("10 Coins", "$10", 0, Color.white, 10),
            };

        AdsManager.Instance.OnGrantReward += ObtainReward;
    }
}
