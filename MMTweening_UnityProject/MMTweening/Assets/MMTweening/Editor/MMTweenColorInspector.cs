using UnityEditor;

[CustomEditor(typeof(MMTweenColor))]
public class MMTweenColorInspector : InspectorBase<MMTweenColor>
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawDefaultInspector();

        DrawLoopTypeProperty();

        DrawEaseProperty();

        if (_myTarget.Ease == MMTweeningEaseEnum.Curve)
            DrawAnimCurveProperty();

        DrawIsDelayProperty();

        InitOnAwakeProperty();

        DrawDurationProperty();

        DrawIgnoreTimeScaleProperty();

        DrawPlayAutomaticallyProperty();

        serializedObject.ApplyModifiedProperties();
        EditorApplication.update.Invoke();
    }
}
