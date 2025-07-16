using UnityEngine;

public class StaminaRechargeItem : Item
{
    [SerializeField] private int _staminaRecovered = 10;

    protected override void PurchaseAction()
    {
        StaminaManager.Instance.RechargeStamina(_staminaRecovered);
    }
}
