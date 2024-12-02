using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemsDisplay : MonoBehaviour
{
    [Header("Store Item")]
    [SerializeField] private StoreItemIcon _storeItemIconPrefab;
    [SerializeField] private GridLayoutGroup _gridLayout;

    [Header("Buy Button")]
    [SerializeField] private Button _buyButton;

    [Header("Close Button")]
    [SerializeField] private Button _closeButton;

    StoreItem _selectedStoreItem;

    public delegate void StoreItemSelect(StoreItem storeItemIcon);

    public event StoreItemSelect OnStoreItemSelect;

    void Start()
    {
        LoadStoreItems();

        ConfigureBuyButton();

        ConfigureCloseButton();

        StoreManager.Instance.OnStoreStatusUpdate += UpdatePossibleItemsToBuy;
    }

    public void SelectStoreItem(StoreItem selectedStoreItem)
    {
        if (_selectedStoreItem != selectedStoreItem)
        {
            _selectedStoreItem = selectedStoreItem;

            _buyButton.interactable = true;
        }
        else
        {
            UnselectStoreItem();
        }

        OnStoreItemSelect(selectedStoreItem);
    }

    private void UnselectStoreItem()
    {
        _selectedStoreItem = null;

        _buyButton.interactable = false;
    }

    private void OnStoreItemBuy()
    {
        if (_selectedStoreItem == null) throw new Exception("Trying to buy an item that wasn't selected");

        _selectedStoreItem.Purchase();
    }

    private void LoadStoreItems()
    {
        List<StoreItem> storeItems = StoreManager.Instance.StoreItems;

        foreach (StoreItem storeItem in storeItems)
        {
            Instantiate(_storeItemIconPrefab, transform.position, transform.rotation).Configure(storeItem, _gridLayout.transform, this);
        }
    }

    private void ConfigureBuyButton()
    {
        _buyButton.interactable = false;

        _buyButton.onClick.AddListener(OnStoreItemBuy);
    }

    private void ConfigureCloseButton()
    {
        _closeButton.onClick.AddListener(UnselectStoreItem);
    }

    private void UpdatePossibleItemsToBuy(StoreStatus storeStatus)
    {
        UnselectStoreItem();
    }
}
