using System;
using UnityEngine;

namespace UITweening
{
    public class UIVirtualValueTweenerInfo
    {
        public float From;
        public float To;
        public float Duration;
        public bool IgnoreTimeScale = true;
        public UITweeningEaseEnum Ease = UITweeningEaseEnum.Linear;
        public Action<float, bool> Callback;
    }

    public class UIVirtualValueTweener
    {
        public UIVirtualValueTweenerInfo TweenInfo { get; private set; }
        public bool IsPlaying { get; private set; }
        public float CurDuration { get; private set; }
        public float Value { get; private set; }

        public UIVirtualValueTweener(UIVirtualValueTweenerInfo tweenInfo)
        {
            TweenInfo = tweenInfo;
        }

        public void Play()
        {
            IsPlaying = true;
        }

        public void Stop()
        {
            IsPlaying = false;
        }

        public void UpdateValue(float clampedValue)
        {
            if (!IsPlaying)
                return;

            CurDuration += TweenInfo.IgnoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;

            float diff = TweenInfo.To - TweenInfo.From;
            float delta = diff * clampedValue;

            Value = TweenInfo.From + delta;

            if (TweenInfo.Callback == null)
                Debug.LogWarning("Callback did not set");
            else
                TweenInfo.Callback(Value, clampedValue == 1);
        }
    }
}
