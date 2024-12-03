using TMPro;
using UnityEngine;

public class StaminaDisplay : MonoBehaviour
{
    [Header("Stamina Text")]
    [SerializeField] private TextMeshProUGUI _staminaText;
    [SerializeField] private TextMeshProUGUI _nextUpdateText;
    [SerializeField] private TextMeshProUGUI _lastUpdateText;

    void Start()
    {
        StaminaManager.Instance.OnStaminaUpdate += UpdateStaminaDisplay;
    }

    private void UpdateStaminaDisplay(Stamina stamina)
    {
        _staminaText.text = $"Stamina: {stamina.Amount} / {StaminaManager.Instance.MaxStamina}";

        _nextUpdateText.text = $"Next Update: {stamina.NextUpdateTime}";

        _lastUpdateText.text = $"Last Update: {stamina.LastUpdateTime}";
    }
}
