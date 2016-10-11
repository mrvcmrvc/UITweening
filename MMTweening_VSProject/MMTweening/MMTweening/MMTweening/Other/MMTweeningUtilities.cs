using UnityEngine;

public class MMTweeningUtilities
{
    public static float Linear(float start, float end, float curTime, float duration)
    {
        curTime /= duration;
        end -= start;
        return end * curTime + start;
    }

    public static float EaseInCubic(float start, float end, float curTime, float duration)
    {
        curTime /= duration;
        end -= start;
        return end * curTime * curTime * curTime + start;
    }

    public static float EaseOutCubic(float start, float end, float curTime, float duration)
    {
        //curTime--;
        curTime = curTime / duration - 1;
        end -= start;
        //(t=t/d-1)

        return end * (curTime * curTime * curTime + 1) + start;
    }

    public static Vector3 EaseOutCubic(Vector3 start, Vector3 end, float curTime, float duration)
    {
        //curTime--;
        curTime = curTime / duration - 1;
        end -= start;
        //(t=t/d-1)

        return end * (curTime * curTime * curTime + 1) + start;
    }

    public static float EaseInOutCubic(float start, float end, float curTime, float duration)
    {
        curTime = curTime / duration - 1;
        end -= start;
        return end * (curTime * curTime * curTime + 1) + start;
    }

    public static Vector3 EaseInOutCubic(Vector3 start, Vector3 end, float curTime, float duration)
    {
        curTime = curTime / duration - 1;
        end -= start;
        return end * (curTime * curTime * curTime + 1) + start;
    }

    public static float EaseInSine(float start, float end, float curTime, float duration)
    {
        end -= start;
        return -end * Mathf.Cos(curTime / duration / 1 * (Mathf.PI / 2)) + end + start;
    }

    public static float EaseOutSine(float start, float end, float curTime, float duration)
    {
        end -= start;
        return end * Mathf.Sin(curTime / duration / 1 * (Mathf.PI / 2)) + start;
    }

    public static float EaseInOutSine(float start, float end, float curTime, float duration)
    {
        end -= start;
        return -end / 2 * (Mathf.Cos(Mathf.PI * curTime / duration / 1) - 1) + start;
    }

    public static float EaseInExpo(float start, float end, float curTime, float duration)
    {
        end -= start;
        return end * Mathf.Pow(2, 10 * (curTime / duration / 1 - 1)) + start;
    }

    public static float EaseOutExpo(float start, float end, float curTime, float duration)
    {
        end -= start;
        return end * (-Mathf.Pow(2, -10 * curTime / duration / 1) + 1) + start;
    }

    public static float EaseInOutExpo(float start, float end, float curTime, float duration)
    {
        curTime /= .5f;
        end -= start;
        if (curTime < 1)
            return end / 2 * Mathf.Pow(2, 10 * (curTime / duration - 1)) + start;
        curTime--;
        return end / 2 * (-Mathf.Pow(2, -10 * curTime / duration) + 2) + start;
    }

    public static float EaseInOutBack(float start, float end, float curTime, float duration)
    {
        curTime /= duration;
        //Debug.Log(end + " " + start + " " +curTime);
        float s = 1.70158f;
        end -= start;
        //curTime /= duration;
        curTime /= .5f;
        if ((curTime) < 1)
        {
            s *= (1.525f);
            return end / 2 * (curTime * curTime * (((s) + 1) * curTime - s)) + start;
        }
        curTime -= 2;
        s *= (1.525f);
        return end / 2 * ((curTime) * curTime * (((s) + 1) * curTime + s) + 2) + start;
    }

//    static function EaseInQuad(start : float, distance : float,
//                     elapsedTime : float, duration : float) : float {
//  // clamp elapsedTime so that it cannot be greater than duration
//  elapsedTime = (elapsedTime > duration) ? 1.0 : elapsedTime / duration;
//  return distance*elapsedTime*elapsedTime + start;
//}
 
///**
// * quadratic easing out - decelerating to zero velocity
// */
//static function EaseOutQuad(start : float, distance : float,
//                     elapsedTime : float, duration : float) : float {
//  // clamp elapsedTime so that it cannot be greater than duration
//  elapsedTime = (elapsedTime > duration) ? 1.0 : elapsedTime / duration;
//  return -distance * elapsedTime*(elapsedTime-2) + start;
//}
 
///**
// * quadratic easing in/out - acceleration until halfway, then deceleration
// */
//static function EaseInOutQuad(start : float, distance : float,
//                     elapsedTime : float, duration : float) : float {
//  // clamp elapsedTime so that it cannot be greater than duration
//  elapsedTime = (elapsedTime > duration) ? 2.0 : elapsedTime / (duration / 2);
//  if (elapsedTime < 1) return distance/2*elapsedTime*elapsedTime + start;
//  elapsedTime--;
//  return -distance/2 * (elapsedTime*(elapsedTime-2) - 1) + start;
//}

    /**
 * quartic easing in - accelerating from zero velocity
 */
//static function EaseInQuart(start : float, distance : float,
//                     elapsedTime : float, duration : float) : float {
//  // clamp elapsedTime so that it cannot be greater than duration
//  elapsedTime = (elapsedTime > duration) ? 1.0 : elapsedTime / duration;
//  return distance*elapsedTime*elapsedTime*elapsedTime*elapsedTime + start;
//}
 
///**
// * quartic easing out - decelerating to zero velocity
// */
//static function EaseOutQuart(start : float, distance : float,
//                     elapsedTime : float, duration : float) : float {
//  // clamp elapsedTime so that it cannot be greater than duration
//  elapsedTime = (elapsedTime > duration) ? 1.0 : elapsedTime / duration;
//  elapsedTime--;
//  return -distance * (elapsedTime*elapsedTime*elapsedTime*elapsedTime - 1) +
//                         start;
//}
 
///**
// * quartic easing in/out - acceleration until halfway, then deceleration
// */
//static function EaseInOutQuart(start : float, distance : float,
//                     elapsedTime : float, duration : float) : float {
//  // clamp elapsedTime so that it cannot be greater than duration
//  elapsedTime = (elapsedTime > duration) ? 2.0 : elapsedTime / (duration / 2);
//  if (elapsedTime < 1) return distance/2*
//                         elapsedTime*elapsedTime*elapsedTime*elapsedTime +
//                         start;
//  elapsedTime -= 2;
//  return -distance/2 * (elapsedTime*elapsedTime*elapsedTime*elapsedTime - 2) +
//                         start;
//}

    /**
 * quintic easing in - accelerating from zero velocity
 */
//static function EaseInQuint(start : float, distance : float,
//                     elapsedTime : float, duration : float) : float {
//  // clamp elapsedTime so that it cannot be greater than duration
//  elapsedTime = (elapsedTime > duration) ? 1.0 : elapsedTime / duration;
//  return distance*elapsedTime*elapsedTime*elapsedTime*elapsedTime*elapsedTime +
//                         start;
//}
 
///**
// * quintic easing out - decelerating to zero velocity
// */
//static function EaseOutQuint(start : float, distance : float,
//                     elapsedTime : float, duration : float) : float {
//  // clamp elapsedTime so that it cannot be greater than duration
//  elapsedTime = (elapsedTime > duration) ? 1.0 : elapsedTime / duration;
//  elapsedTime--;
//  return distance * (elapsedTime * elapsedTime * elapsedTime * elapsedTime *
//                     elapsedTime + 1) + start;
//}
 
///**
// * quintic easing in/out - acceleration until halfway, then deceleration
// */
//static function EaseInOutQuint(start : float, distance : float,
//                     elapsedTime : float, duration : float) : float {
//  // clamp elapsedTime so that it cannot be greater than duration
//  elapsedTime = (elapsedTime > duration) ? 2.0 : elapsedTime / (duration / 2);
//  if (elapsedTime < 1) return distance/2 * elapsedTime * elapsedTime *
//                         elapsedTime * elapsedTime * elapsedTime + start;
//  elapsedTime -= 2;
//  return distance/2 * (elapsedTime * elapsedTime * elapsedTime * elapsedTime *
//                       elapsedTime + 2) + start;
//}

/**
 * circular easing in - accelerating from zero velocity
 */
//static function EaseInCirc(start : float, distance : float,
//                     elapsedTime : float, duration : float) : float {
//  // clamp elapsedTime so that it cannot be greater than duration
//  elapsedTime = (elapsedTime > duration) ? 1.0 : elapsedTime / duration;
//  return -distance * (Mathf.Sqrt(1 - elapsedTime*elapsedTime) - 1) + start;
//}
 
///**
// * circular easing out - decelerating to zero velocity
// */
//static function EaseOutCirc(start : float, distance : float,
//                     elapsedTime : float, duration : float) : float {
//  // clamp elapsedTime so that it cannot be greater than duration
//  elapsedTime = (elapsedTime > duration) ? 1.0 : elapsedTime / duration;
//  elapsedTime--;
//  return distance * Mathf.Sqrt(1 - elapsedTime*elapsedTime) + start;
//}
 
///**
// * circular easing in/out - acceleration until halfway, then deceleration
// */
//static function EaseInOutCirc(start : float, distance : float,
//                     elapsedTime : float, duration : float) : float {
//  // clamp elapsedTime so that it cannot be greater than duration
//  elapsedTime = (elapsedTime > duration) ? 2.0 : elapsedTime / (duration / 2);
//  if (elapsedTime < 1) return -distance/2 *
//                         (Mathf.Sqrt(1 - elapsedTime*elapsedTime) - 1) + start;
//  elapsedTime -= 2;
//  return distance/2 * (Mathf.Sqrt(1 - elapsedTime*elapsedTime) + 1) + start;
//}
}
