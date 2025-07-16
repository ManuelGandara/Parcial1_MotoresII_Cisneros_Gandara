using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BackgroundAssigner : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        _spriteRenderer.sprite = BackgroundManager.Instance.SelectedBG;
    }
}
