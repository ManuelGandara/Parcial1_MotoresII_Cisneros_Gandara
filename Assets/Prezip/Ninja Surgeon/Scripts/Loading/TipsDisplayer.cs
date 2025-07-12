using UnityEngine;
using UnityEngine.UI;

public class TipsDisplayer : MonoBehaviour
{
    [SerializeField] private Text _tipsText;

    LoadingTips _loadingTips;

    void Awake()
    {
        _loadingTips = GetComponent<LoadingTips>();
    }

    void OnEnable()
    {
        _loadingTips.OnTipUpdate += UpdateTipText;

        UpdateTipText(_loadingTips.CurrentTip);
    }

    void OnDisable()
    {
        _loadingTips.OnTipUpdate -= UpdateTipText;
    }

    void UpdateTipText(string tip)
    {
        _tipsText.text = "Pro Tip: " + tip;
    }
}
