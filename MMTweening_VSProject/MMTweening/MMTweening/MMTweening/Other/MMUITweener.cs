using System;
using System.Collections.Generic;
using UnityEngine;

public enum MMTweeningEaseEnum
{
    Curve,
    Linear,
    InCubic,
    OutCubic,
    InOutCubic,
    InSine,
    OutSine,
    InOutSine,
    InExpo,
    OutExpo,
    InOutExpo,
    InBack,
    OutBack,
    InOutBack
}

public enum MMTweeningLoopTypeEnum
{
    Once,
    Loop,
    PingPong,
} 

public abstract class MMUITweener : MonoBehaviour
{
    #region CommonVariables
    [HideInInspector]
    public MMTweeningEaseEnum Ease;
    [HideInInspector]
    public MMTweeningLoopTypeEnum LoopType;
    [HideInInspector]
    public AnimationCurve AnimationCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));

    [HideInInspector]
    public bool PlayAutomatically;
    [HideInInspector]
    public bool Delay;
    [HideInInspector]
    public bool IsForward;
    [HideInInspector]
    public bool IsReverse;
    [HideInInspector]
    public bool IsPaused;
    [HideInInspector]
    public bool IsPlaying;
    [HideInInspector]
    public float DelayDuration;
    [HideInInspector]
    public float Duration;
    [HideInInspector]
    public bool IgnoreTimeScale = true;

    public float CurDuration { get { return _curDuration; } }

    int _directionSign;
    float _clampedValue, _curDuration, _enableTime;
    bool _firstEnable, _enabled, _onHalfWayFired;
    #endregion

    #region Events
    public delegate void TweenCallback();

    List<TweenCallback> persistent_onKill, persistent_onFinish, persistent_onStart, persistent_onUpdate, persistent_onPause, persistent_onResume, persistent_onHalfWay;
    List<TweenCallback> nonPersistent_onKill, nonPersistent_onFinish, nonPersistent_onStart, nonPersistent_onUpdate,nonPersistent_onPause, nonPersistent_onResume, nonPersistent_onHalfWay;

    #region Callback Functions
    /// <summary>
    /// If you add non persistent callback, it will be unsubscribed as soon as it is called
    /// </summary>
    /// <param name="callback">Callback to call</param>
    /// <param name="isPersistent"></param>
    /// <returns></returns>
    public MMUITweener AddOnKill(TweenCallback callback, bool isPersistent = true)
    {
        if (isPersistent)
            persistent_onKill.Add(callback);
        else
            nonPersistent_onKill.Add(callback);

        return this;
    }
    /// <summary>
    /// If you add non persistent callback, it will be unsubscribed as soon as it is called
    /// </summary>
    /// <param name="callback">Callback to call</param>
    /// <param name="isPersistent"></param>
    /// <returns></returns>
    public MMUITweener AddOnFinish(TweenCallback callback, bool isPersistent = true)
    {
        if (isPersistent)
            persistent_onFinish.Add(callback);
        else
            nonPersistent_onFinish.Add(callback);

        return this;
    }
    /// <summary>
    /// If you add non persistent callback, it will be unsubscribed as soon as it is called
    /// </summary>
    /// <param name="callback">Callback to call</param>
    /// <param name="isPersistent"></param>
    /// <returns></returns>
    public MMUITweener AddOnStart(TweenCallback callback, bool isPersistent = true)
    {
        if (isPersistent)
            persistent_onStart.Add(callback);
        else
            nonPersistent_onStart.Add(callback);

        return this;
    }
    /// <summary>
    /// If you add non persistent callback, it will be unsubscribed as soon as it is called
    /// </summary>
    /// <param name="callback">Callback to call</param>
    /// <param name="isPersistent"></param>
    /// <returns></returns>
    public MMUITweener AddOnUpdate(TweenCallback callback, bool isPersistent = true)
    {
        if (isPersistent)
            persistent_onUpdate.Add(callback);
        else
            nonPersistent_onUpdate.Add(callback);

        return this;
    }
    /// <summary>
    /// If you add non persistent callback, it will be unsubscribed as soon as it is called
    /// </summary>
    /// <param name="callback">Callback to call</param>
    /// <param name="isPersistent"></param>
    /// <returns></returns>
    public MMUITweener AddOnPause(TweenCallback callback, bool isPersistent = true)
    {
        if (isPersistent)
            persistent_onPause.Add(callback);
        else
            nonPersistent_onPause.Add(callback);

        return this;
    }
    /// <summary>
    /// If you add non persistent callback, it will be unsubscribed as soon as it is called
    /// </summary>
    /// <param name="callback">Callback to call</param>
    /// <param name="isPersistent"></param>
    /// <returns></returns>
    public MMUITweener AddOnResume(TweenCallback callback, bool isPersistent = true)
    {
        if (isPersistent)
            persistent_onResume.Add(callback);
        else
            nonPersistent_onResume.Add(callback);

        return this;
    }
    /// <summary>
    /// If you add non persistent callback, it will be unsubscribed as soon as it is called
    /// </summary>
    /// <param name="callback">Callback to call</param>
    /// <param name="isPersistent"></param>
    /// <returns></returns>
    public MMUITweener AddOnHalfWay(TweenCallback callback, bool isPersistent = true)
    {
        if (isPersistent)
            persistent_onHalfWay.Add(callback);
        else
            nonPersistent_onHalfWay.Add(callback);

        return this;
    }
    #endregion

    #region Callback Fire Functions
    protected void FireOnKill()
    {
        if (nonPersistent_onKill != null && nonPersistent_onKill.Count > 0)
        {
            nonPersistent_onKill.ForEach(pe => pe());
            nonPersistent_onKill.Clear();
        }

        if (persistent_onKill != null && persistent_onKill.Count > 0)
            persistent_onKill.ForEach(pe => pe());
    }

    protected void FireOnFinish()
    {
        if (nonPersistent_onFinish != null && nonPersistent_onFinish.Count > 0)
        {
            nonPersistent_onFinish.ForEach(pe => pe());
            nonPersistent_onFinish.Clear();
        }

        if (persistent_onFinish != null && persistent_onFinish.Count > 0)
            persistent_onFinish.ForEach(pe => pe());
    }

    protected void FireOnStart()
    {
        if (nonPersistent_onStart != null && nonPersistent_onStart.Count > 0)
        {
            nonPersistent_onStart.ForEach(pe => pe());
            nonPersistent_onStart.Clear();
        }

        if (persistent_onStart != null && persistent_onStart.Count > 0)
            persistent_onStart.ForEach(pe => pe());
    }

    protected void FireOnUpdate()
    {
        if (nonPersistent_onUpdate != null && nonPersistent_onUpdate.Count > 0)
        {
            nonPersistent_onUpdate.ForEach(pe => pe());
            nonPersistent_onUpdate.Clear();
        }

        if (persistent_onUpdate != null && persistent_onUpdate.Count > 0)
            persistent_onUpdate.ForEach(pe => pe());
    }

    protected void FireOnPause()
    {
        if (nonPersistent_onPause != null && nonPersistent_onPause.Count > 0)
        {
            nonPersistent_onPause.ForEach(pe => pe());
            nonPersistent_onPause.Clear();
        }

        if (persistent_onPause != null && persistent_onPause.Count > 0)
            persistent_onPause.ForEach(pe => pe());
    }

    protected void FireOnResume()
    {
        if (nonPersistent_onResume != null && nonPersistent_onResume.Count > 0)
        {
            nonPersistent_onResume.ForEach(pe => pe());
            nonPersistent_onResume.Clear();
        }

        if (persistent_onResume != null && persistent_onResume.Count > 0)
            persistent_onResume.ForEach(pe => pe());
    }

    protected void FireOnHalfWay()
    {
        if (nonPersistent_onHalfWay != null && nonPersistent_onHalfWay.Count > 0)
        {
            nonPersistent_onHalfWay.ForEach(pe => pe());
            nonPersistent_onHalfWay.Clear();
        }

        if (persistent_onHalfWay != null && persistent_onHalfWay.Count > 0)
            persistent_onHalfWay.ForEach(pe => pe());
    }
    #endregion

    #endregion

    void Awake()
    {
        if (Duration < 0f)
        {
            Debug.LogWarning("Tweener duration of " + gameObject.name + "is below 0, setting it to 0.");
            Duration = 0f;
        }

        ResetEventDelegates();

        IsForward = false;
        IsReverse = true;

        _onHalfWayFired = false;

        _directionSign = -1;

        IsPaused = false;
        IsPlaying = false;

        CloseUpdate();
        ResetForFirstEnable();

        Wake();
    }

    void OnEnable()
    {
        if (PlayAutomatically)
            PlayForward();
    }

    public void ResetEventDelegates()
    {
        persistent_onKill = new List<TweenCallback>();
        persistent_onFinish = new List<TweenCallback>();
        persistent_onStart = new List<TweenCallback>();
        persistent_onUpdate = new List<TweenCallback>();
        persistent_onPause = new List<TweenCallback>();
        persistent_onResume = new List<TweenCallback>();
        persistent_onHalfWay = new List<TweenCallback>();

        nonPersistent_onKill = new List<TweenCallback>();
        nonPersistent_onFinish = new List<TweenCallback>();
        nonPersistent_onStart = new List<TweenCallback>();
        nonPersistent_onUpdate = new List<TweenCallback>();
        nonPersistent_onPause = new List<TweenCallback>();
        nonPersistent_onResume = new List<TweenCallback>();
        nonPersistent_onHalfWay = new List<TweenCallback>();
    }

    public void PlayForward()
    {
        if (IsForward)
            return;

        if (IsPlaying || IsPaused)
            CalculateNewCurDuration();

        if (IsPaused)
            ResumeTween();

        Play(true);
    }

    void CalculateNewCurDuration()
    {
        float curTimeRatio = _curDuration / Duration;

        _curDuration = (1 - curTimeRatio) * Duration;
    }

    public void PlayReverse()
    {
        if (IsReverse)
            return;

        if(IsPlaying || IsPaused)
            CalculateNewCurDuration();

        if (IsPaused)
            ResumeTween();

        Play(false);
    }

    public void PauseTween()
    {
        IsPaused = true;
        IsPlaying = false;

        FireOnPause();
    }

    public void ResumeTween()
    {
        IsPaused = false;
        IsPlaying = true;

        FireOnResume();
    }

    void Play(bool forward)
    {
        FireOnStart();

        SetPlayingDirection(forward);

        InitClampedValue(forward);

        _enabled = true;
        _enableTime = Time.realtimeSinceStartup;
    }

    void Update()
    {
        if(_enabled && !IsPaused)
        {
            if(Delay)
            {
                if (_firstEnable && Time.realtimeSinceStartup - _enableTime < DelayDuration)
                    return;
                else
                    _firstEnable = false;
            }

            _clampedValue = GetSample();

            CheckIfClampValueExceeds();

            SetValue(_clampedValue);
            PlayAnim();

            FireOnUpdate();

            CheckIfAnimShouldFinish();

            UpdateCurDuration();
        }
    }

    void UpdateCurDuration()
    {
        _curDuration += _directionSign * (IgnoreTimeScale ? Time.fixedDeltaTime : Time.fixedDeltaTime);

        if (((IsForward && CurDuration > Duration / 2f) || (IsReverse && CurDuration < Duration / 2f)) && !_onHalfWayFired)
        {
            _onHalfWayFired = true;
            FireOnHalfWay();
        }


        if (_curDuration > Duration)
            _curDuration = Duration;

        if (_curDuration < 0f)
            _curDuration = 0f;
    }

    float GetSample()
    {

        float curClampedValue = _clampedValue;
        
        switch(Ease)
        {
            case MMTweeningEaseEnum.Linear:
                curClampedValue = MMTweeningUtilities.Linear(0f, 1f, _curDuration, Duration);
                break;
            case MMTweeningEaseEnum.InCubic:
                curClampedValue = MMTweeningUtilities.EaseInCubic(0f, 1f, _curDuration, Duration);
                break;
            case MMTweeningEaseEnum.OutCubic:
                curClampedValue = MMTweeningUtilities.EaseOutCubic(0f, 1f, _curDuration, Duration);
                break;
            case MMTweeningEaseEnum.InOutCubic:
                curClampedValue = MMTweeningUtilities.EaseInOutCubic(0f, 1f, _curDuration, Duration);
                break;
            case MMTweeningEaseEnum.InSine:
                curClampedValue = MMTweeningUtilities.EaseInSine(0f, 1f, _curDuration, Duration);
                break;
            case MMTweeningEaseEnum.OutSine:
                curClampedValue = MMTweeningUtilities.EaseOutSine(0f, 1f, _curDuration, Duration);
                break;
            case MMTweeningEaseEnum.InOutSine:
                curClampedValue = MMTweeningUtilities.EaseInOutSine(0f, 1f, _curDuration, Duration);
                break;
            case MMTweeningEaseEnum.InExpo:
                curClampedValue = MMTweeningUtilities.EaseInExpo(0f, 1f, _curDuration, Duration);
                break;
            case MMTweeningEaseEnum.OutExpo:
                curClampedValue = MMTweeningUtilities.EaseOutExpo(0f, 1f, _curDuration, Duration);
                break;
            case MMTweeningEaseEnum.InOutExpo:
                curClampedValue = MMTweeningUtilities.EaseInOutExpo(0f, 1f, _curDuration, Duration);
                break;
            case MMTweeningEaseEnum.InOutBack:
                curClampedValue = MMTweeningUtilities.EaseInOutBack(0f, 1f, _curDuration, Duration);
                break;
            case MMTweeningEaseEnum.Curve:
            case MMTweeningEaseEnum.InBack:
            case MMTweeningEaseEnum.OutBack:
                break;
        }

        return curClampedValue;
    }

    void CheckIfClampValueExceeds()
    {
        switch (LoopType)
        {
            case MMTweeningLoopTypeEnum.Once:
                if (_clampedValue >= 1f)
                    _clampedValue = 1f;
                else if (_clampedValue <= 0f)
                    _clampedValue = 0f;
                break;
            case MMTweeningLoopTypeEnum.PingPong:
                if (_clampedValue >= 1f)
                {
                    _clampedValue = 1f;

                    SetPlayingDirection(false);
                }
                else if (_clampedValue <= 0f)
                {
                    _clampedValue = 0f;

                    SetPlayingDirection(true);
                }
                break;
            case MMTweeningLoopTypeEnum.Loop:
                if (_clampedValue >= 1f)
                {
                    _clampedValue = 0f;

                    InitValueToFROM();

                    SetPlayingDirection(true);
                }
                //else if (_clampedValue <= 0f && IsReverse)
                //{
                //    _clampedValue = 1f;

                //    SetPlayingDirection(true);

                //    InitValueToTO();
                //}
                break;
        }
    }

    void SetPlayingDirection(bool forward)
    {
        IsForward = forward;
        IsReverse = !forward;

        if (forward)
            _directionSign = 1;
        else
            _directionSign = -1;

        if((forward && CurDuration < Duration / 2f) || (!forward && CurDuration > Duration / 2f))
            _onHalfWayFired = false;
    }

    void CheckIfAnimShouldFinish()
    {
        if (LoopType == MMTweeningLoopTypeEnum.Once)
        {
            if ((_clampedValue == 0f && IsReverse) || (_clampedValue == 1f && IsForward))
                FinishTween();
        }
    }

    void InitClampedValue(bool forward)
    {
        if (IsPaused)
            return;

        if (forward)
            _clampedValue = 0f;
        else
            _clampedValue = 1f;
    }

    /// <summary>
    /// DIRECTLY KILLS the tween.
    /// </summary>
    public void KillTween()
    {
        IsPaused = false;
        IsPlaying = false;

        CloseUpdate();
        ResetForFirstEnable();

        Kill();

        FireOnKill();
    }

    void CloseUpdate()
    {
        _enabled = false;
    }

    void ResetForFirstEnable()
    {
        _enableTime = 0f;
        _firstEnable = true;
    }

    /// <summary>
    /// Sets the tweening object to from/to value DIRECTLY, depending on the forward/reverse. Then KILLS the tween.
    /// </summary>
    public void FinishTween()
    {
        if (IsForward)
            _clampedValue = 1f;
        else if (IsReverse)
            _clampedValue = 0f;

        SetValue(_clampedValue);
        PlayAnim();

        IsPlaying = false;

        Finish();

        CloseUpdate();
        ResetForFirstEnable();

        FireOnFinish();
    }

    public virtual void InitValueToFROM()
    {
        IsForward = false;
        IsReverse = true;

        _curDuration = 0f;

        PlayAnim();
    }

    public virtual void InitValueToTO()
    {
        IsForward = true;
        IsReverse = false;

        _curDuration = Duration;

        PlayAnim();
    }

    #region Abstract Methods
    protected abstract void Wake();
    protected abstract void SetValue(float clampedValue);
    protected abstract void PlayAnim();
    protected abstract void Finish();
    protected abstract void Kill();
    #endregion
}
