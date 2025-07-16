using UnityEngine;

[RequireComponent(typeof(Shop))]
public class ShopEntry : MonoBehaviour
{
    [SerializeField] private GameObject _itemEntryPrefab;

    Shop _shop;

    void Start()
    {
        _shop = GetComponent<Shop>();

        foreach (Item item in _shop.Items)
        {
            Instantiate(_itemEntryPrefab, transform).GetComponent<ItemEntry>().Build(item, _shop);
        }
    }
}
