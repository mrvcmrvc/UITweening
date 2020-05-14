using UnityEditor;

[CustomEditor(typeof(UITweenColor))]
public class UITweenColorInspector : InspectorBase<UITweenColor>
{
    private SerializedProperty _easeProperty;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        _easeProperty = serializedObject.FindProperty("Ease");

        DrawDefaultInspector();

        DrawLoopTypeProperty();

        DrawEaseProperty();

        if (_easeProperty.enumValueIndex == (int)UITweeningEaseEnum.Curve)
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
