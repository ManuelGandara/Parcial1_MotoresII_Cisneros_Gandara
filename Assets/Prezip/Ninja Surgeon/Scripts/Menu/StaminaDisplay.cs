using UnityEngine;
using UnityEngine.UI;

public class StaminaDisplay : MonoBehaviour
{
    [SerializeField] private Text _staminaText;

    void Start()
    {
        StaminaManager.Instance.OnStaminaUpdate += DisplayNextStamina;

        DisplayNextStamina(StaminaManager.Instance.CurrentStamina);
    }

    void DisplayNextStamina(Stamina stamina)
    {
        _staminaText.text = "♥ " + stamina.Amount;
    }
}
