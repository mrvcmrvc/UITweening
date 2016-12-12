using System;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class MMTweenScale : MMUITweener
{
    public Vector3 From, To;

    public Vector3 Value { get; private set; }

    RectTransform myTransform;

    #region implemented abstract members of MMUITweener

    protected override void Wake()
    {
        myTransform = gameObject.GetComponent<RectTransform>();

        base.Wake();
    }

    protected override void SetValue(float clampedValue)
    {
        Vector3 diff = To - From;
        Vector3 delta = diff * clampedValue;

        Value = From + delta;
    }

    protected override void PlayAnim()
    {
        if (myTransform == null)
            return;

        myTransform.localScale = Value;
    }

    protected override void Finish()
    {
    }

    protected override void Kill()
    {
    }

    #endregion

    #region ContextMenu

    [ContextMenu("Set FROM")]
    void SetFrom()
    {
        From = GetComponent<RectTransform>().localScale;
    }

    [ContextMenu("Set TO")]
    void SetTo()
    {
        To = GetComponent<RectTransform>().localScale;
    }

    [ContextMenu("Assume FROM")]
    void AssumeFrom()
    {
        GetComponent<RectTransform>().localScale = From;
    }

    [ContextMenu("Assume TO")]
    void AssumeTo()
    {
        GetComponent<RectTransform>().localScale = To;
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
