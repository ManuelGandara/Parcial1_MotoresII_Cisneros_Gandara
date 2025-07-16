using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private int _price;
    [SerializeField] private bool _isInfinite;
    [SerializeField] private Sprite _icon;

    public string Name { get { return _name; } }

    public int Price { get { return _price; } }

    public bool IsInfinite {  get { return _isInfinite; } }

    public Sprite Icon { get { return _icon; } }

    public virtual bool CanBuy()
    {
        return StoreManager.Instance.Currency >= _price;
    }

    public void GetPurchased()
    {
        StoreManager.Instance.SpendCurrency(_price);

        PurchaseAction();
    }

    protected abstract void PurchaseAction();
}
