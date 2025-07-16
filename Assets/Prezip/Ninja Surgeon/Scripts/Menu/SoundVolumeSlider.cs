using UnityEngine;
using UnityEngine.UI;

public class SoundVolumeSlider : MonoBehaviour
{
    [SerializeField] private string _audioMixerGroupName;

    Slider _volumeSlider;

    void Awake()
    {
        _volumeSlider = GetComponent<Slider>();
    }

    void Start()
    {
        _volumeSlider.value = SoundManager.Instance.GetAudioMixerVolume(_audioMixerGroupName);

        _volumeSlider.onValueChanged.AddListener(UpdateMixerVolume);

        SyncVolume();
    }

    void OnEnable()
    {
        SyncVolume();
    }

    void SyncVolume()
    {
        _volumeSlider.value = SoundManager.Instance.GetAudioMixerVolume(_audioMixerGroupName);
    }

    void UpdateMixerVolume(float volume)
    {
        SoundManager.Instance.UpdateMixerVolume(_audioMixerGroupName, volume);
    }
}
