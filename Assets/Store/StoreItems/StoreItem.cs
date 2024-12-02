using UnityEngine;

public abstract class StoreItem
{
    string _name;
    string _emoji;
    int _cost;
    Color _bgColor;

    public StoreItem(string name, string emoji, int cost, Color bgColor)
    {
        _name = name;
        _emoji = emoji;
        _cost = cost;
        _bgColor = bgColor;
    }

    public string Name { get { return _name; } }

    public string Emoji { get { return _emoji; } }

    public int Cost { get { return _cost; } }

    public Color BGColor { get { return _bgColor; } }

    public virtual bool CanBuy(int currentCurrency)
    {
        return currentCurrency >= _cost;
    }

    public virtual void Purchase()
    {
        StoreManager.Instance.PurchaseItem(this);
    }

    public abstract void Obtain();
}
