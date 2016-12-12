using System;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class MMTweenPosition : MMUITweener
{
    public Vector3 From, To;
    public bool UseWorldPosition, UseRigidbody;

    public Vector3 Value { get; private set; }

    RectTransform myTransform;
    Rigidbody myRigidbody;
    Rigidbody2D myRigidbody2D;

    protected override void Wake()
    {
        if (UseWorldPosition && UseRigidbody)
            CheckForRigidbodyAndCollider();
        else
            myTransform = gameObject.GetComponent<RectTransform>();

        base.Wake();
    }

    void CheckForRigidbodyAndCollider()
    {
        //TODO: Şimdilik sadece 3D rigidbody check edilip ekleniyor, ilerde projenin 2D veya 3D olmasına bağlı olarak check işlemi yapılmalı
        if (gameObject.GetComponent<Rigidbody>() == null)
        {
            myRigidbody = gameObject.AddComponent<Rigidbody>();
            myRigidbody.isKinematic = true;
        }
        else
            myRigidbody = gameObject.GetComponent<Rigidbody>();

        if (gameObject.GetComponent<Collider>() == null)
            gameObject.AddComponent<BoxCollider>();
    }

    protected override void SetValue(float clampedValue)
    {
        Vector3 diff = To - From;
        Vector3 delta = diff * clampedValue;

        Value = From + delta;
    }

    protected override void PlayAnim()
    {
        if (UseWorldPosition)
        {
            if (UseRigidbody)
            {
                if (myRigidbody != null)
                    myRigidbody.position = Value;
                else if(myRigidbody2D != null)
                    myRigidbody2D.position = Value;
            }
            else if(myTransform != null)
                myTransform.position = Value;
        }
        else if(myTransform != null)
            myTransform.localPosition = Value;
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
        if (UseWorldPosition)
            From = GetComponent<RectTransform>().position;
        else
            From = GetComponent<RectTransform>().localPosition;
    }

    [ContextMenu("Set TO")]
    void SetTo()
    {
        if (UseWorldPosition)
            To = GetComponent<RectTransform>().position;
        else
            To = GetComponent<RectTransform>().localPosition;
    }

    [ContextMenu("Assume FROM")]
    void AssumeFrom()
    {
        if (UseWorldPosition)
            GetComponent<RectTransform>().position= From;
        else
            GetComponent<RectTransform>().localPosition = From;
    }

    [ContextMenu("Assume TO")]
    void AssumeTo()
    {
        if (UseWorldPosition)
            GetComponent<RectTransform>().position = To;
        else
            GetComponent<RectTransform>().localPosition = To;
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
