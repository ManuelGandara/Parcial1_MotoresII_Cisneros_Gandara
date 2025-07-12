using System;
using System.Collections;
using UnityEngine;

public class LoadingTips : MonoBehaviour
{
    [SerializeField] private string[] _tips;
    [SerializeField] private float _tipsRefreshTimeInSeconds = 3;

    public event Action<string> OnTipUpdate = delegate { };

    public string CurrentTip { get { return _tips[_currentTipIndex]; } }

    int _currentTipIndex;
    Coroutine _tipsDisplayCoroutine;

    void OnEnable()
    {
        _currentTipIndex = UnityEngine.Random.Range(0, _tips.Length);

        _tipsDisplayCoroutine = StartCoroutine(DisplayTip());
    }

    void OnDisable()
    {
        StopCoroutine(_tipsDisplayCoroutine);
    }

    IEnumerator DisplayTip()
    {
        while (true)
        {
            OnTipUpdate(CurrentTip);

            yield return new WaitForSeconds(_tipsRefreshTimeInSeconds);

            _currentTipIndex = (_currentTipIndex + 1) % _tips.Length;
        }
    }
}
