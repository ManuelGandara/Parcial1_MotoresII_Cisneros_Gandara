using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemIcon : MonoBehaviour
{
    [Header("Display")]
    [SerializeField] private Image _background;
    [SerializeField] private Image _highlightImage;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _emojiText;
    [SerializeField] private TextMeshProUGUI _costText;

    StoreItem _storeItem;

    public void Configure(StoreItem storeItem, Transform gridLayout)
    {
        _storeItem = storeItem;

        _emojiText.text = storeItem.Emoji;

        _costText.text = storeItem.Cost.ToString();

        _nameText.text = storeItem.Name;

        _background.color = storeItem.BGColor;

        _highlightImage.gameObject.SetActive(false);

        transform.SetParent(gridLayout);

        transform.localScale = Vector3.one;

        gameObject.name = storeItem.Name;
    }

    public virtual bool CanBuy()
    {
        return _storeItem.CanBuy(StoreManager.Instance.Currency);
    }
}
