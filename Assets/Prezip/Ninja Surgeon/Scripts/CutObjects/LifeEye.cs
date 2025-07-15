using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeEye : MonoBehaviour
{
    public int lifePlus = 1;
    void Awake()
    {
        GameManager.instance.UpdateLife(lifePlus);
    }
}
