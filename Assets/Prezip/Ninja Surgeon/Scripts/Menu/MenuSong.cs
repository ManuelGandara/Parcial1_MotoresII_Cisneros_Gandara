using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MenuSong : MonoBehaviour
{
    [SerializeField] private AudioClip[] _availableSongs;

    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();

        audioSource.clip = _availableSongs.Where(song => song.name == RemoteConfigManager.Instance.RemoteConfigValues.MenuSong).FirstOrDefault();

        audioSource.Play();

        audioSource.loop = true;
    }
}
