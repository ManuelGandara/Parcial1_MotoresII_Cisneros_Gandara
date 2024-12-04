using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;

    [Header("Initial Volumes")]
    [Range(0f, 1f)][SerializeField] private float _initMasterVol = 0.5f;
    [Range(0f, 1f)][SerializeField] private float _initMusicVol = 1f;
    [Range(0f, 1f)][SerializeField] private float _initSFXVol = 1f;

    private void Start()
    {
        _masterSlider.value = _initMasterVol;

        SetMasterVolume(_initMasterVol);

        _musicSlider.value = _initMusicVol;

        SetMusicVolume(_initMusicVol);

        _sfxSlider.value = _initSFXVol;

        SetSFXVolume(_initSFXVol);
    }

    public void SetMasterVolume(float value)
    {
        MusicManager.Instance.SetVolume(MixerGroup.MasterVolume, value);
    }

    public void SetMusicVolume(float value)
    {
        MusicManager.Instance.SetVolume(MixerGroup.MusicVolume, value);
    }

    public void SetSFXVolume(float value)
    {
        MusicManager.Instance.SetVolume(MixerGroup.SFXVolume, value);
    }
}