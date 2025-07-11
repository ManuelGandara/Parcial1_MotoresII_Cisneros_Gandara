using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuLoader : MonoBehaviour
{
    [Header("Linked Scenes")]
    [SerializeField] private string _mainMenuScene = "Menu";

    [Header("UI")]
    [SerializeField] private GameObject _loadingPanel;
    [SerializeField] private Button _startButton;

    void Start()
    {
        _startButton.onClick.AddListener(LoadMenu);
    }

    void LoadMenu()
    {
        _loadingPanel.SetActive(true);

        _startButton.gameObject.SetActive(false);

        StartCoroutine(LoadMenuAsync());
    }

    IEnumerator LoadMenuAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_mainMenuScene, LoadSceneMode.Single);

        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
