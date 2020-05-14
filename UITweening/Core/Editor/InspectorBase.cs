using UnityEditor;

namespace UITweening
{
    public class InspectorBase<T> : Editor where
        T : UITweener
    {
        private SerializedProperty _easeProperty;
        private SerializedProperty _animCurveProperty;
        private SerializedProperty _initOnAwakeProperty;
        private SerializedProperty _durationProperty;
        private SerializedProperty _shakePunchAmountProperty;
        private SerializedProperty _shakePunchDirectionProperty;
        private SerializedProperty _ignoreTimeScaleProperty;
        private SerializedProperty _playAutoProperty;
        private SerializedProperty _delayProperty;
        private SerializedProperty _delayDurationProperty;
        private SerializedProperty _loopTypeProperty;

        protected T _myTarget;

        void OnEnable()
        {
            _myTarget = (T)target;
        }

        protected void DrawEaseProperty()
        {
            _easeProperty = serializedObject.FindProperty("Ease");
            EditorGUILayout.PropertyField(_easeProperty, false);
        }

        protected void DrawAnimCurveProperty()
        {
            _animCurveProperty = serializedObject.FindProperty("CustomAnimationCurve");
            EditorGUILayout.PropertyField(_animCurveProperty, false);
        }

        protected void InitOnAwakeProperty()
        {
            _initOnAwakeProperty = serializedObject.FindProperty("InitOnAwake");
            EditorGUILayout.PropertyField(_initOnAwakeProperty, false);
        }

        protected void DrawDurationProperty()
        {
            _durationProperty = serializedObject.FindProperty("Duration");
            EditorGUILayout.PropertyField(_durationProperty, false);
        }

        protected void DrawShakePunchAmountProperty()
        {
            _shakePunchAmountProperty = serializedObject.FindProperty("ShakePunchAmount");
            EditorGUILayout.PropertyField(_shakePunchAmountProperty, false);

            _shakePunchDirectionProperty = serializedObject.FindProperty("ShakePunchDirection");
            EditorGUILayout.PropertyField(_shakePunchDirectionProperty, false);
        }

        protected void DrawIgnoreTimeScaleProperty()
        {
            _ignoreTimeScaleProperty = serializedObject.FindProperty("IgnoreTimeScale");
            EditorGUILayout.PropertyField(_ignoreTimeScaleProperty, false);
        }

        protected void DrawPlayAutomaticallyProperty()
        {
            _playAutoProperty = serializedObject.FindProperty("PlayAutomatically");
            EditorGUILayout.PropertyField(_playAutoProperty, false);
        }

        protected void DrawIsDelayProperty()
        {
            _delayProperty = serializedObject.FindProperty("Delay");
            EditorGUILayout.PropertyField(_delayProperty, false);

            if (_myTarget.Delay)
                DrawDelayDurationProperty();
        }

        protected void DrawDelayDurationProperty()
        {
            _delayDurationProperty = serializedObject.FindProperty("DelayDuration");
            EditorGUILayout.PropertyField(_delayDurationProperty, false);
        }

        protected void DrawLoopTypeProperty()
        {
            _loopTypeProperty = serializedObject.FindProperty("LoopType");
            EditorGUILayout.PropertyField(_loopTypeProperty, false);
        }
    }
}
