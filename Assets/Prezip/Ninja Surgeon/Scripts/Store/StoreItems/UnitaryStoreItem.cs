using UnityEngine;

public abstract class UnitaryStoreItem : StoreItem
{
    string _id;

    public UnitaryStoreItem(string id, string name, string emoji, int cost, Color bgColor) : base(name, emoji, cost, bgColor)
    {
        _id = id;
    }

    public string Id { get { return _id; } }

    public override bool CanBuy()
    {
        return base.CanBuy() && !WasSold();
    }

    public override void Purchase()
    {
        StoreManager.Instance.PurchaseUnitaryItem(this);
    }

    public bool WasSold()
    {
        return StoreManager.Instance.DidSell(_id);
    }

    public void TryObtain()
    {
        if (WasSold()) Obtain();
    }
}
