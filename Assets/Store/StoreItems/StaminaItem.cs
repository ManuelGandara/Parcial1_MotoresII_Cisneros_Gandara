using UnityEngine;

public class StaminaItem : StoreItem
{
    [SerializeField] private int _obtainedStamina;

    public override void Obtain()
    {
        StaminaManager.Instance.RechargeStamina(_obtainedStamina);
    }
}
