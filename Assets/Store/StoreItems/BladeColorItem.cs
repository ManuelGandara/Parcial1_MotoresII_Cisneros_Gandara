using UnityEngine;

public class BladeColorItem : UnitaryStoreItem
{
    [Header("Blade Color")]
    [SerializeField] private Color color;

    public override void Obtain()
    {
        // En alg�n lado habr�a que guardar el color de la espada
    }
}
