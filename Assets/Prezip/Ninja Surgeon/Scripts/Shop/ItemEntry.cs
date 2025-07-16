using UnityEngine;
using UnityEngine.UI;

public class ItemEntry : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Text _nameText;
    [SerializeField] private Text _priceText;
    [SerializeField] private Button _buyButton;

    Item _item;
    Shop _shop;

    public ItemEntry Build(Item item, Shop shop)
    {
        _item = item;

        _shop = shop;

        gameObject.name = $"{_item.name} Item Entry";

        _icon.sprite = _item.Icon;

        _nameText.text = _item.Name;

        _priceText.text = $"$ {_item.Price}";

        _buyButton.onClick.AddListener(Buy);

        UpdateBuyConditions(null);

        _shop.OnPurchase += UpdateBuyConditions;

        StoreManager.Instance.OnStoreStatusUpdate += UpdateBuyConditions;

        return this;
    }

    void OnDestroy()
    {
        _shop.OnPurchase -= UpdateBuyConditions;

        StoreManager.Instance.OnStoreStatusUpdate -= UpdateBuyConditions;
    }

    void Buy()
    {
        PopUp.Instance.LoadPopUp(_item.GetPopUpTitle(), _item.GetPopUpDescription(), () => _shop.PurchaseItem(_item));
    }

    void UpdateBuyConditions(object any)
    {
        _buyButton.enabled = _shop.CanBuyItem(_item);

        _buyButton.GetComponentInChildren<Text>().text = _shop.ItemIsAvailable(_item) ? "Buy" : "Sold";
    }
}
