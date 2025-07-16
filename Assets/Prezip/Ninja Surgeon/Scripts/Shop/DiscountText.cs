using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DiscountText : MonoBehaviour
{
    [SerializeField] private float _maxExpansionScale = 1.2f;
    [SerializeField] private float _timeToScale = 3f;

    GameObject _parentComponent;
    Vector3 _minScale, _maxScale;

    void Start()
    {
        _minScale = transform.localScale;

        _maxScale = new Vector3(_maxExpansionScale, _maxExpansionScale, _maxExpansionScale);

        Text discountText = GetComponent<Text>();

        _parentComponent = discountText.transform.parent.gameObject;

        float discountPercentile = RemoteConfigManager.Instance.RemoteConfigValues.StoreDiscount;

        if (discountPercentile > 0)
        {
            discountText.text = $"{Mathf.FloorToInt(discountPercentile * 100)}% Off!";

            StartCoroutine(FloatButton());
        }
        else
        {
            _parentComponent.SetActive(false);
        }
    }

    IEnumerator FloatButton()
    {
        Vector3 sourceScale = _minScale;

        Vector3 targetScale = _maxScale;

        while (true)
        {
            float timeElapsed = 0;

            while (timeElapsed < _timeToScale)
            {
                _parentComponent.transform.localScale = Vector3.Lerp(sourceScale, targetScale, timeElapsed / _timeToScale);

                timeElapsed += Time.deltaTime;

                yield return null;
            }

            targetScale = sourceScale;

            sourceScale = _parentComponent.transform.localScale;
        }
    }
}
