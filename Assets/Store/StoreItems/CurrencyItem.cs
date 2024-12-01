using UnityEngine;

public class CurrencyItem : StoreItem
{
    [SerializeField] private int _obtainedCurrency;

    public override void Obtain()
    {
        StoreManager.Instance.ObtainCurrency(_obtainedCurrency);
    }
}
