using UnityEngine;
using UITweening;

public class TestScript : MonoBehaviour
{
    private UITweener tweener;

    private void Awake()
    {
        tweener = gameObject.GetComponent<UITweener>();

        SetTargetFrameRate(60);
    }

    public void SetTargetFrameRate(int rate)
    {
        Application.targetFrameRate = rate;
    }

    public void KillTween()
    {
        tweener.KillTween();
    }

    public void FinishTween()
    {
        tweener.FinishTween();
    }

    public void PlayForward()
    {
        tweener.PlayForward();
    }

    public void PlayReverse()
    {
        tweener.PlayReverse();
    }

    public void PauseTween()
    {
        tweener.PauseTween();
    }

    public void ResumeTween()
    {
        tweener.ResumeTween();
    }

    public void InitValueToFROM()
    {
        tweener.InitValueToFROM();
    }

    public void InitValueToTO()
    {
        tweener.InitValueToTO();
    }

    public void SetTimeScaleTo(float newTimeScale)
    {
        Time.timeScale = newTimeScale;
    }

    public void RegisterToStartEvent()
    {
        tweener.AddOnStart(OnTweenStarted);
    }

    public void RegisterToFinishEvent()
    {
        tweener.AddOnFinish(OnTweenFinished);
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
