using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [Header("Sliders")]
    [SerializeField] private Slider _masterSlider;

    [Header("Initial Volumes")]
    [Range(0f, 1f)][SerializeField] private float _initMasterVol = 0.5f;

    private void Start()
    {
        _masterSlider.value = _initMasterVol;

        SetMasterVolume(_initMasterVol);
    }

    public void SetMasterVolume(float value)
    {
        MusicManager.Instance.SetVolume(MixerGroup.MasterVolume, value);
    }
}