using System.Linq;
using UnityEngine;

public class IngameMusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] _availableSongs;
    [SerializeField] private AudioClip _selectedSong;

    public static IngameMusicManager Instance;

    public AudioClip SelectedSong { get { return _selectedSong; } }

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

    public void SelectSong(string songName)
    {
        _selectedSong = _availableSongs.Where(clip => clip.name == songName).FirstOrDefault();
    }
}
