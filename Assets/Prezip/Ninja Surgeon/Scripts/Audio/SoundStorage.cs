using UnityEngine;

public class SoundStorage : MonoBehaviour
{
    [SerializeField] private float _defaultValue = 1f;

    public void PersistMixerVolume(string mixer, float volume)
    {
        PlayerPrefs.SetFloat(mixer, volume);

        PlayerPrefs.Save();
    }

    public float RetrieveMixerVolume(string mixer)
    {
        if (PlayerPrefs.HasKey(mixer))
        {
            return PlayerPrefs.GetFloat(mixer);
        }
        else
        {
            PersistMixerVolume(mixer, _defaultValue);

            return _defaultValue;
        }
    }

    public void DeleteMixerVolumes(string mixer)
    {
        if (PlayerPrefs.HasKey(mixer))
        {
            PlayerPrefs.DeleteKey(mixer);

            PlayerPrefs.Save();
        }
    }
}
