using UnityEngine;

public abstract class UnitaryStoreItem : StoreItem
{
    [SerializeField] private string _id;

    public override bool CanBuy(int currentCurrency)
    {
        return base.CanBuy(currentCurrency) && StoreManager.Instance.DidSell(_id);
    }
}
