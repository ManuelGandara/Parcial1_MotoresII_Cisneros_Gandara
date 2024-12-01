using UnityEngine;

public abstract class StoreItem : MonoBehaviour
{
    [Header("Store Properties")]
    [SerializeField] private string _name;
    [SerializeField] private int _cost;

    public string Name { get { return _name; } }

    public int Cost { get { return _cost; } }

    public virtual bool CanBuy(int currentCurrency)
    {
        return currentCurrency >= _cost;
    }

    public abstract void Obtain();
}
