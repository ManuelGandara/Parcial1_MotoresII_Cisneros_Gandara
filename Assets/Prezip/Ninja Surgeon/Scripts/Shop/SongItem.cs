using UnityEngine;

public class SongItem : Item
{
    [SerializeField] private AudioClip _song;

    protected override void PurchaseAction()
    {
        IngameMusicManager.Instance.SelectSong(_song.name);
    }
}
