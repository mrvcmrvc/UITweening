using UnityEditor;

[CustomEditor(typeof(MMTweenPosition))]
public class MMTweenPositionInspector : InspectorBase<MMTweenPosition>
{
    private SerializedProperty _easeProperty;
    private SerializedProperty _loopTypeProperty;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        _easeProperty = serializedObject.FindProperty("Ease");
        _loopTypeProperty = serializedObject.FindProperty("LoopType");

        if (_easeProperty.enumValueIndex != (int)MMTweeningEaseEnum.Shake)
        {
            DrawDefaultInspector();

            DrawLoopTypeProperty();
        }
        else
        {
            DrawShakePunchAmountProperty();

            _loopTypeProperty.enumValueIndex = (int)MMTweeningLoopTypeEnum.PingPong;
        }

        DrawEaseProperty();

        if (_easeProperty.enumValueIndex == (int)MMTweeningEaseEnum.Curve)
            DrawAnimCurveProperty();

        DrawIsDelayProperty();

        InitOnAwakeProperty();

        if (_easeProperty.enumValueIndex == (int)MMTweeningEaseEnum.Shake)
            _myTarget.SetDuration(0.02f);
        else
            DrawDurationProperty();

        DrawIgnoreTimeScaleProperty();

        DrawPlayAutomaticallyProperty();

        serializedObject.ApplyModifiedProperties();
        EditorApplication.update.Invoke();
    }
}
