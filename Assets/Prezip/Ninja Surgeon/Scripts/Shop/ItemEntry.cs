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
    Text _buyButtonText;

    public ItemEntry Build(Item item, Shop shop)
    {
        _item = item;

        _shop = shop;

        gameObject.name = $"{_item.name} Item Entry";

        _icon.sprite = _item.Icon;

        _nameText.text = _item.Name;

        _priceText.text = $"$ {_item.Price}";

        _buyButtonText = _buyButton.GetComponentInChildren<Text>();

        _buyButton.onClick.AddListener(Buy);

        UpdateBuyConditions(null);

        _shop.OnPurchase += UpdateBuyConditions;

        StoreManager.Instance.OnStoreStatusUpdate += UpdateBuyConditions;

        RestoreDataManager.Instance.OnRestore += UpdateBuyConditions;

        return this;
    }

    void OnDestroy()
    {
        _shop.OnPurchase -= UpdateBuyConditions;

        StoreManager.Instance.OnStoreStatusUpdate -= UpdateBuyConditions;

        RestoreDataManager.Instance.OnRestore -= UpdateBuyConditions;
    }

    void Buy()
    {
        PopUp.Instance.LoadPopUp(_item.GetPopUpTitle(), _item.GetPopUpDescription(), () => _shop.PurchaseItem(_item));
    }

    void UpdateBuyConditions()
    {
        _buyButton.enabled = _item.CanBuy();

        _buyButtonText.text = _item.WasSold() ? "Sold" : "Buy";
    }

    void UpdateBuyConditions(object any)
    {
        UpdateBuyConditions();
    }
}
