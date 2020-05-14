using UnityEditor;

[CustomEditor(typeof(UITweenRotation))]
public class UITweenRotationInspector : InspectorBase<UITweenRotation>
{
    private SerializedProperty _easeProperty;
    private SerializedProperty _loopTypeProperty;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        _easeProperty = serializedObject.FindProperty("Ease");
        _loopTypeProperty = serializedObject.FindProperty("LoopType");

        if (_easeProperty.enumValueIndex != (int)UITweeningEaseEnum.Shake)
        {
            DrawDefaultInspector();

            DrawLoopTypeProperty();
        }
        else
        {
            DrawShakePunchAmountProperty();

            _loopTypeProperty.enumValueIndex = (int)UITweeningLoopTypeEnum.PingPong;
        }

        DrawEaseProperty();

        if (_easeProperty.enumValueIndex == (int)UITweeningEaseEnum.Curve)
            DrawAnimCurveProperty();

        DrawIsDelayProperty();

        InitOnAwakeProperty();

        if (_easeProperty.enumValueIndex == (int)UITweeningEaseEnum.Shake)
            _myTarget.SetDuration(0.02f);
        else
            DrawDurationProperty();

        DrawIgnoreTimeScaleProperty();

        DrawPlayAutomaticallyProperty();

        serializedObject.ApplyModifiedProperties();
        EditorApplication.update.Invoke();
    }
}