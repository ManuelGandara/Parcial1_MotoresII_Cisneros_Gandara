using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Slider _progressBar;

    PlaySceneLoader _playSceneLoader;

    void Start()
    {
        _playSceneLoader = GetComponent<PlaySceneLoader>();

        _playSceneLoader.OnStaminaCheck += EnablePlayButton;

        _playSceneLoader.OnLoadStart += DisplayLoadingScreen;

        _playSceneLoader.OnProgressUpdate += UpdateProgressBar;

        _playButton.onClick.AddListener(_playSceneLoader.Play);

        _playButton.enabled = _playSceneLoader.CanPlay;

        _loadingScreen.SetActive(false);
    }

    void EnablePlayButton(bool isEnabled)
    {
        _playButton.enabled = isEnabled;
    }

    void DisplayLoadingScreen()
    {
        _loadingScreen.SetActive(true);
    }

    void UpdateProgressBar(float progress)
    {
        _progressBar.value = progress + 0.1f;
    }
}
