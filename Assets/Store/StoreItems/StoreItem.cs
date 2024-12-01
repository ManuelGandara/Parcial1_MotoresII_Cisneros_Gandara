using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class StoreItem : MonoBehaviour
{
    [Header("Store Properties")]
    [SerializeField] private string _name;
    [SerializeField] private int _cost;

    [Header("Display")]
    [SerializeField] private Image _highlightImage;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private TextMeshProUGUI _descriptionText;

    protected virtual void Start()
    {
        _costText.text = _cost.ToString();

        _descriptionText.text = _name;

        ToggleSelection(false);
    }

    public string Name { get { return _name; } }

    public int Cost { get { return _cost; } }

    public virtual bool CanBuy(int currentCurrency)
    {
        return currentCurrency >= _cost;
    }

    public void ToggleSelection(bool isSelected)
    {
        _highlightImage.gameObject.SetActive(isSelected);
    }

    public abstract void Obtain();
}
