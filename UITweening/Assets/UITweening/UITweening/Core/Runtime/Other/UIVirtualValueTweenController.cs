using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UITweening
{
    public class UIVirtualValueTweenController : MonoBehaviour
    {
        private static UIVirtualValueTweenController instance;
        public static UIVirtualValueTweenController Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<UIVirtualValueTweenController>();

                return instance;
            }
        }

        public List<UIVirtualValueTweener> ActiveTweenerList { get; private set; }

        private List<UIVirtualValueTweener> finishedTweens = new List<UIVirtualValueTweener>();

        private void Awake()
        {
            if (instance == null)
                instance = this;

            if (ActiveTweenerList == null)
                ActiveTweenerList = new List<UIVirtualValueTweener>();

            finishedTweens = new List<UIVirtualValueTweener>();
        }

        private void OnDestroy()
        {
            instance = null;

            ActiveTweenerList = null;
            finishedTweens = null;
        }

        public void StartTweener(UIVirtualValueTweener tween)
        {
            if (ActiveTweenerList == null)
                ActiveTweenerList = new List<UIVirtualValueTweener>();

            tween.Play();

            ActiveTweenerList.Add(tween);
        }

        public void StopTweener(UIVirtualValueTweener tween)
        {
            tween.Stop();

            finishedTweens.Add(tween);
        }

        private void Update()
        {
            foreach (var tween in ActiveTweenerList.ToList())
            {
                var clampedValue = UITweeningUtilities.GetSample(
                    tween.CurDuration,
                    tween.TweenInfo.Duration,
                    tween.TweenInfo.Ease);

                tween.UpdateValue(clampedValue);

                if (clampedValue == 1 && !finishedTweens.Contains(tween))
                    finishedTweens.Add(tween);
            }
        }

        private void LateUpdate()
        {
            if (finishedTweens.Count == 0)
                return;

            foreach (var finishedTween in finishedTweens)
                ActiveTweenerList.Remove(finishedTween);

            finishedTweens.Clear();
        }
    }
}
