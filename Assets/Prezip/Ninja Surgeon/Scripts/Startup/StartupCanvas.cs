using UnityEngine;
using UnityEngine.UI;

public class StartupCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _syncComponent;
    [SerializeField] private GameObject _serverStatus;
    [SerializeField] private GameObject _serverOutPanel;
    [SerializeField] private Button _startButton;
    [SerializeField] private Slider _progressBar;
    [SerializeField] private GameObject _loadingPanel;

    MenuLoader _menuLoader;

    void Awake()
    {
        _menuLoader = GetComponent<MenuLoader>();
    }

    void Start()
    {
        RemoteConfigManager.Instance.OnRemoteConfigLoad += DisplayRemoteConfigValues;

        _menuLoader.OnLoadStart += DisplayLoadingComponents;

        _menuLoader.OnProgressUpdate += UpdateProgressBar;

        _startButton.onClick.AddListener(_menuLoader.LoadMenu);

        _syncComponent.SetActive(true);

        _serverOutPanel.SetActive(false);

        _startButton.gameObject.SetActive(false);

        _loadingPanel.SetActive(false);
    }

    void OnDestroy()
    {
        RemoteConfigManager.Instance.OnRemoteConfigLoad -= DisplayRemoteConfigValues;
    }

    void DisplayRemoteConfigValues()
    {
        _syncComponent.SetActive(false);

        if (RemoteConfigManager.Instance.RemoteConfigValues.ServerIsOut)
        {
            _serverOutPanel.SetActive(true);
        }
        else
        {
            _serverStatus.SetActive(true);

            _startButton.gameObject.SetActive(true);
        }
    }

    void DisplayLoadingComponents()
    {
        _progressBar.gameObject.SetActive(true);

        _startButton.gameObject.SetActive(false);

        _loadingPanel.SetActive(true);
    }

    void UpdateProgressBar(float progress)
    {
        _progressBar.value = progress + 0.1f;
    }
}
