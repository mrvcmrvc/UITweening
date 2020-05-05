using UnityEngine;

public class MMTweeningUtilities
{
    public static float GetSample(float curDuration, float duration, MMTweeningEaseEnum ease, AnimationCurve animationCurve = null)
    {
        float curClampedValue = 0f;

        switch (ease)
        {
            case MMTweeningEaseEnum.Shake:
                curClampedValue = AnimationCurve(animationCurve, curDuration, duration);
                break;
            //case MMTweeningEaseEnum.Punch:
            //    curClampedValue = AnimationCurve(PunchAnimationCurve, curDuration, duration);
            //    break;
            case MMTweeningEaseEnum.Curve:
                curClampedValue = AnimationCurve(animationCurve, curDuration, duration);
                break;
            case MMTweeningEaseEnum.Linear:
                curClampedValue = Linear(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InCubic:
                curClampedValue = EaseInCubic(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.OutCubic:
                curClampedValue = EaseOutCubic(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InOutCubic:
                curClampedValue = EaseInOutCubic(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InSine:
                curClampedValue = EaseInSine(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.OutSine:
                curClampedValue = EaseOutSine(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InOutSine:
                curClampedValue = EaseInOutSine(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InExpo:
                curClampedValue = EaseInExpo(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.OutExpo:
                curClampedValue = EaseOutExpo(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InOutExpo:
                curClampedValue = EaseInOutExpo(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InBack:
                curClampedValue = EaseInBack(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.OutBack:
                curClampedValue = EaseOutBack(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InOutBack:
                curClampedValue = EaseInOutBack(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InQuintic:
                curClampedValue = EaseInQuint(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.OutQuintic:
                curClampedValue = EaseOutQuint(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InOutQuintic:
                curClampedValue = EaseInOutQuint(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InQuartic:
                curClampedValue = EaseInQuartic(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.OutQuartic:
                curClampedValue = EaseOutQuartic(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InOutQuartic:
                curClampedValue = EaseInOutQuartic(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InQuadratic:
                curClampedValue = EaseInQuadratic(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.OutQuadratic:
                curClampedValue = EaseOutQuadratic(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InOutQuadratic:
                curClampedValue = EaseInOutQuadratic(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InCircular:
                curClampedValue = EaseInCircular(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.OutCircular:
                curClampedValue = EaseOutCircular(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InOutCircular:
                curClampedValue = EaseInOutCircular(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InElastic:
                curClampedValue = EaseInElastic(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.OutElastic:
                curClampedValue = EaseOutElastic(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InOutElastic:
                curClampedValue = EaseInOutElastic(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InBounce:
                curClampedValue = EaseInBounce(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.OutBounce:
                curClampedValue = EaseOutBounce(0f, 1f, curDuration, duration);
                break;
            case MMTweeningEaseEnum.InOutBounce:
                curClampedValue = EaseInOutBounce(0f, 1f, curDuration, duration);
                break;
        }

        return curClampedValue;
    }

    #region Animation Curve
    public static float AnimationCurve(AnimationCurve curve, float curTime, float duration)
    {
        if (duration == 0.0f)
            return curve.Evaluate(0f);

        curTime /= (duration / curve.keys[curve.length - 1].time);

        return curve.Evaluate(curTime);
    } 
    #endregion

    #region Linear
    public static float Linear(float start, float end, float curTime, float duration)
    {
        curTime /= duration;

        return end * curTime + start;
    } 
    #endregion

    #region Cubic
    public static float EaseInCubic(float start, float end, float curTime, float duration)
    {
        curTime /= duration;

        return end * curTime * curTime * curTime + start;
    }

    public static float EaseOutCubic(float start, float end, float curTime, float duration)
    {
        curTime = curTime / duration - 1;

        return end * (curTime * curTime * curTime + 1) + start;
    }

    public static Vector3 EaseOutCubic(Vector3 start, Vector3 end, float curTime, float duration)
    {
        curTime = curTime / duration - 1;

        return end * (curTime * curTime * curTime + 1) + start;
    }

    public static float EaseInOutCubic(float start, float end, float curTime, float duration)
    {
        curTime = curTime / duration - 1;

        return end * (curTime * curTime * curTime + 1) + start;
    }

    public static Vector3 EaseInOutCubic(Vector3 start, Vector3 end, float curTime, float duration)
    {
        curTime = curTime / duration - 1;

        return end * (curTime * curTime * curTime + 1) + start;
    }
    #endregion

    #region Sine
    public static float EaseInSine(float start, float end, float curTime, float duration)
    {
        return -end * Mathf.Cos(curTime / duration / 1 * (Mathf.PI / 2)) + end + start;
    }

    public static float EaseOutSine(float start, float end, float curTime, float duration)
    {
        return end * Mathf.Sin(curTime / duration / 1 * (Mathf.PI / 2)) + start;
    }

    public static float EaseInOutSine(float start, float end, float curTime, float duration)
    {
        return -end / 2 * (Mathf.Cos(Mathf.PI * curTime / duration / 1) - 1) + start;
    }
    #endregion

    #region Expo
    public static float EaseInExpo(float start, float end, float curTime, float duration)
    {
        return end * Mathf.Pow(2, 10 * (curTime / duration / 1 - 1)) + start;
    }

    public static float EaseOutExpo(float start, float end, float curTime, float duration)
    {
        return end * (-Mathf.Pow(2, -10 * curTime / duration / 1) + 1) + start;
    }

    public static float EaseInOutExpo(float start, float end, float curTime, float duration)
    {
        curTime /= .5f;

        if (curTime < 1)
            return end / 2 * Mathf.Pow(2, 10 * (curTime / duration - 1)) + start;

        curTime--;

        return end / 2 * (-Mathf.Pow(2, -10 * curTime / duration) + 2) + start;
    }
    #endregion

    #region Quintic
    public static float EaseInQuint(float start, float end, float curTime, float duration)
    {
        curTime = (curTime > duration) ? 1.0f : curTime / duration;
        return end * curTime * curTime * curTime * curTime * curTime + start;
    }

    public static float EaseOutQuint(float start, float end, float curTime, float duration)
    {
        curTime = (curTime > duration) ? 1.0f : curTime / duration;
        curTime--;
        return end * (curTime * curTime * curTime * curTime * curTime + 1) + start;
    }

    public static float EaseInOutQuint(float start, float end, float curTime, float duration)
    {
        curTime = (curTime > duration) ? 2.0f : curTime / (duration / 2);

        if (curTime < 1)
            return end / 2 * curTime * curTime * curTime * curTime * curTime + start;

        curTime -= 2;
        return end / 2 * (curTime * curTime * curTime * curTime * curTime + 2) + start;
    }
    #endregion

    #region Quartic
    public static float EaseInQuartic(float start, float end, float curTime, float duration)
    {
        curTime = (curTime > duration) ? 1.0f : curTime / duration;

        return end * curTime * curTime * curTime * curTime + start;
    }

    public static float EaseOutQuartic(float start, float end, float curTime, float duration)
    {
        curTime = (curTime > duration) ? 1.0f : curTime / duration;
        curTime--;

        return -end * (curTime * curTime * curTime * curTime - 1) + start;
    }

    public static float EaseInOutQuartic(float start, float end, float curTime, float duration)
    {
        curTime = (curTime > duration) ? 2.0f : curTime / (duration / 2);

        if (curTime < 1)
            return end / 2 * curTime * curTime * curTime * curTime + start;

        curTime -= 2;

        return -end / 2 * (curTime * curTime * curTime * curTime - 2) + start;
    }
    #endregion

    #region Quadratic
    public static float EaseInQuadratic(float start, float end, float curTime, float duration)
    {
        curTime = (curTime > duration) ? 1.0f : curTime / duration;

        return end * curTime * curTime + start;
    }
    public static float EaseOutQuadratic(float start, float end, float curTime, float duration)
    {
        curTime = (curTime > duration) ? 1.0f : curTime / duration;

        return -end * curTime * (curTime - 2) + start;
    }
    public static float EaseInOutQuadratic(float start, float end, float curTime, float duration)
    {
        curTime = (curTime > duration) ? 2.0f : curTime / (duration / 2);

        if (curTime < 1)
            return end / 2 * curTime * curTime + start;

        curTime--;

        return -end / 2 * (curTime * (curTime - 2) - 1) + start;
    }
    #endregion

    #region Circular
    public static float EaseInCircular(float start, float end, float curTime, float duration)
    {
        curTime = (curTime > duration) ? 1.0f : curTime / duration;

        return -end * (Mathf.Sqrt(1 - curTime * curTime) - 1) + start;
    }
    public static float EaseOutCircular(float start, float end, float curTime, float duration)
    {
        curTime = (curTime > duration) ? 1.0f : curTime / duration;
        curTime--;

        return end * Mathf.Sqrt(1 - curTime * curTime) + start;
    }
    public static float EaseInOutCircular(float start, float end, float curTime, float duration)
    {
        curTime = (curTime > duration) ? 2.0f : curTime / (duration / 2);

        if (curTime < 1)
            return -end / 2 * (Mathf.Sqrt(1 - curTime * curTime) - 1) + start;

        curTime -= 2;

        return end / 2 * (Mathf.Sqrt(1 - curTime * curTime) + 1) + start;
    }
    #endregion

    #region Back
    public static float EaseInBack(float start, float end, float curTime, float duration)
    {
        float s = 1.70158f;

        curTime /= duration;

        return end * curTime * curTime * ((s + 1) * curTime - s) + start;
    }

    public static float EaseOutBack(float start, float end, float curTime, float duration)
    {
        float s = 1.70158f;

        curTime /= duration;
        curTime -= 1f;

        return end * (curTime * curTime * ((s + 1) * curTime + s) + 1 ) + start;
    }

    public static float EaseInOutBack(float start, float end, float curTime, float duration)
    {
        float s = 1.70158f * 1.525f;

        curTime /= (duration / 2f);

        if (curTime < 1)
            return end / 2 * (curTime * curTime * (((s) + 1) * curTime - s)) + start;

        curTime -= 2;

        return end / 2 * ((curTime) * curTime * (((s) + 1) * curTime + s) + 2) + start;
    }
    #endregion

    #region Elastic
    public static float EaseInElastic(float start, float end, float curTime, float duration)
    {
        if (curTime == 0)
            return start;

        curTime /= duration;

        if (curTime == 1)
            return start + end;

        float p = duration * .3f;
        float a = end;
        float s = p / 4f;

        curTime--;

        float postFix = a * Mathf.Pow(2, 10 * curTime);

        return - (postFix * Mathf.Sin((curTime * duration - s) * (2 * Mathf.PI) / p)) + start;
    }

    public static float EaseOutElastic(float start, float end, float curTime, float duration)
    {
        if (curTime == 0)
            return start;

        curTime /= duration;

        if (curTime == 1)
            return start + end;

        float p = duration * .3f;
        float a = end;
        float s = p / 4f;
        float postFix = a * Mathf.Pow(2, -10 * curTime);

        return postFix * Mathf.Sin((curTime * duration - s) * (2 * Mathf.PI) / p) + end + start;
    }

    public static float EaseInOutElastic(float start, float end, float curTime, float duration)
    {
        if (curTime == 0)
            return start;

        curTime /= (duration / 2f);

        if (curTime == 2)
            return start + end;

        float p = duration * (.3f * 1.5f);
        float a = end;
        float s = p / 4f;
        float postFix = 0f;

        if (curTime < 1)
        {
            postFix = a * Mathf.Pow(2, 10 * --curTime);

            return -.5f * (postFix * Mathf.Sin((curTime * duration - s) * (2 * Mathf.PI) / p)) + start;
        }

        postFix = a * Mathf.Pow(2, -10 * --curTime);

        return postFix * Mathf.Sin((curTime * duration - s) * (2 * Mathf.PI) / p) * .5f + end + start;
    }
    #endregion

    #region Bounce
    public static float EaseInBounce(float start, float end, float curTime, float duration)
    {
        return end - EaseOutBounce(0f, end, duration - curTime, duration) + start;
    }

    public static float EaseOutBounce(float start, float end, float curTime, float duration)
    {
        curTime /= duration;

        if(curTime < (1f / 2.75f))
            return end * (7.5625f * curTime * curTime) + start;
        else if(curTime < (2 / 2.75f))
        {
            curTime -= (1.5f / 2.75f);

            return end * (7.5625f * curTime * curTime + .75f) + start;
        }
        else if(curTime < (2.5f / 2.75f))
        {
            curTime -= (2.25f / 2.75f);

            return end * (7.5625f * curTime * curTime + .9375f) + start;
        }

        curTime -= (2.625f / 2.75f);

        return end * (7.5625f * curTime * curTime + .984375f) + start;
    }

    public static float EaseInOutBounce(float start, float end, float curTime, float duration)
    {
        if (curTime < (duration / 2f))
            return EaseInBounce(0f, end, curTime * 2f, duration) * .5f + start;

        return EaseOutBounce(0f, end, curTime * 2 - duration, duration) * .5f + end * .5f + start;
    }
    #endregion
}
