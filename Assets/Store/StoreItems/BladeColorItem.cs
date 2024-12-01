using UnityEngine;

public class BladeColorItem : UnitaryStoreItem
{
    [Header("Blade Color")]
    [SerializeField] private Color color;

    public override void Obtain()
    {
        // En algún lado habría que guardar el color de la espada
    }
}
