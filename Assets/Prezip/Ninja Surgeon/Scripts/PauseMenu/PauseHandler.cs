using UnityEngine;
using UnityEngine.UI;

public class PauseHandler : MonoBehaviour
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private GameObject _pauseMenu;

    bool _paused = false;

    void Start()
    {
        _pauseButton.onClick.AddListener(Pause);

        _closeButton.onClick.AddListener(Resume);

        _pauseMenu.SetActive(false);
    }

    void OnDestroy()
    {
        if (_paused)
        {
            SoundManager.Instance.TemporarilyIncreaseVolume();
        }
    }

    public void Pause()
    {
        _paused = true;

        Time.timeScale = 0;

        SoundManager.Instance.TemporarilyDecreaseVolume();

        _pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        _paused = false;

        Time.timeScale = 1;

        SoundManager.Instance.TemporarilyIncreaseVolume();

        _pauseMenu.SetActive(false);
    }
}
