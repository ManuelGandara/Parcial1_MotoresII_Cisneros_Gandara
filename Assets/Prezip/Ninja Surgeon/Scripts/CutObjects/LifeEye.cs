using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeEye : MonoBehaviour
{
    void Awake()
    {
        GameManager.instance.UpdateLife(RemoteConfigManager.Instance.RemoteConfigValues.LivesIncrease);
    }
}
