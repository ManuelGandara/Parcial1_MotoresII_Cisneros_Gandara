using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonLogic : MonoBehaviour
{

    public void Play()
    {
        SceneManager.LoadScene("Play");
    }

}
