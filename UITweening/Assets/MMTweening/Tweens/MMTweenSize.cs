using System;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class MMTweenSize : MMUITweener
{
    public Vector2 From, To;

    public Vector2 Value { get; private set; }

    RectTransform myTransform;

    protected override void Wake()
    {
        myTransform = gameObject.GetComponent<RectTransform>();

        base.Wake();
    }

    protected override void SetValue(float clampedValue)
    {
        if (Ease == MMTweeningEaseEnum.Shake/* || Ease == MMTweeningEaseEnum.Punch*/)
        {
            Vector2 delta = ShakePunchDirection * ShakePunchAmount * clampedValue;

            Value = GetComponent<RectTransform>().sizeDelta + delta;
        }
        else
        {
            Vector2 diff = To - From;
            Vector2 delta = diff * clampedValue;

            Value = From + delta;
        }
    }

    protected override void PlayAnim()
    {
        if (myTransform == null)
            return;

        myTransform.sizeDelta = Value;
    }

    protected override void Finish()
    {
    }

    protected override void Kill()
    {
    }

    #region ContextMenu
    [ContextMenu("Set FROM")]
    void SetFrom()
    {
        From = GetComponent<RectTransform>().sizeDelta;
    }

    [ContextMenu("Set TO")]
    void SetTo()
    {
        To = GetComponent<RectTransform>().sizeDelta;
    }

    [ContextMenu("Assume FROM")]
    void AssumeFrom()
    {
        GetComponent<RectTransform>().sizeDelta = From;
    }

    [ContextMenu("Assume TO")]
    void AssumeTo()
    {
        GetComponent<RectTransform>().sizeDelta = To;
    }

    public override void InitValueToFROM()
    {
        Value = From;

        base.InitValueToFROM();
    }

    public override void InitValueToTO()
    {
        Value = To;

        base.InitValueToTO();
    }
    #endregion
}
