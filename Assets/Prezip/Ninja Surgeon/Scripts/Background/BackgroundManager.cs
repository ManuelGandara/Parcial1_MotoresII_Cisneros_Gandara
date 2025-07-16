using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private List<Sprite> _possibleBackgrounds;

    public static BackgroundManager Instance;

    public Sprite SelectedBG { get { return _storage.SelectedItem; } }

    public List<Sprite> AvailableBGs { get { return _storage.AvailableItems; } }

    ItemStorage<Sprite> _storage;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;

            _storage = new ItemStorage<Sprite>
            (
                "Backgrounds",
                new ObtainedItems(_possibleBackgrounds.First().name),
                (bgName) => _possibleBackgrounds.Find(bg => bg.name == bgName)
            );

            DontDestroyOnLoad(this);
        }
    }

    public bool HasBG(Sprite bg)
    {
        return _storage.AvailableItems.Any(availableBg => availableBg.name == bg.name);
    }

    public bool DoesNotHaveBG(Sprite bg)
    {
        return _storage.AvailableItems.All(availableBg => availableBg.name != bg.name);
    }

    public void ObtainBG(Sprite bg)
    {
        _storage.ObtainItem(bg, bg.name);
    }

    internal void RestoreObtainedBGs()
    {
        _storage.ResetItems();
    }
}
