using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemIcon : MonoBehaviour
{
    [Header("Display")]
    [SerializeField] public int size;
    [SerializeField] private Image _background;
    [SerializeField] private Image _highlightImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _emojiText;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private Button _selectItemButton;

    StoreItem _storeItem;
    StoreItemsDisplay _storeItemsDisplay;
    Transform _gridLayout;

    public void Configure(StoreItem storeItem, Transform gridLayout, StoreItemsDisplay storeItemsDisplay)
    {
        _storeItem = storeItem;

        _gridLayout = gridLayout;

        _storeItemsDisplay = storeItemsDisplay;

        ConfigureText();

        ConfigureImages();

        ConfigureTransform();

        ConfigureButtons();

        ConfigureSubscriptions();
    }

    public void UpdateSelection(StoreItem selectedStoreItem)
    {
        _highlightImage.gameObject.SetActive(selectedStoreItem == _storeItem);
    }

    private void ConfigureText()
    {
        _emojiText.text = _storeItem.Emoji;

        _costText.text = _storeItem.Cost.ToString();

        _nameText.text = _storeItem.Name;

        gameObject.name = _storeItem.Name;
    }

    private void ConfigureImages()
    {
        _background.color = _storeItem.BGColor;

        _highlightImage.gameObject.SetActive(false);
    }

    private void ConfigureTransform()
    {
        transform.SetParent(_gridLayout);

        transform.localScale = Vector3.one * size;
    }

    private void ConfigureButtons()
    {
        _selectItemButton.onClick.AddListener(() => _storeItemsDisplay.SelectStoreItem(_storeItem));

        _selectItemButton.interactable = _storeItem.CanBuy();
    }

    private void ConfigureSubscriptions()
    {
        _storeItemsDisplay.OnStoreItemSelect += OnItemSelect;

        StoreManager.Instance.OnStoreStatusUpdate += OnPurchase;
    }

    private void OnItemSelect(StoreItem storeItem)
    {
        _highlightImage.gameObject.SetActive(_storeItem == storeItem);
    }

    private void OnPurchase(StoreStatus storeStatus)
    {
        _selectItemButton.interactable = _storeItem.CanBuy();

        _highlightImage.gameObject.SetActive(false);
    }
}
