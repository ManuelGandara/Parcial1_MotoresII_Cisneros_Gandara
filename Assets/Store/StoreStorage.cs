using UnityEngine;

public class StoreStorage : MonoBehaviour
{
    [Header("Storage Settings")]
    [SerializeField] private string _fileName = "Store";
    [SerializeField] private bool _hardDelete = false;
    JSONStorage<StoreStatus> _storage;

    [Header("Default Values")]
    [SerializeField] private int _defaultCurrency = 100;
    [SerializeField] private string[] _defaultUnitaryItemsBought = new string[] { };
    StoreStatus _defaultData;

    public static StoreStorage Instance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;

            _defaultData = new StoreStatus()
            {
                Currency = _defaultCurrency,
                UnitaryItemsBought = _defaultUnitaryItemsBought
            };

            _storage = new JSONStorage<StoreStatus>(_fileName, _defaultData);

            DontDestroyOnLoad(this);
        }
    }

    void Start()
    {
        StoreManager.Instance.OnStoreStatusUpdate += PersistCurrency;
    }

    public StoreStatus LoadStoreStatus()
    {
        return _storage.Load();
    }

    public void PersistCurrency(StoreStatus storeStatus)
    {
        _storage.Persist(storeStatus);
    }

    public void DeletedPersistedStoreStatus()
    {
        _storage.Delete(_hardDelete);
    }
}