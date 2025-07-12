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
        };

        _isReady = true;

        OnRemoteConfigLoad();
    }
}
