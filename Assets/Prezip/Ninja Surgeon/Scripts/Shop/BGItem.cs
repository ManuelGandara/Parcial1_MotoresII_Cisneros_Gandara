using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGItem : Item
{
    [SerializeField] private Sprite _background;

    public override string GetPopUpTitle()
    {
        return "Buy Background";
    }

    public override string GetPopUpDescription()
    {
        return $"Spend ${Price} to view the {_background.name} image in the background?";
    }

    public override bool WasSold()
    {
        return BackgroundManager.Instance.HasBG(_background);
    }

    protected override bool SatisfiesAdditionalBuyConditions()
    {
        return BackgroundManager.Instance.DoesNotHaveBG(_background);
    }

    protected override void PurchaseAction()
    {
        BackgroundManager.Instance.ObtainBG(_background);
    }
}
