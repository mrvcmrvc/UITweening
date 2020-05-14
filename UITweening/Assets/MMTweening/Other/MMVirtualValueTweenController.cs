using System.Collections.Generic;
using UnityEngine;

public class MMVirtualValueTweenController : MonoBehaviour
{
    private static MMVirtualValueTweenController _instance;
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

    private List<MMVirtualValueTweener> _finishedTweens = new List<MMVirtualValueTweener>();

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
            var clampedValue = MMTweeningUtilities.GetSample(
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
