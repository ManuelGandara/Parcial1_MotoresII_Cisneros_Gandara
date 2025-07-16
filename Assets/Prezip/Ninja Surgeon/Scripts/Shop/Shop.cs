using System;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Item[] _availableItems;

    public event Action<Item> OnPurchase;

    public Item[] Items { get { return _availableItems; } }

    public void PurchaseItem(Item item)
    {
        item.GetPurchased();

        OnPurchase(item);
    }
}
