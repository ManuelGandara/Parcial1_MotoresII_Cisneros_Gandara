using UnityEngine;
using UnityEngine.UI;

public class SoundVolumeSlider : MonoBehaviour
{
    [SerializeField] private string _audioMixerGroupName;

    void Start()
    {
        Slider _volumeSlider = GetComponent<Slider>();

        _volumeSlider.value = SoundManager.Instance.GetAudioMixerVolume(_audioMixerGroupName);

        _volumeSlider.onValueChanged.AddListener(UpdateMixerVolume);
    }

    void UpdateMixerVolume(float volume)
    {
        SoundManager.Instance.UpdateMixerVolume(_audioMixerGroupName, volume);
    }
}
