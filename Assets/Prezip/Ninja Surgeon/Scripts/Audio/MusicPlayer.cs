using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    AudioSource _audioSource;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        _audioSource.clip = IngameMusicManager.Instance.SelectedSong;
    }

    void Start()
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.Play();
        }

        _audioSource.loop = true;
    }
}
