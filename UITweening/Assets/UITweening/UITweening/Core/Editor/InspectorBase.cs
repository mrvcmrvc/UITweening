using UnityEditor;

namespace UITweening
{
    public abstract class InspectorBase<T> : Editor where
        T : UITweener
    {
        protected SerializedProperty easeProperty;
        protected SerializedProperty animCurveProperty;
        protected SerializedProperty initOnAwakeProperty;
        protected SerializedProperty durationProperty;
        protected SerializedProperty shakePunchAmountProperty;
        protected SerializedProperty shakePunchDirectionProperty;
        protected SerializedProperty ignoreTimeScaleProperty;
        protected SerializedProperty playAutoProperty;
        protected SerializedProperty delayProperty;
        protected SerializedProperty delayDurationProperty;
        protected SerializedProperty loopTypeProperty;

        protected T myTarget;

        protected virtual void OnEnable()
        {
            myTarget = (T)target;
            
            easeProperty = serializedObject.FindProperty(UITweener.EditorEase);
            animCurveProperty = serializedObject.FindProperty(UITweener.EditorAnimCurve);
            initOnAwakeProperty = serializedObject.FindProperty(UITweener.EditorInitOnAwake);
            durationProperty = serializedObject.FindProperty(UITweener.EditorDuration);
            shakePunchAmountProperty = serializedObject.FindProperty(UITweener.EditorShakePunchAmount);
            shakePunchDirectionProperty = serializedObject.FindProperty(UITweener.EditorShakePunchDirection);
            ignoreTimeScaleProperty = serializedObject.FindProperty(UITweener.EditorIgnoreTimeScale);
            playAutoProperty = serializedObject.FindProperty(UITweener.EditorPlayAuto);
            delayProperty = serializedObject.FindProperty(UITweener.EditorDelay);
            delayDurationProperty = serializedObject.FindProperty(UITweener.EditorDelayDuration);
            loopTypeProperty = serializedObject.FindProperty(UITweener.EditorLoopType);
        }

        protected void DrawEaseProperty()
        {
            EditorGUILayout.PropertyField(easeProperty, false);
        }

        protected void DrawAnimCurveProperty()
        {
            EditorGUILayout.PropertyField(animCurveProperty, false);
        }

        protected void InitOnAwakeProperty()
        {
            EditorGUILayout.PropertyField(initOnAwakeProperty, false);
        }

        protected void DrawDurationProperty()
        {
            EditorGUILayout.PropertyField(durationProperty, false);
        }

        protected void DrawShakePunchAmountProperty()
        {
            EditorGUILayout.PropertyField(shakePunchAmountProperty, false);

            EditorGUILayout.PropertyField(shakePunchDirectionProperty, false);
        }

        protected void DrawIgnoreTimeScaleProperty()
        {
            EditorGUILayout.PropertyField(ignoreTimeScaleProperty, false);
        }

        protected void DrawPlayAutomaticallyProperty()
        {
            EditorGUILayout.PropertyField(playAutoProperty, false);
        }

        protected void DrawIsDelayProperty()
        {
            EditorGUILayout.PropertyField(delayProperty, false);

            if (myTarget.Delay)
                DrawDelayDurationProperty();
        }

        protected void DrawDelayDurationProperty()
        {
            EditorGUILayout.PropertyField(delayDurationProperty, false);
        }

        protected void DrawLoopTypeProperty()
        {
            EditorGUILayout.PropertyField(loopTypeProperty, false);
        }
    }
}
