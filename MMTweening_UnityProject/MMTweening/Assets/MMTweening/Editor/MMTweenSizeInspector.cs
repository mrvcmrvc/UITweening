using UnityEditor;

[CustomEditor(typeof(MMTweenSize))]
public class MMTweenSizeInspector : InspectorBase<MMTweenSize>
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if (_myTarget.Ease != MMTweeningEaseEnum.Shake)
        {
            DrawDefaultInspector();

            DrawLoopTypeProperty();
        }
        else
        {
            DrawShakePunchAmountProperty();

            _myTarget.LoopType = MMTweeningLoopTypeEnum.PingPong;
        }

        DrawEaseProperty();

        if (_myTarget.Ease == MMTweeningEaseEnum.Curve)
            DrawAnimCurveProperty();

        DrawIsDelayProperty();

        InitOnAwakeProperty();

        if (_myTarget.Ease == MMTweeningEaseEnum.Shake)
            _myTarget.SetDuration(0.02f);
        else
            DrawDurationProperty();

        DrawIgnoreTimeScaleProperty();

        DrawPlayAutomaticallyProperty();

        serializedObject.ApplyModifiedProperties();
        EditorApplication.update.Invoke();
    }
}