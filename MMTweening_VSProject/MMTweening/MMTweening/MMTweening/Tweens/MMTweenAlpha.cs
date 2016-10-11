using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
/// <summary>
/// Does NOT work with MMTweenColor.
/// </summary>
public class MMTweenAlpha : MMUITweener
{
    [HideInInspector]
    public float From, To;

    public float Value { get; private set; }

    Image myImage;

    protected override void Wake()
    {
        myImage = gameObject.GetComponent<Image>();

        InitValueToFROM();
    }

    protected override void SetValue(float clampedValue)
    {
        Value = From + CalculateA(clampedValue);
    }

    float CalculateA(float clampedValue)
    {
        float diff = To - From;
        return diff * clampedValue;
    }

    protected override void PlayAnim()
    {
        if (myImage == null)
            return;

        Color color = myImage.color;
        color.a = Value;

        myImage.color = color;
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
        From = GetComponent<Image>().color.a;
    }

    [ContextMenu("Set TO")]
    void SetTo()
    {
        To = GetComponent<Image>().color.a;
    }

    [ContextMenu("Assume FROM")]
    void AssumeFrom()
    {
        Color color = GetComponent<Image>().color;
        color.a = From;

        GetComponent<Image>().color = color;
    }

    [ContextMenu("Assume TO")]
    void AssumeTo()
    {
        Color color = GetComponent<Image>().color;
        color.a = To;

        GetComponent<Image>().color = color;
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
