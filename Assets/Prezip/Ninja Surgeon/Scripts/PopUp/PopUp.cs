using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    [SerializeField] private GameObject _popUpPanel;
    [SerializeField] private Text _popUpTitle;
    [SerializeField] private Text _popUpDescription;
    [SerializeField] private Button _yesButton;
    [SerializeField] private Button _noButton;
    [SerializeField] private Button _closeButton;

    UnityAction _onAccept;

    public static PopUp Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        _noButton.onClick.AddListener(Close);

        _closeButton.onClick.AddListener(Close);

        Close();
    }

    void OnDisable()
    {
        _yesButton.onClick.RemoveListener(Close);
    }

    public void LoadPopUp(string title, string description, Action onAccept)
    {
        Time.timeScale = 0;

        _yesButton.onClick.RemoveAllListeners();

        _onAccept = () => {
            onAccept();

            Close();
        };

        _yesButton.onClick.AddListener(_onAccept);

        _popUpTitle.text = title;

        _popUpDescription.text = description;

        _popUpPanel.SetActive(true);
    }

    public void Close()
    {
        Time.timeScale = 1;

        _popUpPanel.SetActive(false);
    }
}
