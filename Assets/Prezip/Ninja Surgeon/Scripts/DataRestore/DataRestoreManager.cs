using UnityEngine;

public class DataRestoreManager : MonoBehaviour
{
    public void ResetAllData()
    {
        StaminaManager.Instance.ResetStamina();

        StoreManager.Instance.ResetStore();
    }
}
