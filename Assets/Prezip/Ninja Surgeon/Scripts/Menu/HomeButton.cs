using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeButton : MonoBehaviour
{
    [SerializeField] private string _menuScene = "Menu";
    [SerializeField] private string _popUpTitle;
    [SerializeField] private string _popUpDescription;
    [SerializeField] private GameObject _loadingPanel;

    public event Action OnLoadStart = delegate { };

    public event Action<float> OnProgressUpdate = delegate { };

    Button _homeButton;

    private void Awake()
    {
        _homeButton = GetComponent<Button>();
    }

    void Start()
    {
        _homeButton.onClick.AddListener(LoadPopUp);
    }

    void LoadPopUp()
    {
        PopUp.Instance.LoadPopUp(_popUpTitle, _popUpDescription, GoBackToMenu);
    }

    void GoBackToMenu()
    {
        StartCoroutine(LoadMenuSceneAsync());
    }

    IEnumerator LoadMenuSceneAsync()
    {
        Time.timeScale = 0f;

        _loadingPanel.SetActive(true);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_menuScene, LoadSceneMode.Single);

        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            OnProgressUpdate(asyncLoad.progress);

            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }

        Time.timeScale = 1f;
    }
}
