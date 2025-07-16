using UnityEngine;
using UnityEngine.UI;

public class ServerDataDisplayer : MonoBehaviour
{
    Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    private void OnEnable()
    {
        RemoteConfigValues remoteConfigValues = RemoteConfigManager.Instance.RemoteConfigValues;

        _text.text = $"{remoteConfigValues.VersionText} {remoteConfigValues.Version}.{remoteConfigValues.Patch} - {remoteConfigValues.ServerInfo}";
    }
}
