using UnityEngine;

public class StaminaItem : StoreItem
{
    [Header("Effect Attributes")]
    [SerializeField] private int _obtainedStamina;

    public override void Obtain()
    {
        StaminaManager.Instance.RechargeStamina(_obtainedStamina);
    }
}
