using System.Collections.Generic;
using UnityEngine;

public class UIVirtualValueTweenController : MonoBehaviour
{
    private static UIVirtualValueTweenController _instance;
    public static UIVirtualValueTweenController Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<UIVirtualValueTweenController>();

            return _instance;
        }
    }

    public List<UIVirtualValueTweener> ActiveTweenerList { get; private set; }

    private List<UIVirtualValueTweener> _finishedTweens = new List<UIVirtualValueTweener>();

    private void Awake()
    {
        if (_instance == null)
            _instance = this;

        if (ActiveTweenerList == null)
            ActiveTweenerList = new List<UIVirtualValueTweener>();

        _finishedTweens = new List<UIVirtualValueTweener>();
    }

    private void OnDestroy()
    {
        _instance = null;

        ActiveTweenerList = null;
        _finishedTweens = null;
    }

    public void StartTweener(UIVirtualValueTweener tween)
    {
        if (ActiveTweenerList == null)
            ActiveTweenerList = new List<UIVirtualValueTweener>();

        ActiveTweenerList.Add(tween);
    }

    public void StopTweener(UIVirtualValueTweener tween)
    {
        _finishedTweens.Add(tween);
    }

    private void Update()
    {
        foreach (var tween in ActiveTweenerList)
        {
            var clampedValue = UITweeningUtilities.GetSample(
                tween.CurDuration,
                tween.TweenInfo.Duration, 
                tween.TweenInfo.Ease);

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
}
