using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerT : MonoBehaviour
{
    [SerializeField] public GameObject Message1;
    [SerializeField] public GameObject Message2;
    [SerializeField] public GameObject Eye;

    public void ShowMessage1()
    {
        Time.timeScale = 0f;
        Message1.SetActive(true);
    }

    public void ShowMessage2()
    {
        Time.timeScale = 0f;
        Message2.SetActive(true);
    }
}
