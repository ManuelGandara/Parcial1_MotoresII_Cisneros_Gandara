using UnityEngine;

// Esto es para probar en la UI. Por defecto está deshabilitado
public class StaminaTestController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Recargo Stamina");

            StaminaManager.Instance.RechargeStamina(10);
        }
        
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("Consumo Stamina");

            StaminaManager.Instance.ConsumeStamina(10);
        }
    }
}
