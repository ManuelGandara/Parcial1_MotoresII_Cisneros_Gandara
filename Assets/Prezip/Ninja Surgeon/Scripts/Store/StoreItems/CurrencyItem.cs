using UnityEngine;

public class CurrencyItem : StoreItem
{
    int _obtainedCurrency;

    public CurrencyItem(string name, string emoji, int cost, Color bgColor, int obtainedCurrency) : base(name, emoji, cost, bgColor)
    {
        _obtainedCurrency = obtainedCurrency;
    }

    public override void Obtain()
    {
        StoreManager.Instance.ObtainCurrency(_obtainedCurrency);
    }
}
