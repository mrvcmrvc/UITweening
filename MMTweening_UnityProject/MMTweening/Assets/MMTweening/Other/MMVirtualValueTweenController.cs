using System;
using System.Collections.Generic;
using UnityEngine;

public class MMVirtualValueTweenController : MonoBehaviour
{
    static MMVirtualValueTweenController _instance;
    public static MMVirtualValueTweenController Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<MMVirtualValueTweenController>();

            return _instance;
        }
    }

    public List<MMVirtualValueTweener> ActiveTweenerList { get; private set; }

    List<MMVirtualValueTweener> _finishedTweens = new List<MMVirtualValueTweener>();

    private void Awake()
    {
        if (_instance == null)
            _instance = this;

        if (ActiveTweenerList == null)
            ActiveTweenerList = new List<MMVirtualValueTweener>();

        _finishedTweens = new List<MMVirtualValueTweener>();
    }

    private void OnDestroy()
    {
        _instance = null;

        ActiveTweenerList = null;
        _finishedTweens = null;
    }

    public void StartTweener(MMVirtualValueTweener tween)
    {
        if (ActiveTweenerList == null)
            ActiveTweenerList = new List<MMVirtualValueTweener>();

        ActiveTweenerList.Add(tween);
    }

    public void StopTweener(MMVirtualValueTweener tween)
    {
        _finishedTweens.Add(tween);
    }

    private void Update()
    {
        foreach (var tween in ActiveTweenerList)
        {
            var clampedValue = GetSample(tween.CurDuration, tween.TweenInfo.Duration, tween.TweenInfo.Ease);

            tween.UpdateValue(clampedValue);

            if (clampedValue == 1 && !_finishedTweens.Contains(tween))
                _finishedTweens.Add(tween);
        }
    }

    private void LateUpdate()
    {
        if (_finishedTweens.Count == 0)
            return;

        foreach (var finishedTween in _finishedTweens)
            ActiveTweenerList.Remove(finishedTween);

        _finishedTweens.Clear();
    }

    float GetSample(float curDuration, float duration, MMTweeningEaseEnum ease)
    {
        float curClampedValue = 0f;

        switch (ease)
        {
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

        if (curClampedValue > 1f)
            curClampedValue = 1f;

        return curClampedValue;
    }
}
