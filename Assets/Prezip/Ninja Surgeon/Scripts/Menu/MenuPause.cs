using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPausa : MonoBehaviour
{
    public GameObject Pausepanel;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause() 
    {
        Pausepanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        Pausepanel.SetActive(false);
        Time.timeScale = 1;
    }
}
