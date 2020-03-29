using System;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class MMTweenPosition : MMUITweener
{
    public Vector3 From, To;
    public bool UseRigidbody;

    public Vector3 Value { get; private set; }

    private RectTransform _myTransform;
    private Rigidbody _myRigidbody;
    private Rigidbody2D _myRigidbody2D;

    protected override void Wake()
    {
        if (UseRigidbody)
            CheckForRigidbodyAndCollider();
        else
            _myTransform = gameObject.GetComponent<RectTransform>();

        base.Wake();
    }

    void CheckForRigidbodyAndCollider()
    {
        //TODO: Şimdilik sadece 3D rigidbody check edilip ekleniyor, ilerde projenin 2D veya 3D olmasına bağlı olarak check işlemi yapılmalı
        if (gameObject.GetComponent<Rigidbody>() == null)
        {
            _myRigidbody = gameObject.AddComponent<Rigidbody>();
            _myRigidbody.isKinematic = true;
        }
        else
            _myRigidbody = gameObject.GetComponent<Rigidbody>();

        if (gameObject.GetComponent<Collider>() == null)
            gameObject.AddComponent<BoxCollider>();
    }

    protected override void SetValue(float clampedValue)
    {
        if(Ease == MMTweeningEaseEnum.Shake/* || Ease == MMTweeningEaseEnum.Punch*/)
        {
            Vector2 delta = ShakePunchDirection * ShakePunchAmount * clampedValue;

            Value = GetComponent<RectTransform>().anchoredPosition + delta;
        }
        else
        {
            Vector3 diff = To - From;
            Vector3 delta = diff * clampedValue;

            Value = From + delta;
        }
    }

    protected override void PlayAnim()
    {
        if (UseRigidbody)
        {
            if (_myRigidbody != null)
                _myRigidbody.position = Value;
            else if(_myRigidbody2D != null)
                _myRigidbody2D.position = Value;
        }
        else if(_myTransform != null)
            _myTransform.anchoredPosition = Value;
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
        From = GetComponent<RectTransform>().anchoredPosition;
    }

    [ContextMenu("Set TO")]
    void SetTo()
    {
        To = GetComponent<RectTransform>().anchoredPosition;
    }

    [ContextMenu("Assume FROM")]
    void AssumeFrom()
    {
        GetComponent<RectTransform>().anchoredPosition = From;
    }

    [ContextMenu("Assume TO")]
    void AssumeTo()
    {
        GetComponent<RectTransform>().anchoredPosition = To;
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
