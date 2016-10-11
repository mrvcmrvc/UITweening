using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MMTweenAlpha))]
public class MMTweenAlphaInspector : Editor
{
    MMTweenAlpha myTarget;

    void OnEnable()
    {
        myTarget = (MMTweenAlpha)target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        myTarget.From = EditorGUILayout.Slider("From", myTarget.From, 0f, 1f);
        myTarget.To = EditorGUILayout.Slider("To", myTarget.To, 0f, 1f);

        myTarget.LoopType = (MMTweeningLoopTypeEnum)EditorGUILayout.EnumPopup("Loop Type", myTarget.LoopType);

        myTarget.Ease = (MMTweeningEaseEnum)EditorGUILayout.EnumPopup("Ease", myTarget.Ease);
        if (myTarget.Ease == MMTweeningEaseEnum.Curve)
            myTarget.AnimationCurve = EditorGUILayout.CurveField(myTarget.AnimationCurve);

        myTarget.Delay = EditorGUILayout.Toggle("Delay", myTarget.Delay);
        if (myTarget.Delay)
            myTarget.DelayDuration = EditorGUILayout.FloatField("Delay Duration", myTarget.DelayDuration);

        myTarget.Duration = EditorGUILayout.FloatField("Duration", myTarget.Duration);
        myTarget.IgnoreTimeScale = EditorGUILayout.Toggle("Ignore TimeScale", myTarget.IgnoreTimeScale);

        myTarget.PlayAutomatically = EditorGUILayout.Toggle("Play Automatically", myTarget.PlayAutomatically);

        if (EditorGUI.EndChangeCheck() || GUI.changed)
            EditorUtility.SetDirty(myTarget);
    }
}
