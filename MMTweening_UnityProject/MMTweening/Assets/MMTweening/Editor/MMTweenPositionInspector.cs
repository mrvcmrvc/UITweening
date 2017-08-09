using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(MMTweenPosition))]
public class MMTweenPositionInspector : InspectorBase
{
    MMTweenPosition myTarget;

    void OnEnable()
    {
        myTarget = (MMTweenPosition)target;

        Enable(myTarget);
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        if (myTarget.Ease != MMTweeningEaseEnum.Shake/* && myTarget.Ease != MMTweeningEaseEnum.Punch*/)
            DrawDefaultInspector();
        else
            DrawShakePunchAmountField();

        if (/*myTarget.Ease == MMTweeningEaseEnum.Punch || */myTarget.Ease == MMTweeningEaseEnum.Shake)
            myTarget.LoopType = MMTweeningLoopTypeEnum.PingPong;
        else
            myTarget.LoopType = (MMTweeningLoopTypeEnum)EditorGUILayout.EnumPopup("Loop Type", myTarget.LoopType);

        DrawEaseField();

        if(myTarget.Ease == MMTweeningEaseEnum.Curve)
            DrawAnimCurveField();

        myTarget.Delay = EditorGUILayout.Toggle("Delay", myTarget.Delay);
        if (myTarget.Delay)
            myTarget.DelayDuration = EditorGUILayout.FloatField("Delay Duration", myTarget.DelayDuration);

        InitOnAwakeField();

        if (/*myTarget.Ease == MMTweeningEaseEnum.Punch || */myTarget.Ease == MMTweeningEaseEnum.Shake)
            myTarget.SetDuration(0.02f);
        else
            DrawDurationField();

        myTarget.IgnoreTimeScale = EditorGUILayout.Toggle("Ignore TimeScale", myTarget.IgnoreTimeScale);

        myTarget.PlayAutomatically = EditorGUILayout.Toggle("Play Automatically", myTarget.PlayAutomatically);

        if (!Application.isPlaying && (EditorGUI.EndChangeCheck() || GUI.changed))
            EditorSceneManager.MarkSceneDirty(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
    }
}
