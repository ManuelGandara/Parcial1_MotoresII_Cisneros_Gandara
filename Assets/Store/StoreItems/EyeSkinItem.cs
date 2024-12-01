using UnityEngine;

public class EyeSkinItem : UnitaryStoreItem
{
    [Header("Eye Color")]
    [SerializeField] private Color _eyeColor;

    public override void Obtain()
    {
        // Debería cambiar el skin de los ojos
    }
}
