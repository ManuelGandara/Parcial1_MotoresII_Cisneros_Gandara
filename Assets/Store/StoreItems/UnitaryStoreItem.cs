using UnityEngine;

public abstract class UnitaryStoreItem : StoreItem
{
    [SerializeField] private string _id;

    protected override void Start()
    {
        base.Start();

        if (StoreManager.Instance.DidSell(_id)) Obtain();
    }

    public string Id { get { return _id; } }

    public override bool CanBuy(int currentCurrency)
    {
        return base.CanBuy(currentCurrency) && StoreManager.Instance.DidSell(_id);
    }
}
