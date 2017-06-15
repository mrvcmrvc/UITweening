using System;
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
    CanvasGroup myCanvasGroup;

    protected override void Wake()
    {
        myImage = gameObject.GetComponent<Graphic>();
        myCanvasGroup = gameObject.GetComponent<CanvasGroup>();

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
        UpdateImage();

        UpdateCanvasGroup();
    }

    void UpdateImage()
    {
        if (myImage == null)
            return;

        Color color = myImage.color;
        color.a = Value;

        myImage.color = color;
    }

    void UpdateCanvasGroup()
    {
        if (myCanvasGroup == null)
            return;

        myCanvasGroup.alpha = Value;
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
        if(GetComponent<Graphic>() != null)
            From = GetComponent<Graphic>().color.a;
        else if (GetComponent<CanvasGroup>() != null)
            From = GetComponent<CanvasGroup>().alpha;
    }

    [ContextMenu("Set TO")]
    void SetTo()
    {
        if (GetComponent<Graphic>() != null)
            To = GetComponent<Graphic>().color.a;
        else if (GetComponent<CanvasGroup>() != null)
            To = GetComponent<CanvasGroup>().alpha;
    }

    [ContextMenu("Assume FROM")]
    void AssumeFrom()
    {
        if(GetComponent<Graphic>() != null)
        {
            Color color = GetComponent<Graphic>().color;
            color.a = From;

            GetComponent<Graphic>().color = color;
        }
        else if(GetComponent<CanvasGroup>() != null)
            GetComponent<CanvasGroup>().alpha = From;
    }

    [ContextMenu("Assume TO")]
    void AssumeTo()
    {
        if (GetComponent<Graphic>() != null)
        {
            Color color = GetComponent<Graphic>().color;
            color.a = To;

            GetComponent<Graphic>().color = color;
        }
        else if (GetComponent<CanvasGroup>() != null)
            GetComponent<CanvasGroup>().alpha = To;
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
