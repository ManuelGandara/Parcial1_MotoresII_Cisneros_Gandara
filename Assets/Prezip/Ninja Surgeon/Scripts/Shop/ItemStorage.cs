using System;
using System.Collections.Generic;
using System.Linq;

public class ItemStorage<T>
{
    bool _selectOnObtain;
    Func<string, T> _itemIdToItemMapper;
    JSONStorage<ObtainedItems> _persistedItems;
    List<T> _availableItems;
    ObtainedItems _defaultData, _obtainedItems;
    T _selectedItem;

    public ItemStorage(string fileName, ObtainedItems defaultData, Func<string, T> itemIdToItemMapper, bool retrieveOnLoad = true, bool selectOnObtain = true)
    {
        _persistedItems = new JSONStorage<ObtainedItems>(fileName);

        _defaultData = defaultData;

        _itemIdToItemMapper = itemIdToItemMapper;

        _selectOnObtain = selectOnObtain;

        if (retrieveOnLoad)
        {
            RetrieveItems();
        }
    }

    public T SelectedItem { get { return _selectedItem; } }

    public List<T> AvailableItems { get { return _availableItems; } }

    public void SelectItem(T item, string itemId)
    {
        _selectedItem = item;

        _obtainedItems.SelectedItem = itemId;

        _persistedItems.Persist(_obtainedItems);
    }

    public void ObtainItem(T item, string itemId)
    {
        _availableItems.Add(item);

        _obtainedItems.Items = _obtainedItems.Items.ToHashSet().Append(itemId).ToArray();

        if (_selectOnObtain)
        {
            SelectItem(item, itemId);
        }
        else
        {
            _persistedItems.Persist(_obtainedItems);
        }
    }

    public void RetrieveItems()
    {
        _obtainedItems = _persistedItems.Load(_defaultData);

        _selectedItem = _itemIdToItemMapper(_obtainedItems.SelectedItem);

        _availableItems = _obtainedItems.Items.Select(_itemIdToItemMapper).ToList();
    }

    public void ResetItems()
    {
        _persistedItems.Delete();

        RetrieveItems();
    }
}