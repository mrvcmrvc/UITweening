using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Does NOT work with MMTweenAlpha.
/// </summary>
public class MMTweenColor : MMUITweener
{
    public Color From, To;

    public Color Value { get; private set; }

    Graphic myImage;

    protected override void Wake()
    {
        myImage = gameObject.GetComponent<Graphic>();

        base.Wake();
    }

    protected override void SetValue(float clampedValue)
    {
        float fromH, fromS, fromV;
        float toH, toS, toV;

        Color.RGBToHSV(From, out fromH, out fromS, out fromV);
        Color.RGBToHSV(To, out toH, out toS, out toV);

        float h = CalculateHue(clampedValue, fromH, toH);

        float s = CalculateSV(clampedValue, fromS, toS);
        float v = CalculateSV(clampedValue, fromV, toV);

        Value = Color.HSVToRGB(h, s, v);
    }

    float CalculateHue(float clampedValue, float from, float to)
    {
        float diff = to - from;
        int dirSign = 1;

        if (Mathf.Abs(diff) > 0.5f)
        {
            if (diff > 0)
                dirSign = -1;

            diff = 1 - Mathf.Abs(diff);
        }

        float newH = from + (dirSign * diff * clampedValue);
        if (newH > 1.0f)
            newH -= 1;
        else if (newH < 0.0f)
            newH += 1.0f;

        return newH;
    }

    float CalculateSV(float clampedValue, float from, float to)
    {
        float diff = to - from;
        return from + (diff * clampedValue);
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
        From = GetComponent<Graphic>().color;
    }

    [ContextMenu("Set TO")]
    void SetTo()
    {
        To = GetComponent<Graphic>().color;
    }

    [ContextMenu("Assume FROM")]
    void AssumeFrom()
    {
        GetComponent<Graphic>().color = From;
    }

    [ContextMenu("Assume TO")]
    void AssumeTo()
    {
        GetComponent<Graphic>().color = To;
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
