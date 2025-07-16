using UnityEngine;
using UnityEngine.UI;

public class RestoreDataButton : MonoBehaviour
{
    [SerializeField] private string _popUpTitle;
    [SerializeField] private string _popUpDescription;

    Button _restoreButton;

    private void Awake()
    {
        _restoreButton = GetComponent<Button>();
    }

    void Start()
    {
        _restoreButton.onClick.AddListener(LoadPopUp);
    }

    void LoadPopUp()
    {
        PopUp.Instance.LoadPopUp(_popUpTitle, _popUpDescription, RestoreDataManager.Instance.RestoreData);
    }
}
