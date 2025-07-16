using System;
using UnityEngine;

public class GachaSystem : MonoBehaviour
{
    [SerializeField][Range(0, 1)] private float _displayProbability = 0.5f;
    [SerializeField] private Item[] _rewards;
    [SerializeField] private Item _defaultReward;

    public static GachaSystem Instance;

    public event Action<Item, Item> OnActivation = delegate { };

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void TryPickReward()
    {
        if (UnityEngine.Random.Range(0f, 1f) > _displayProbability)
        {
            Item randomReward = _rewards[UnityEngine.Random.Range(0, _rewards.Length)];

            OnActivation(randomReward, _defaultReward);
        }
    }
}
