using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UITweening
{
    public enum UITweeningEaseEnum
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

    public enum UITweeningLoopTypeEnum
    {
        Once,
        Loop,
        PingPong,
    }

    public enum UITweeningDirection
    {
        Forward = 1,
        Reverse = -1
    }

    public abstract class UITweener : MonoBehaviour
    {
        #region CommonVariables
        [HideInInspector] public UITweeningEaseEnum Ease;
        [HideInInspector] public UITweeningLoopTypeEnum LoopType;
        [HideInInspector] public bool InitOnAwake;
        [HideInInspector] public AnimationCurve CustomAnimationCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));
        [HideInInspector] public AnimationCurve PunchAnimationCurve = new AnimationCurve(new Keyframe(0.0f, 0.0f), new Keyframe(0.112586f, 0.9976035f), new Keyframe(0.3120486f, -0.1720615f), new Keyframe(0.4316337f, 0.07030682f), new Keyframe(0.5524869f, -0.03141804f), new Keyframe(0.6549395f, 0.003909959f), new Keyframe(0.770987f, -0.009817753f), new Keyframe(0.8838775f, 0.001939224f), new Keyframe(1.0f, 0.0f));
        [HideInInspector] public AnimationCurve ShakeAnimationCurve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(0.25f, 1f), new Keyframe(0.75f, -1f), new Keyframe(1f, 0f));
        [HideInInspector] public bool PlayAutomatically;
        [HideInInspector] public bool Delay;
        [HideInInspector] public bool CanPlayForward;
        [HideInInspector] public bool CanPlayReverse;
        [HideInInspector] public bool IsPaused = false;
        [HideInInspector] public bool IsPlaying = false;
        [HideInInspector] public float DelayDuration;
        [HideInInspector] public bool IgnoreTimeScale = true;
        [HideInInspector] public float Duration;
        [HideInInspector] public float ShakePunchAmount;
        [HideInInspector] public Vector3 ShakePunchDirection;

        public UITweeningDirection Direction { get; private set; }
        public float CurDuration { get; private set; }

        private float _clampedValue, _minClampedValue, _maxClampedValue, _enableTime = 0f;
        private bool _firstEnable = true, _enabled = false, _onHalfWayFired = false;
        #endregion

        #region Events
        public delegate void TweenCallback();

        private List<TweenCallback> persistent_onKill = new List<TweenCallback>(), persistent_onFinish = new List<TweenCallback>(),
            persistent_onStart = new List<TweenCallback>(), persistent_onUpdate = new List<TweenCallback>(),
            persistent_onPause = new List<TweenCallback>(), persistent_onResume = new List<TweenCallback>(), persistent_onHalfWay = new List<TweenCallback>();

        private List<TweenCallback> nonPersistent_onKill = new List<TweenCallback>(), nonPersistent_onFinish = new List<TweenCallback>(),
            nonPersistent_onStart = new List<TweenCallback>(), nonPersistent_onUpdate = new List<TweenCallback>(),
            nonPersistent_onPause = new List<TweenCallback>(), nonPersistent_onResume = new List<TweenCallback>(), nonPersistent_onHalfWay = new List<TweenCallback>();

        #region Callback Functions
        /// <summary>
        /// If you add non persistent callback, it will be unsubscribed as soon as it is called
        /// </summary>
        /// <param name="callback">Callback to call</param>
        /// <param name="isPersistent"></param>
        /// <returns></returns>
        public UITweener AddOnKill(TweenCallback callback, bool isPersistent = true)
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
        public UITweener AddOnFinish(TweenCallback callback, bool isPersistent = true)
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
        public UITweener AddOnStart(TweenCallback callback, bool isPersistent = true)
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
        public UITweener AddOnUpdate(TweenCallback callback, bool isPersistent = true)
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
        public UITweener AddOnPause(TweenCallback callback, bool isPersistent = true)
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
        public UITweener AddOnResume(TweenCallback callback, bool isPersistent = true)
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
        public UITweener AddOnHalfWay(TweenCallback callback, bool isPersistent = true)
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
            List<TweenCallback> cache = new List<TweenCallback>(nonPersistent_onKill);
            if (cache.Count > 0)
            {
                nonPersistent_onKill.Clear();

                cache.ForEach(pe => pe());
            }

            if (persistent_onKill != null && persistent_onKill.Count > 0)
                persistent_onKill.ForEach(pe => pe());
        }

        protected void FireOnFinish()
        {
            List<TweenCallback> cache = new List<TweenCallback>(nonPersistent_onFinish);
            if (cache.Count > 0)
            {
                nonPersistent_onFinish.Clear();

                cache.ForEach(pe => pe());
            }

            if (persistent_onFinish != null && persistent_onFinish.Count > 0)
                persistent_onFinish.ForEach(pe => pe());
        }

        protected void FireOnStart()
        {
            List<TweenCallback> cache = new List<TweenCallback>(nonPersistent_onStart);
            if (cache.Count > 0)
            {
                nonPersistent_onStart.Clear();

                cache.ForEach(pe => pe());
            }

            if (persistent_onStart != null && persistent_onStart.Count > 0)
                persistent_onStart.ForEach(pe => pe());
        }

        protected void FireOnUpdate()
        {
            List<TweenCallback> cache = new List<TweenCallback>(nonPersistent_onUpdate);
            if (cache.Count > 0)
            {
                nonPersistent_onUpdate.Clear();

                cache.ForEach(pe => pe());
            }

            if (persistent_onUpdate != null && persistent_onUpdate.Count > 0)
                persistent_onUpdate.ForEach(pe => pe());
        }

        protected void FireOnPause()
        {
            List<TweenCallback> cache = new List<TweenCallback>(nonPersistent_onPause);
            if (cache.Count > 0)
            {
                nonPersistent_onPause.Clear();

                cache.ForEach(pe => pe());
            }

            if (persistent_onPause != null && persistent_onPause.Count > 0)
                persistent_onPause.ForEach(pe => pe());
        }

        protected void FireOnResume()
        {
            List<TweenCallback> cache = new List<TweenCallback>(nonPersistent_onResume);
            if (cache.Count > 0)
            {
                nonPersistent_onResume.Clear();

                cache.ForEach(pe => pe());
            }

            if (persistent_onResume != null && persistent_onResume.Count > 0)
                persistent_onResume.ForEach(pe => pe());
        }

        protected void FireOnHalfWay()
        {
            List<TweenCallback> cache = new List<TweenCallback>(nonPersistent_onHalfWay);
            if (cache.Count > 0)
            {
                nonPersistent_onHalfWay.Clear();

                cache.ForEach(pe => pe());
            }

            if (persistent_onHalfWay != null && persistent_onHalfWay.Count > 0)
                persistent_onHalfWay.ForEach(pe => pe());
        }
        #endregion

        #endregion

        private void Awake()
        {
            if (Duration <= 0f)
            {
                Debug.LogWarning("Tweener duration of " + gameObject.name + " is equal or below 0, setting it to 0.");
                SetDuration(0f);
            }

            Wake();
        }

        private void OnEnable()
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
                AnimationCurve animCurve = null;
                if (Ease.Equals(UITweeningEaseEnum.Shake))
                    animCurve = ShakeAnimationCurve;
                else if (Ease.Equals(UITweeningEaseEnum.Curve))
                    animCurve = CustomAnimationCurve;

                clampValues.Add(UITweeningUtilities.GetSample(i, Duration, Ease, animCurve));

                i += 0.02f;
            }

            clampValues = clampValues.OrderBy(v => v).ToList();

            _minClampedValue = clampValues[0];
            _maxClampedValue = clampValues[clampValues.Count - 1];
        }

        public void SetEase(UITweeningEaseEnum easeType)
        {
            if ((/*easeType == UITweeningEaseEnum.Punch || */easeType == UITweeningEaseEnum.Shake)
                && (GetType() == typeof(UITweenColor) || GetType() == typeof(UITweenAlpha)))
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

        private void Play(bool forward)
        {
            FireOnStart();

            SetPlayingDirection(forward);

            InitClampedValue(forward);

            _enabled = true;
            _enableTime = Time.realtimeSinceStartup;

            IsPlaying = true;
            IsPaused = false;
        }

        private void Update()
        {
            if (_enabled && !IsPaused)
            {
                if (Delay)
                {
                    if (_firstEnable && Time.realtimeSinceStartup - _enableTime < DelayDuration)
                        return;
                    else
                        _firstEnable = false;
                }

                AnimationCurve animCurve = null;
                if (Ease.Equals(UITweeningEaseEnum.Shake))
                    animCurve = ShakeAnimationCurve;
                else if (Ease.Equals(UITweeningEaseEnum.Curve))
                    animCurve = CustomAnimationCurve;

                _clampedValue = UITweeningUtilities.GetSample(CurDuration, Duration, Ease, animCurve);

                SetValue(_clampedValue);
                PlayAnim();

                FireOnUpdate();

                if (CheckIfAnimShouldFinish())
                    return;

                CheckForDirectionChange();

                StepCurDuration();
            }
        }

        private bool CheckIfAnimShouldFinish()
        {
            switch (LoopType)
            {
                case UITweeningLoopTypeEnum.Once:
                    if ((CurDuration == 0f && Direction == UITweeningDirection.Reverse) || (CurDuration == Duration && Direction == UITweeningDirection.Forward))
                    {
                        FinishTween();

                        return true;
                    }
                    break;
                case UITweeningLoopTypeEnum.PingPong:
                case UITweeningLoopTypeEnum.Loop:
                    break;
            }

            return false;
        }

        private void CheckForDirectionChange()
        {
            switch (LoopType)
            {
                case UITweeningLoopTypeEnum.Once:
                    break;
                case UITweeningLoopTypeEnum.PingPong:
                    if (CurDuration >= Duration)
                    {
                        _clampedValue = 1f;

                        SetPlayingDirection(false);
                    }
                    else if (CurDuration <= 0f)
                    {
                        _clampedValue = 0f;

                        SetPlayingDirection(true);
                    }
                    break;
                case UITweeningLoopTypeEnum.Loop:
                    if (CurDuration >= Duration && Direction == UITweeningDirection.Forward)
                    {
                        InitValueToFROM();

                        SetPlayingDirection(true);
                    }
                    else if (CurDuration <= 0f && Direction == UITweeningDirection.Reverse)
                    {
                        InitValueToTO();

                        SetPlayingDirection(false);
                    }
                    break;
            }
        }

        private void StepCurDuration()
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



        private void SetCanPlayDir(bool canPlayForward, bool canPlayReverse)
        {
            CanPlayForward = canPlayForward;
            CanPlayReverse = canPlayReverse;
        }

        private void SetPlayingDirection(bool forward)
        {
            SetCanPlayDir(!forward, forward);

            if (forward)
                Direction = UITweeningDirection.Forward;
            else
                Direction = UITweeningDirection.Reverse;

            if ((forward && CurDuration < Duration / 2f) || (!forward && CurDuration > Duration / 2f))
                _onHalfWayFired = false;
        }

        private void InitClampedValue(bool forward)
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

        private void CloseUpdate()
        {
            _enabled = false;
        }

        private void ResetForFirstEnable()
        {
            _enableTime = 0f;
            _firstEnable = true;
        }

        /// <summary>
        /// Sets the tweening object to FROM/TO value DIRECTLY, depending on the forward/reverse. Then KILLS the tween.
        /// </summary>
        public void FinishTween()
        {
            if (Direction == UITweeningDirection.Forward)
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

            Direction = UITweeningDirection.Forward;

            CurDuration = 0f;
            _clampedValue = 0f;

            SetValue(_clampedValue);
            PlayAnim();
        }

        public virtual void InitValueToTO()
        {
            SetCanPlayDir(false, true);

            Direction = UITweeningDirection.Reverse;

            CurDuration = Duration;
            _clampedValue = 1f;

            SetValue(_clampedValue);
            PlayAnim();
        }

        protected virtual void Wake()
        {
            if (InitOnAwake)
                InitValueToFROM();
        }

        #region Abstract Methods
        protected abstract void SetValue(float clampedValue);
        protected abstract void PlayAnim();
        protected abstract void Finish();
        protected abstract void Kill();
        #endregion
    }

}