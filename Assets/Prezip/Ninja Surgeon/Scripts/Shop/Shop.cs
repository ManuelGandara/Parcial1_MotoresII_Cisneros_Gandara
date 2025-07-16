using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Item[] _availableItems;

    HashSet<Item> _remainingItems;

    public event Action<Item> OnPurchase;

    public Item[] Items { get { return _availableItems; } }

    void Start()
    {
        _remainingItems = _availableItems.ToHashSet();
    }

    public bool CanBuyItem(Item item)
    {
        return item.CanBuy() && ItemIsAvailable(item);
    }

    public bool ItemIsAvailable(Item item)
    {
        return item.IsInfinite || _remainingItems.Contains(item);
    }

    public void PurchaseItem(Item item)
    {
        item.GetPurchased();

        _remainingItems.Remove(item);

        OnPurchase(item);
    }
}
