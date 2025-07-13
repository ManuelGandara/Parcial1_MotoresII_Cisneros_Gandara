using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;

    public static SoundManager Instance;

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

    public void UpdateMixerVolume(string mixerGroup, float volumePercentage)
    {
        _audioMixer.SetFloat(GetVolumeExposedVariable(mixerGroup), 20 * Mathf.Log10(Mathf.Clamp(volumePercentage, 0.0001f, 1)));
    }

    public float GetAudioMixerVolume(string mixerGroup)
    {
        _audioMixer.GetFloat(GetVolumeExposedVariable(mixerGroup), out float volumeDb);

        return Mathf.Pow(10, volumeDb / 20);
    }

    string GetVolumeExposedVariable(string mixerGroup)
    {
        return mixerGroup + "Volume";
    }
}
