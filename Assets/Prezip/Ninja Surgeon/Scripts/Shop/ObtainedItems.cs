using System;

[Serializable]
public class ObtainedItems
{
    public ObtainedItems(string selectedItem)
    {
        SelectedItem = selectedItem;
    }

    public string SelectedItem;

    public string[] Items = new string[] { };
}
