using UnityEngine;

public abstract class UnitaryStoreItem : StoreItem
{
    string _id;

    public UnitaryStoreItem(string id, string name, string emoji, int cost, Color bgColor) : base(name, emoji, cost, bgColor)
    {
        _id = id;

        if (WasSold()) Obtain();
    }

    public string Id { get { return _id; } }

    public override bool CanBuy(int currentCurrency)
    {
        return base.CanBuy(currentCurrency) && WasSold();
    }

    public override void Purchase()
    {
        StoreManager.Instance.PurchaseUnitaryItem(this);
    }

    private bool WasSold()
    {
        return StoreManager.Instance.DidSell(_id);
    }
}
