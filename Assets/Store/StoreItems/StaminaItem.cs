using UnityEngine;

public class StaminaItem : StoreItem
{
    int _obtainedStamina;

    public StaminaItem(string name, string emoji, int cost, Color bgColor, int obtainedStamina) : base(name, emoji, cost, bgColor)
    {
        _obtainedStamina = obtainedStamina;
    }

    public override void Obtain()
    {
        StaminaManager.Instance.RechargeStamina(_obtainedStamina);
    }
}
