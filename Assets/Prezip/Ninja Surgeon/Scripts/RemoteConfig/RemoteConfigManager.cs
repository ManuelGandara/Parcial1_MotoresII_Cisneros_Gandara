using System;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;
using UnityEngine;

public class RemoteConfigManager : MonoBehaviour, IRequiredForStartup
{
    [Header("Remote Config Keys")]
    [SerializeField] private string _serviceOutKey = "Bool_ServiceOut";
    [SerializeField] private string _infoServiceKey = "String_InfoService";
    [SerializeField] private string _versionKey = "Int_Version";
    [SerializeField] private string _patchKey = "Float_Parche";
    [SerializeField] private string _versionTextKey = "String_Version";
    [SerializeField] private string _storeDiscountKey = "Float_Store_Discount";
    [SerializeField] private string _menuSongKey = "String_Menu_Song";
    [SerializeField] private string _initialLivesKey = "Int_Initial_Lives";
    [SerializeField] private string _evilEyesLivesDecrease = "Int_Evil_Eyes_Lives_Decrease";
    [SerializeField] private string _evilEyesProbability = "Float_Evil_Eye_Probability";
    [SerializeField] private string _freezeDurationKey = "Float_Freeze_Duration";

    public static RemoteConfigManager Instance;

    public struct UserAttributes { }

    public struct AppAttributes { }

    bool _isReady = false;

    public RemoteConfigValues RemoteConfigValues;

    public event Action OnRemoteConfigLoad = delegate { };

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
        StartProcess();
    }

    public bool IsReady()
    {
        return _isReady;
    }

    async void StartProcess()
    {
        if (Utilities.CheckForInternetConnection())
        {
            await UnityServices.InitializeAsync();

            if (AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }
        }

        RemoteConfigService.Instance.FetchCompleted += FetchCompleted;

        RemoteConfigService.Instance.FetchConfigs(new UserAttributes(), new AppAttributes());
    }

    void FetchCompleted(ConfigResponse response)
    {
        var remoteConfig = RemoteConfigService.Instance.appConfig.config;

        RemoteConfigValues = new RemoteConfigValues
        {
            ServerIsOut = remoteConfig.Value<bool>(_serviceOutKey),
            ServerInfo = remoteConfig.Value<string>(_infoServiceKey),
            Version = remoteConfig.Value<int>(_versionKey),
            Patch = remoteConfig.Value<float>(_patchKey),
            VersionText = remoteConfig.Value<string>(_versionTextKey),
            StoreDiscount = Mathf.Max(0, remoteConfig.Value<float>(_storeDiscountKey)),
            MenuSong = remoteConfig.Value<string>(_menuSongKey),
            InitialLives = Mathf.Max(1, remoteConfig.Value<int>(_initialLivesKey)),
            FreezeDuration = Mathf.Max(1, remoteConfig.Value<float>(_freezeDurationKey)),
            EvilEyesLivesDecrease = Mathf.Max(0, remoteConfig.Value<int>(_evilEyesLivesDecrease)),
            EvilEyesProbability = Mathf.Clamp01(remoteConfig.Value<float>(_evilEyesProbability))
        };

        _isReady = true;

        OnRemoteConfigLoad();
    }
}
