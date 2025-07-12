using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsShowListener, IRequiredForStartup
{
    [SerializeField] private string _gameId = "5741969";
    [SerializeField] string _adId = "Ninja_Surgeon_Rewarded_Ad";

    public delegate void GrantReward();

    public event GrantReward OnGrantReward;

    public static AdsManager Instance;

    private void Awake()
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
        Advertisement.Initialize(_gameId, true, this);
    }

    public bool IsReady()
    {
        return Advertisement.isInitialized;
    }

    public void ShowAd()
    {
        if (!Advertisement.isInitialized) return;

        Advertisement.Show(_adId, this);
    }

    public void OnInitializationComplete()
    {
        Advertisement.Load(_adId, new EmptyUnityAdsLoadListener());
    }

    public void OnUnityAdsShowStart(string placementId)
    {

    }

    public void OnUnityAdsShowClick(string placementId)
    {

    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            OnGrantReward();
        }
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        // Hacer algo cuando falle, supongo...
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        // Hacer algo cuando falle, supongo...
    }
}

public class EmptyUnityAdsLoadListener : IUnityAdsLoadListener
{
    public void OnUnityAdsAdLoaded(string placementId)
    {

    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {

    }
}