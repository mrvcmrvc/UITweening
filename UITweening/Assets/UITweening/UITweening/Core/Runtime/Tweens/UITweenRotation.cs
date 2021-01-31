using UnityEngine;

namespace UITweening
{
    [RequireComponent(typeof(RectTransform))]
    public class UITweenRotation : UITweener
    {
        public Vector3 From, To;

        public Quaternion Value { get; private set; }

        RectTransform myTransform;

        protected override void Wake()
        {
            myTransform = gameObject.GetComponent<RectTransform>();

            base.Wake();
        }

        protected override void SetValue(float clampedValue)
        {
            if (Ease == UITweeningEaseEnum.Shake/* || Ease == UITweeningEaseEnum.Punch*/)
            {
                Vector3 delta = ShakePunchDirection * ShakePunchAmount * clampedValue;

                Vector3 newEulerAngle = myTransform.localEulerAngles + delta;

                Value = Quaternion.Euler(newEulerAngle);
            }
            else
            {
                Quaternion fromQuat = Quaternion.Euler(From);
                Quaternion toQuat = Quaternion.Euler(To);

                Quaternion newQuat = Quaternion.Lerp(fromQuat, toQuat, clampedValue);

                Value = newQuat;
            }
        }

        protected override void PlayAnim()
        {
            if (myTransform == null)
                return;

            myTransform.localRotation = Value;
        }

        protected override void Finish()
        {
        }

        protected override void Kill()
        {
        }

        #region ContextMenu

        [ContextMenu("Set FROM")]
        void SetFrom()
        {
            From = GetComponent<RectTransform>().localEulerAngles;
        }

        [ContextMenu("Set TO")]
        void SetTo()
        {
            To = GetComponent<RectTransform>().localEulerAngles;
        }

        [ContextMenu("Assume FROM")]
        void AssumeFrom()
        {
            GetComponent<RectTransform>().localEulerAngles = From;
        }

        [ContextMenu("Assume TO")]
        void AssumeTo()
        {
            GetComponent<RectTransform>().localEulerAngles = To;
        }

        public override void InitValueToFROM()
        {
            Value = Quaternion.Euler(From);

            base.InitValueToFROM();
        }

        public override void InitValueToTO()
        {
            Value = Quaternion.Euler(To);

            base.InitValueToTO();
        }

        #endregion
    }
}