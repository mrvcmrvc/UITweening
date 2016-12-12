using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Does NOT work with MMTweenColor.
/// </summary>
public class MMTweenAlpha : MMUITweener
{
    [HideInInspector]
    public float From, To;

    public float Value { get; private set; }

    Graphic myImage;

    protected override void Wake()
    {
        myImage = gameObject.GetComponent<Graphic>();

        base.Wake();
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
        From = GetComponent<Graphic>().color.a;
    }

    [ContextMenu("Set TO")]
    void SetTo()
    {
        To = GetComponent<Graphic>().color.a;
    }

    [ContextMenu("Assume FROM")]
    void AssumeFrom()
    {
        Color color = GetComponent<Graphic>().color;
        color.a = From;

        GetComponent<Graphic>().color = color;
    }

    [ContextMenu("Assume TO")]
    void AssumeTo()
    {
        Color color = GetComponent<Graphic>().color;
        color.a = To;

        GetComponent<Graphic>().color = color;
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
