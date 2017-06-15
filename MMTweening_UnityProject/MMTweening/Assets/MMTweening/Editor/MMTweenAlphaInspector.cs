using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(MMTweenAlpha))]
public class MMTweenAlphaInspector : InspectorBase
{
    MMTweenAlpha myTarget;

    void OnEnable()
    {
        myTarget = (MMTweenAlpha)target;

        Enable(myTarget);
    }

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();

        myTarget.From = EditorGUILayout.Slider("From", myTarget.From, 0f, 1f);
        myTarget.To = EditorGUILayout.Slider("To", myTarget.To, 0f, 1f);

        myTarget.LoopType = (MMTweeningLoopTypeEnum)EditorGUILayout.EnumPopup("Loop Type", myTarget.LoopType);

        DrawEaseField();

        if (ease == MMTweeningEaseEnum.Curve)
            DrawAnimCurveField();

        myTarget.Delay = EditorGUILayout.Toggle("Delay", myTarget.Delay);
        if (myTarget.Delay)
            myTarget.DelayDuration = EditorGUILayout.FloatField("Delay Duration", myTarget.DelayDuration);

        DrawDurationField();

        myTarget.IgnoreTimeScale = EditorGUILayout.Toggle("Ignore TimeScale", myTarget.IgnoreTimeScale);

        myTarget.PlayAutomatically = EditorGUILayout.Toggle("Play Automatically", myTarget.PlayAutomatically);

        if (!Application.isPlaying && (EditorGUI.EndChangeCheck() || GUI.changed))
            EditorSceneManager.MarkSceneDirty(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
    }
}
