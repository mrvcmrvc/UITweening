using UnityEditor;

namespace UITweening
{
    [CustomEditor(typeof(UITweenPosition))]
    public class UITweenPositionInspector : InspectorBase<UITweenPosition>
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            if (easeProperty.enumValueIndex != (int)UITweeningEaseEnum.Shake)
            {
                DrawDefaultInspector();

                DrawLoopTypeProperty();
            }
            else
            {
                DrawShakePunchAmountProperty();

                loopTypeProperty.enumValueIndex = (int)UITweeningLoopTypeEnum.PingPong;
            }

            DrawEaseProperty();

            if (easeProperty.enumValueIndex == (int)UITweeningEaseEnum.Curve)
                DrawAnimCurveProperty();

            DrawIsDelayProperty();

            InitOnAwakeProperty();

            if (easeProperty.enumValueIndex == (int)UITweeningEaseEnum.Shake)
                myTarget.SetDuration(0.02f);
            else
                DrawDurationProperty();

            DrawIgnoreTimeScaleProperty();

            DrawPlayAutomaticallyProperty();

            serializedObject.ApplyModifiedProperties();
            EditorApplication.update.Invoke();
        }
    }
}