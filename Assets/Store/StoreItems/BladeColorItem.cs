using UnityEngine;

public class BladeColorItem : UnitaryStoreItem
{
    Color _bladeColor;

    public BladeColorItem(string id, string name, string emoji, int cost, Color bgColor, Color bladeColor) : base(id, name, emoji, cost, bgColor)
    {
        _bladeColor = bladeColor;
    }

    public override void Obtain()
    {
       Debug.Log($"Cambio el color de los ojos a {_bladeColor}"); // En algún lado habría que cambiar el color del Line Renderer de la espada
    }
}
