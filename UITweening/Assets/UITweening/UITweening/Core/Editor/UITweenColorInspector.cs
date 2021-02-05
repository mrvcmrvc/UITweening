using UnityEditor;

namespace UITweening
{
    [CustomEditor(typeof(UITweenColor))]
    public class UITweenColorInspector : InspectorBase<UITweenColor>
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawDefaultInspector();

            DrawLoopTypeProperty();

            DrawEaseProperty();

            if (easeProperty.enumValueIndex == (int)UITweeningEaseEnum.Curve)
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
}