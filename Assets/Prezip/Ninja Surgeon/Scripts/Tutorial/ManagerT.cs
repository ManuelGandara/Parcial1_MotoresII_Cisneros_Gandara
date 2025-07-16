using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerT : MonoBehaviour
{
    [SerializeField] public GameObject Message1;
    [SerializeField] public GameObject Message2;
    [SerializeField] public GameObject Eye;
    //[SerializeField] public GameObject GotIt1;
    //[SerializeField] public GameObject GotIt2;
    [SerializeField] public GameObject Spawner1;
    [SerializeField] public GameObject Spawner2;
    private AnimatorClipInfo Tlife;

    private void Start()
    {
        Time.timeScale = 0;
    }
    /*public void ShowMessage1()
    {
        Time.timeScale = 0f;
        Message1.SetActive(true);
    }*/
    private void Update()
    {
    }
    public void ShowMessage2()
    {
        Time.timeScale = 0f;
        Message2.SetActive(true);
    }

    public void CheckGotIt1()
    {
        Time.timeScale = 1f;
        Message1.SetActive(false);
        Spawner1.SetActive(true);
        StartCoroutine(WaitAndShowMessage2());
    }

    public void CheckGotit2()
    {
        Time.timeScale = 1f;
        Message2.SetActive(false);
        Spawner2.SetActive(true);
        StartCoroutine(WaitAndShowVictory());
    }

    private IEnumerator WaitAndShowMessage2()
    {
        yield return new WaitForSeconds(5f);
        ShowMessage2();
    }

    private IEnumerator WaitAndShowVictory()
    {
        yield return new WaitForSeconds(5f);
        GameManager.instance.Victory();
    }
}
