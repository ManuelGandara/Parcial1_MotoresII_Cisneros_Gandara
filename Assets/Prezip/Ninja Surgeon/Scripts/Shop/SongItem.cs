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

    public override bool WasSold()
    {
        return IngameMusicManager.Instance.HasSong(_song);
    }

    protected override bool SatisfiesAdditionalBuyConditions()
    {
        return IngameMusicManager.Instance.DoesNotHaveSong(_song);
    }

    protected override void PurchaseAction()
    {
        IngameMusicManager.Instance.ObtainSong(_song);
    }
}
