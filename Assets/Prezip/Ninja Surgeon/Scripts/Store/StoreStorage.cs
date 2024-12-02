using UnityEngine;

public class StoreStorage : MonoBehaviour
{
    [Header("Storage Settings")]
    [SerializeField] private string _fileName = "Store";
    [SerializeField] private bool _useSoftDelete = true;
    JSONStorage<StoreStatus> _storage;
    StoreStatus _defaultStatus;

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

            _storage = new JSONStorage<StoreStatus>(_fileName);

            DontDestroyOnLoad(this);
        }
    }

    void Start()
    {
        StoreManager.Instance.OnStoreStatusUpdate += PersistStoreStatus;
    }

    public StoreStatus LoadStoreStatus(StoreStatus defaultStatus)
    {
        _defaultStatus = defaultStatus;

        return _storage.Load(defaultStatus);
    }

    public void PersistStoreStatus(StoreStatus storeStatus)
    {
        _storage.Persist(storeStatus);
    }

    public void DeletedPersistedStoreStatus()
    {
        if (_useSoftDelete && _defaultStatus != null)
        {
            _storage.Persist(_defaultStatus);
        }
        else
        {
            _storage.Delete();
        }
    }
}