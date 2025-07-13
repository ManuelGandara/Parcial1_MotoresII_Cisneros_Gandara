using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySceneLoader : MonoBehaviour
{
    [SerializeField] private string _playScene = "Play";
    [SerializeField] private int _playCost = 10;

    public event Action<bool> OnStaminaCheck = delegate { };

    public event Action OnLoadStart = delegate { };

    public event Action<float> OnProgressUpdate = delegate { };

    public bool CanPlay { get; private set; }

    void Awake()
    {
        CanPlay = StaminaManager.Instance.CurrentStamina.Amount >= _playCost;
    }

    void Start()
    {
        StaminaManager.Instance.OnStaminaUpdate += CheckPlayCondition;
    }

    void OnDestroy()
    {
        StaminaManager.Instance.OnStaminaUpdate -= CheckPlayCondition;
    }

    public void Play()
    {
        OnLoadStart();

        StartCoroutine(LoadPlaySceneAsync());
    }

    public void CheckPlayCondition(Stamina stamina)
    {
        CanPlay = stamina.Amount >= _playCost;

        OnStaminaCheck(CanPlay);
    }

    IEnumerator LoadPlaySceneAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_playScene, LoadSceneMode.Single);

        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            OnProgressUpdate(asyncLoad.progress);

            if (asyncLoad.progress >= 0.9f)
            {
                StaminaManager.Instance.ConsumeStamina(_playCost);

                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
