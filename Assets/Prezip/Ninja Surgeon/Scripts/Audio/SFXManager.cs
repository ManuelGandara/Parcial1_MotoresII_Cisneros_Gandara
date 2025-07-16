using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [Header("Audio")]
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _clips;
    [SerializeField] private float _sfxPitchDiff = 0.1f;

    float _basePitch;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (!_audioSource)
        {
            _audioSource = GetComponent<AudioSource>();
        }

        _basePitch = _audioSource.pitch;
    }

    public void SetVolume(MixerGroup group, float value)
    {
        _audioMixer.SetFloat(group.ToString(), Mathf.Log10(Mathf.Max(0.0001f, value)) * 25);
    }

    public void PlayClip(int index)
    {
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }

        _audioSource.clip = _clips[index];

        _audioSource.pitch = _basePitch + Random.Range(-_sfxPitchDiff, _sfxPitchDiff);

        _audioSource.Play();
    }
}