using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [Header("Song")]
    [SerializeField] private AudioClip _musicClip;

    private void Start()
    {
        MusicManager.Instance.PlayClip(_musicClip);
    }
}
