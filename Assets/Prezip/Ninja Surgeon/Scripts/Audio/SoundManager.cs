using System;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(SoundStorage))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private float _pauseVolumeDx = 5f;

    public static SoundManager Instance;

    public event Action<string, float> OnVolumeChange = delegate { };

    string[] _mixerIds = { "Master", "Music", "SFX" };
    SoundStorage _soundStorage;

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
        _soundStorage = GetComponent<SoundStorage>();

        foreach (string mixerId in _mixerIds)
        {
            UpdateMixerVolume(mixerId, _soundStorage.RetrieveMixerVolume(mixerId));
        }

        OnVolumeChange += GetComponent<SoundStorage>().PersistMixerVolume;
    }

    public void UpdateMixerVolume(string mixerGroup, float volumePercentage)
    {
        _audioMixer.SetFloat(GetVolumeExposedVariable(mixerGroup), 20 * Mathf.Log10(Mathf.Clamp(volumePercentage, 0.0001f, 1)));

        OnVolumeChange(mixerGroup, volumePercentage);
    }

    public float GetAudioMixerVolume(string mixerGroup)
    {
        _audioMixer.GetFloat(GetVolumeExposedVariable(mixerGroup), out float volumeDb);

        return Mathf.Pow(10, volumeDb / 20);
    }

    public void RestoreMixerVolumes()
    {
        foreach (string mixerId in _mixerIds)
        {
            _soundStorage.DeleteMixerVolumes(mixerId);

            UpdateMixerVolume(mixerId, _soundStorage.RetrieveMixerVolume(mixerId));
        }
    }

    public void TemporarilyIncreaseVolume()
    {
        TemporarilyChangeVolume(_pauseVolumeDx);
    }

    public void TemporarilyDecreaseVolume()
    {
        TemporarilyChangeVolume(-_pauseVolumeDx);
    }

    void TemporarilyChangeVolume(float difference)
    {
        if (_audioMixer.GetFloat("MasterVolume", out float currentVolume))
        {
            _audioMixer.SetFloat("MasterVolume", currentVolume + difference);
        }
    }

    string GetVolumeExposedVariable(string mixerGroup)
    {
        return mixerGroup + "Volume";
    }
}
