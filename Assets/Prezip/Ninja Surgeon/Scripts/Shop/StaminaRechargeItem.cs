using UnityEngine;

public class StaminaRechargeItem : Item
{
    [SerializeField] private int _staminaRecovered = 10;

    public override string GetPopUpTitle()
    {
        return $"Buy {_staminaRecovered} Stamina";
    }

    public override string GetPopUpDescription()
    {
        return $"Spend ${Price} to recover ${_staminaRecovered} points of stamina?";
    }

    protected override bool SatisfiesAdditionalBuyConditions()
    {
        return StaminaManager.Instance.DoesNotHaveMaxStamina();
    }

    protected override void PurchaseAction()
    {
        StaminaManager.Instance.RechargeStamina(_staminaRecovered);
    }
}
