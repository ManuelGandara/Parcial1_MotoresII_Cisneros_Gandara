using UnityEngine;

public class EyeSkinItem : UnitaryStoreItem
{
    Color _eyeColor;

    public EyeSkinItem(string id, string name, string emoji, int cost, Color bgColor, Color eyeColor) : base(id, name, emoji, cost, bgColor)
    {
        _eyeColor = eyeColor;
    }

    public override void Obtain()
    {
        Debug.Log($"Cambie color de los ojos a {_eyeColor}"); // Debería cambiar el material de los ojos
    }
}
