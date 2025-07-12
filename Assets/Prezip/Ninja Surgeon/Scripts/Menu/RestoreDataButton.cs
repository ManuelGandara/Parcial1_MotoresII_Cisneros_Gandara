using UnityEngine;
using UnityEngine.UI;

public class RestoreDataButton : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(RestoreData);
    }

    void RestoreData()
    {
        StaminaManager.Instance.ResetStamina();

        StoreManager.Instance.ResetStore();
    }
}
