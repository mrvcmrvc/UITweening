using System;
using System.Collections.Generic;
using System.Linq;
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
    InOutBack,
    InQuintic,
    OutQuintic,
    InOutQuintic,
    InQuartic,
    OutQuartic,
    InOutQuartic,
    InQuadratic,
    OutQuadratic,
    InOutQuadratic,
    InCircular,
    OutCircular,
    InOutCircular,
    InElastic,
    OutElastic,
    InOutElastic,
    InBounce,
    OutBounce,
    InOutBounce,
    //Punch,
    Shake
}

public enum MMTweeningLoopTypeEnum
{
    Once,
    Loop,
    PingPong,
} 

public enum MMTweeningDirection
{
    Forward = 1,
    Reverse = -1
}

public abstract class MMUITweener : MonoBehaviour
{
    #region CommonVariables
    [HideInInspector]
    public MMTweeningEaseEnum Ease;
    [HideInInspector]
    public MMTweeningLoopTypeEnum LoopType;
    [HideInInspector]
    public AnimationCurve CustomAnimationCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));
    [HideInInspector]
    public AnimationCurve PunchAnimationCurve = new AnimationCurve(new Keyframe(0.0f, 0.0f), new Keyframe(0.112586f, 0.9976035f), new Keyframe(0.3120486f, -0.1720615f), new Keyframe(0.4316337f, 0.07030682f), new Keyframe(0.5524869f, -0.03141804f), new Keyframe(0.6549395f, 0.003909959f), new Keyframe(0.770987f, -0.009817753f), new Keyframe(0.8838775f, 0.001939224f), new Keyframe(1.0f, 0.0f));
    [HideInInspector]
    public AnimationCurve ShakeAnimationCurve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(0.25f, 1f), new Keyframe(0.75f, -1f), new Keyframe(1f, 0f));
    [HideInInspector]
    public bool PlayAutomatically;
    [HideInInspector]
    public bool Delay;
    [HideInInspector]
    public bool CanPlayForward;
    [HideInInspector]
    public bool CanPlayReverse;
    [HideInInspector]
    public bool IsPaused;
    [HideInInspector]
    public bool IsPlaying;
    [HideInInspector]
    public float DelayDuration;
    [HideInInspector]
    public bool IgnoreTimeScale = true;
    [HideInInspector]
    public float Duration;
    [HideInInspector]
    public float ShakePunchAmount;
    [HideInInspector]
    public Vector3 ShakePunchDirection;
    
    public MMTweeningDirection Direction { get; private set; }
    public float CurDuration { get; private set; }

    float _clampedValue, _minClampedValue, _maxClampedValue, _enableTime;
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
        if (Duration <= 0f)
        {
            Debug.LogWarning("Tweener duration of " + gameObject.name + " is equal or below 0, setting it to 0.");
            SetDuration(0f);
        }

        ResetEventDelegates();

        _onHalfWayFired = false;

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

    //TODO: Bu kısım yeniden tasarlanmalı, min ve max bi algoritma ile bulunmalı
    public void SetDuration(float newDuration)
    {
        Duration = newDuration;

        List<float> clampValues = new List<float>();

        for (float i = 0f; i <= Duration;)
        {
            clampValues.Add(GetSample(i, Duration));

            i += 0.02f;
        }

        clampValues = clampValues.OrderBy(v => v).ToList();

        _minClampedValue = clampValues[0];
        _maxClampedValue = clampValues[clampValues.Count - 1];
    }

    public void SetEase(MMTweeningEaseEnum easeType)
    {
        if ((/*easeType == MMTweeningEaseEnum.Punch || */easeType == MMTweeningEaseEnum.Shake)
            && (GetType() == typeof(MMTweenColor) || GetType() == typeof(MMTweenAlpha)))
            return;

        Ease = easeType;

        SetDuration(Duration);
    }

    public void SetAnimationCurve(AnimationCurve curve)
    {
        CustomAnimationCurve = curve;

        SetDuration(Duration);
    }

    public void PlayForward()
    {
        if (!CanPlayForward)
            return;

        if (IsPaused)
            ResumeTween();

        Play(true);
    }

    public void PlayReverse()
    {
        if (!CanPlayReverse)
            return;

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

        IsPlaying = true;
        IsPaused = false;
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

            _clampedValue = GetSample(CurDuration, Duration);

            SetValue(_clampedValue);
            PlayAnim();

            FireOnUpdate();
            
            if (CheckIfAnimShouldFinish())
                return;

            CheckForDirectionChange();

            StepCurDuration();
        }
    }

    bool CheckIfAnimShouldFinish()
    {
        switch (LoopType)
        {
            case MMTweeningLoopTypeEnum.Once:
                if ((CurDuration == 0f && Direction == MMTweeningDirection.Reverse) || (CurDuration == Duration && Direction == MMTweeningDirection.Forward))
                {
                    FinishTween();

                    return true;
                }
                break;
            case MMTweeningLoopTypeEnum.PingPong:
            case MMTweeningLoopTypeEnum.Loop:
                break;
        }

        return false;
    }


    private void CheckForDirectionChange()
    {
        switch (LoopType)
        {
            case MMTweeningLoopTypeEnum.Once:
                break;
            case MMTweeningLoopTypeEnum.PingPong:
                if (CurDuration >= Duration)
                {
                    _clampedValue = 1f;

                    SetPlayingDirection(false);
                }
                else if (CurDuration <=  0f)
                {
                    _clampedValue = 0f;

                    SetPlayingDirection(true);
                }
                break;
            case MMTweeningLoopTypeEnum.Loop:
                if (CurDuration >= Duration && Direction == MMTweeningDirection.Forward)
                {
                    InitValueToFROM();

                    SetPlayingDirection(true);
                }
                else if (CurDuration <= 0f && Direction == MMTweeningDirection.Reverse)
                {
                    InitValueToTO();

                    SetPlayingDirection(false);
                }
                break;
        }
    }

    void StepCurDuration()
    {
        CurDuration += (int)Direction * (IgnoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime);

        if (((CanPlayReverse && CurDuration > Duration / 2f) || (CanPlayForward && CurDuration < Duration / 2f)) && !_onHalfWayFired)
        {
            _onHalfWayFired = true;
            FireOnHalfWay();
        }

        if (CurDuration > Duration)
            CurDuration = Duration;

        if (CurDuration < 0f)
            CurDuration = 0f;
    }

    float GetSample(float curDuration, float duration)
    {
        float curClampedValue = 0f;

        switch(Ease)
        {
            case MMTweeningEaseEnum.Shake:
                curClampedValue = MMTweeningUtilities.AnimationCurve(ShakeAnimationCurve, curDuration, duration);
                break;
            //case MMTweeningEaseEnum.Punch:
            //    curClampedValue = MMTweeningUtilities.AnimationCurve(PunchAnimationCurve, curDuration, duration);
            //    break;
            case MMTweeningEaseEnum.Curve:
                curClampedValue = MMTweeningUtilities.AnimationCurve(CustomAnimationCurve, curDuration, duration);
                break;
            case MMTweeningEaseEnum.Linear:
                curClampedValue = MMTweeningUtilities.Linear(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InCubic:
                curClampedValue = MMTweeningUtilities.EaseInCubic(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.OutCubic:
                curClampedValue = MMTweeningUtilities.EaseOutCubic(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InOutCubic:
                curClampedValue = MMTweeningUtilities.EaseInOutCubic(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InSine:
                curClampedValue = MMTweeningUtilities.EaseInSine(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.OutSine:
                curClampedValue = MMTweeningUtilities.EaseOutSine(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InOutSine:
                curClampedValue = MMTweeningUtilities.EaseInOutSine(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InExpo:
                curClampedValue = MMTweeningUtilities.EaseInExpo(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.OutExpo:
                curClampedValue = MMTweeningUtilities.EaseOutExpo(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InOutExpo:
                curClampedValue = MMTweeningUtilities.EaseInOutExpo(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InBack:
                curClampedValue = MMTweeningUtilities.EaseInBack(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.OutBack:
                curClampedValue = MMTweeningUtilities.EaseOutBack(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InOutBack:
                curClampedValue = MMTweeningUtilities.EaseInOutBack(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InQuintic:
                curClampedValue = MMTweeningUtilities.EaseInQuint(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.OutQuintic:
                curClampedValue = MMTweeningUtilities.EaseOutQuint(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InOutQuintic:
                curClampedValue = MMTweeningUtilities.EaseInOutQuint(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InQuartic:
                curClampedValue = MMTweeningUtilities.EaseInQuartic(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.OutQuartic:
                curClampedValue = MMTweeningUtilities.EaseOutQuartic(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InOutQuartic:
                curClampedValue = MMTweeningUtilities.EaseInOutQuartic(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InQuadratic:
                curClampedValue = MMTweeningUtilities.EaseInQuadratic(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.OutQuadratic:
                curClampedValue = MMTweeningUtilities.EaseOutQuadratic(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InOutQuadratic:
                curClampedValue = MMTweeningUtilities.EaseInOutQuadratic(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InCircular:
                curClampedValue = MMTweeningUtilities.EaseInCircular(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.OutCircular:
                curClampedValue = MMTweeningUtilities.EaseOutCircular(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InOutCircular:
                curClampedValue = MMTweeningUtilities.EaseInOutCircular(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InElastic:
                curClampedValue = MMTweeningUtilities.EaseInElastic(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.OutElastic:
                curClampedValue = MMTweeningUtilities.EaseOutElastic(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InOutElastic:
                curClampedValue = MMTweeningUtilities.EaseInOutElastic(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InBounce:
                curClampedValue = MMTweeningUtilities.EaseInBounce(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.OutBounce:
                curClampedValue = MMTweeningUtilities.EaseOutBounce(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InOutBounce:
                curClampedValue = MMTweeningUtilities.EaseInOutBounce(0f, 1f, curDuration, duration);
                break;
        }

        return curClampedValue;
    }

    void CheckIfClampValueExceeds()
    {
        switch (LoopType)
        {
            case MMTweeningLoopTypeEnum.Once:
                if (_clampedValue >= _maxClampedValue)
                    _clampedValue = _maxClampedValue;
                else if (_clampedValue <= _minClampedValue)
                    _clampedValue = _minClampedValue;
                break;
            case MMTweeningLoopTypeEnum.PingPong:
                if (_clampedValue >= _maxClampedValue)
                {
                    _clampedValue = _maxClampedValue;

                    SetPlayingDirection(false);
                }
                else if (_clampedValue <= _minClampedValue)
                {
                    _clampedValue = _minClampedValue;

                    SetPlayingDirection(true);
                }
                break;
            case MMTweeningLoopTypeEnum.Loop:
                if (_clampedValue >= _maxClampedValue && Direction == MMTweeningDirection.Forward)
                {
                    InitValueToFROM();

                    SetPlayingDirection(true);
                }
                else if(_clampedValue <= _minClampedValue && Direction == MMTweeningDirection.Reverse)
                {
                    InitValueToTO();

                    SetPlayingDirection(false);
                }
                break;
        }
    }

    void SetCanPlayDir(bool canPlayForward, bool canPlayReverse)
    {
        CanPlayForward = canPlayForward;
        CanPlayReverse = canPlayReverse;
    }

    void SetPlayingDirection(bool forward)
    {
        SetCanPlayDir(!forward, forward);

        if (forward)
            Direction =  MMTweeningDirection.Forward;
        else
            Direction = MMTweeningDirection.Reverse;

        if((forward && CurDuration < Duration / 2f) || (!forward && CurDuration > Duration / 2f))
            _onHalfWayFired = false;
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
    /// Sets the tweening object to FROM/TO value DIRECTLY, depending on the forward/reverse. Then KILLS the tween.
    /// </summary>
    public void FinishTween()
    {
        if (Direction == MMTweeningDirection.Forward)
            InitValueToTO();
        else
            InitValueToFROM();

        IsPlaying = false;

        Finish();

        CloseUpdate();
        ResetForFirstEnable();

        FireOnFinish();
    }

    public virtual void InitValueToFROM()
    {
        SetCanPlayDir(true, false);

        Direction = MMTweeningDirection.Forward;

        CurDuration = 0f;
        _clampedValue = 0f;

        SetValue(_clampedValue);
        PlayAnim();
    }

    public virtual void InitValueToTO()
    {
        SetCanPlayDir(false, true);

        Direction = MMTweeningDirection.Reverse;

        CurDuration = Duration;
        _clampedValue = 1f;

        SetValue(_clampedValue);
        PlayAnim();
    }

    protected virtual void Wake()
    {
        InitValueToFROM();
    }

    #region Abstract Methods
    protected abstract void SetValue(float clampedValue);
    protected abstract void PlayAnim();
    protected abstract void Finish();
    protected abstract void Kill();
    #endregion
}
