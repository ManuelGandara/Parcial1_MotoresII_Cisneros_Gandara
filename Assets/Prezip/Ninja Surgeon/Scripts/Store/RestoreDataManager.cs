using System;
using UnityEngine;

public class RestoreDataManager : MonoBehaviour
{
    public static RestoreDataManager Instance;

    public event Action OnRestore = delegate { };

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

    public void RestoreData()
    {
        StaminaManager.Instance.ResetStamina();

        StoreManager.Instance.ResetStore();

        SoundManager.Instance.RestoreMixerVolumes();

        IngameMusicManager.Instance.RestoreObtainedSongs();

        BackgroundManager.Instance.RestoreObtainedBGs();

        OnRestore();
    }
}
