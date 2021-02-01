using UnityEngine;
using UITweening;

public class TestScript : MonoBehaviour
{
    private UITweener _tweener;

    private void Awake()
    {
        _tweener = gameObject.GetComponent<UITweener>();

        SetTargetFrameRate(60);
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

    public void RegisterToStartEvent()
    {
        _tweener.AddOnStart(OnTweenStarted);
    }

    public void RegisterToFinishEvent()
    {
        _tweener.AddOnFinish(OnTweenFinished);
    }
    
    private void OnTweenStarted()
    {
        Debug.Log("Tween Started");
    }

    private void OnTweenFinished()
    {
        Debug.Log("Tween Finished");        
    }
}
