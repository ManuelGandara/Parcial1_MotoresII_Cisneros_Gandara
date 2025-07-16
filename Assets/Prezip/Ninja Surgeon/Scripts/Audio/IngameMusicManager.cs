using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IngameMusicManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _possibleSongs;

    public static IngameMusicManager Instance;

    public AudioClip SelectedSong { get { return _storage.SelectedItem; } }

    public List<AudioClip> AvailableSongs { get { return _storage.AvailableItems; } }

    ItemStorage<AudioClip> _storage;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;

            _storage = new ItemStorage<AudioClip>
            (
                "IngameMusic",
                new ObtainedItems(_possibleSongs.First().name),
                (songName) => _possibleSongs.Find(song => song.name == songName)
            );

            DontDestroyOnLoad(this);
        }
    }

    public bool HasSong(AudioClip song)
    {
        return _storage.AvailableItems.Any(availableSong => availableSong.name == song.name);
    }

    public bool DoesNotHaveSong(AudioClip song)
    {
        return _storage.AvailableItems.All(availableSong => availableSong.name != song.name);
    }

    public void ObtainSong(AudioClip song)
    {
        _storage.ObtainItem(song, song.name);
    }

    internal void RestoreObtainedSongs()
    {
        _storage.ResetItems();
    }
}
