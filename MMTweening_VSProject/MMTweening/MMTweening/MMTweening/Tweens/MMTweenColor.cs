using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
/// <summary>
/// Does NOT work with MMTweenAlpha.
/// </summary>
public class MMTweenColor : MMUITweener
{
    public Color From, To;

    public Color Value { get; private set; }

    Image myImage;

    protected override void Wake()
    {
        myImage = gameObject.GetComponent<Image>();

        InitValueToFROM();
    }

    protected override void SetValue(float clampedValue)
    {
        float r = CalculateR(clampedValue);
        float g = CalculateG(clampedValue);
        float b = CalculateB(clampedValue);
        float a = CalculateA(clampedValue);

        Value = From + new Color(r, g, b, a);
    }

    float CalculateR(float clampedValue)
    {    
        float diff = To.r - From.r;
        return diff * clampedValue;
    }

    float CalculateG(float clampedValue)
    {
        float diff = To.g - From.g;
        return diff * clampedValue;
    }

    float CalculateB(float clampedValue)
    {
        float diff = To.b - From.b;
        return diff * clampedValue;
    }

    float CalculateA(float clampedValue)
    {
        float diff = To.a - From.a;
        return diff * clampedValue;
    }

    protected override void PlayAnim()
    {
        if (myImage == null)
            return;

        myImage.color = Value;
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
        From = GetComponent<Image>().color;
    }

    [ContextMenu("Set TO")]
    void SetTo()
    {
        To = GetComponent<Image>().color;
    }

    [ContextMenu("Assume FROM")]
    void AssumeFrom()
    {
        GetComponent<Image>().color = From;
    }

    [ContextMenu("Assume TO")]
    void AssumeTo()
    {
        GetComponent<Image>().color = To;
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
