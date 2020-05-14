using UnityEngine;
using UITweening;

public class TestScript : MonoBehaviour
{
    UITweener _tweener;

    void Awake()
    {
        _tweener = gameObject.GetComponent<UITweener>();

        Application.targetFrameRate = 120;
    }

    public void SetTargetFrameRate(int rate)
    {
        Application.targetFrameRate = rate;
    }

    public void KillTween()
    {
        _tweener.KillTween();
    }

    public void FinishTween()
    {
        _tweener.FinishTween();
    }

    public void PlayForward()
    {
        _tweener.PlayForward();
    }

    public void PlayReverse()
    {
        _tweener.PlayReverse();
    }

    public void PauseTween()
    {
        _tweener.PauseTween();
    }

    public void ResumeTween()
    {
        _tweener.ResumeTween();
    }

    public void InitValueToFROM()
    {
        _tweener.InitValueToFROM();
    }

    public void InitValueToTO()
    {
        _tweener.InitValueToTO();
    }

    public void SetTimeScaleTo(float newTimeScale)
    {
        Time.timeScale = newTimeScale;
    }
}
