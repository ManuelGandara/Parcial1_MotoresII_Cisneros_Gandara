using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.RemoteConfig;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Threading.Tasks;
using TMPro;

public class GameConfig : MonoBehaviour
{
    public struct UserAttributes { }

    public struct AppAttributes { }

    [SerializeField] GameObject ServerOutPanel;
    [SerializeField] TextMeshProUGUI VersionText;
    [SerializeField] TextMeshProUGUI ParcheText;
    [SerializeField] TextMeshProUGUI VersText;
    [SerializeField] TextMeshProUGUI InfoServer;

    private void Start()
    {
        startProcess();
    }

    async void startProcess()
    {
        if (Utilities.CheckForInternetConnection())
        {
            await initializeRemoteConfig();
        }
        else
        {

        }

        RemoteConfigService.Instance.FetchCompleted += Fetch;

        RemoteConfigService.Instance.FetchConfigs(new UserAttributes(), new AppAttributes());
    }

    async Task initializeRemoteConfig()
    {
        await UnityServices.InitializeAsync();

        if (AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }

    void Fetch(ConfigResponse response)
    {
        ServerOutPanel.SetActive(RemoteConfigService.Instance.appConfig.config.Value<bool>("Bool_ServiceOut"));

        InfoServer.text = RemoteConfigService.Instance.appConfig.config.Value<string>("String_InfoService").ToString();
        VersionText.text = RemoteConfigService.Instance.appConfig.config.Value<int>("Int_Version").ToString();
        ParcheText.text = RemoteConfigService.Instance.appConfig.config.Value<float>("Float_Parche").ToString();
        VersText.text = RemoteConfigService.Instance.appConfig.config.Value<string>("String_Version").ToString();
    }
}
