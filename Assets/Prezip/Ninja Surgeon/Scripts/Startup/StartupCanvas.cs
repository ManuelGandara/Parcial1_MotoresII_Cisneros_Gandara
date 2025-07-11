using UnityEngine;

public class StartupCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _loadingComponent;
    [SerializeField] private GameObject _serverOutPanel;
    [SerializeField] private GameObject _startButton;

    void Start()
    {
        RemoteConfigManager.Instance.OnRemoteConfigLoad += DisplayButtonsOnRemoteConfigLoad;
    }

    void OnDestroy()
    {
        RemoteConfigManager.Instance.OnRemoteConfigLoad -= DisplayButtonsOnRemoteConfigLoad;
    }

    void DisplayButtonsOnRemoteConfigLoad()
    {
        _loadingComponent.SetActive(false);

        if (RemoteConfigManager.Instance.RemoteConfigValues.ServerIsOut)
        {
            _serverOutPanel.SetActive(true);
        }
        else
        {
            _startButton.SetActive(true);
        }
    }
}
