using UnityEditor;

namespace UITweening
{
    [CustomEditor(typeof(UITweenAlpha))]
    public class UITweenAlphaInspector : InspectorBase<UITweenAlpha>
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