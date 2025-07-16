using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private int _price;
    [SerializeField] private Sprite _icon;

    public string Name { get { return _name; } }

    public int Price { get { return _price - Mathf.FloorToInt(RemoteConfigManager.Instance.RemoteConfigValues.StoreDiscount * _price); } }

    public Sprite Icon { get { return _icon; } }

    public bool CanBuy()
    {
        return StoreManager.Instance.Currency >= Price && SatisfiesAdditionalBuyConditions();
    }

    public virtual bool CanRetrieve()
    {
        return SatisfiesAdditionalBuyConditions();
    }

    public virtual bool WasSold()
    {
        return false;
    }

    public void GetPurchased()
    {
        StoreManager.Instance.SpendCurrency(Price);

        GetRetrieved();
    }

    public void GetRetrieved()
    {
        PurchaseAction();
    }

    public abstract string GetPopUpTitle();

    public abstract string GetPopUpDescription();

    protected abstract bool SatisfiesAdditionalBuyConditions();

    protected abstract void PurchaseAction();
}
