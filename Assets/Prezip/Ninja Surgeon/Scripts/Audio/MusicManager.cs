using System;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [Header("Audio")]
    [SerializeField] public AudioMixer _audioMixer;
    [SerializeField] public AudioSource _audioSource;
    [SerializeField] public AudioClip _clips;

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
    }

    public void SetVolume(MixerGroup group, float value)
    {
        _audioSource.volume = value;

        _audioMixer.SetFloat(group.ToString(), Mathf.Log10(Mathf.Max(0.0001f, value)) * 25);
    }

    public void PlayClip(AudioClip clip)
    {
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }

        _audioSource.clip = clip;

        _audioSource.Play();
    }
}

