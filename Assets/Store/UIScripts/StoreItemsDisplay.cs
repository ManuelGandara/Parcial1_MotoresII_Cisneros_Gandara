using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemsDisplay : MonoBehaviour
{
    [Header("Store Text")]
    [SerializeField] private StoreItemIcon _storeItemIconPrefab;
    [SerializeField] private GridLayoutGroup _gridLayout;

    void Start()
    {
        List<StoreItem> storeItems = StoreManager.Instance.StoreItems;

        foreach (StoreItem storeItem in storeItems)
        {
            Instantiate(_storeItemIconPrefab, transform.position, transform.rotation).Configure(storeItem, _gridLayout.transform);
        }
    }
}
