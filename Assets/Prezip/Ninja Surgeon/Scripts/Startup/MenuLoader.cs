using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLoader : MonoBehaviour
{
    [SerializeField] private string _mainMenuScene = "Menu";

    public event Action OnLoadStart = delegate { };

    public event Action<float> OnProgressUpdate = delegate { };

    public void LoadMenu()
    {
        OnLoadStart();

        StartCoroutine(LoadMenuAsync());
    }

    IEnumerator LoadMenuAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_mainMenuScene, LoadSceneMode.Single);

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
    }
}
