using UnityEngine;

public class SongItem : Item
{
    [SerializeField] private AudioClip _song;

    public override string GetPopUpTitle()
    {
        return "Buy In Game Song";
    }

    public override string GetPopUpDescription()
    {
        return $"Spend ${Price} to listen to {_song.name} song in game?";
    }

    protected override void PurchaseAction()
    {
        IngameMusicManager.Instance.SelectSong(_song.name);
    }
}
