using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonLogic : MonoBehaviour
{
    [SerializeField] private int _playCost = 10;

    public void Play()
    {
        SceneManager.LoadScene("Play");

        StaminaManager.Instance.ConsumeStamina(_playCost);
    }

}
