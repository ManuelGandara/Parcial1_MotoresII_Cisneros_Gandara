using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayGameButton : MonoBehaviour
{
    [SerializeField] private int _playCost = 10;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Play);
    }

    void Play()
    {
        SceneManager.LoadScene("Play");

        StaminaManager.Instance.ConsumeStamina(_playCost);
    }

}
