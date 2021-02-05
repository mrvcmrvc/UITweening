using UnityEngine;

namespace UITweening
{
    [RequireComponent(typeof(RectTransform))]
    public class UITweenSize : UITweener
    {
        public Vector2 From, To;

        public Vector2 Value { get; private set; }

        private RectTransform myTransform;

        protected override void Wake()
        {
            myTransform = gameObject.GetComponent<RectTransform>();

            base.Wake();
        }

        protected override void SetValue(float clampedValue)
        {
            if (Ease == UITweeningEaseEnum.Shake/* || Ease == UITweeningEaseEnum.Punch*/)
            {
                Vector2 delta = ShakePunchDirection * ShakePunchAmount * clampedValue;

                Value = GetComponent<RectTransform>().sizeDelta + delta;
            }
            else
            {
                Vector2 diff = To - From;
                Vector2 delta = diff * clampedValue;

                Value = From + delta;
            }
        }

        protected override void PlayAnim()
        {
            if (myTransform == null)
                return;

            myTransform.sizeDelta = Value;
        }

        protected override void Finish()
        {
        }

        protected override void Kill()
        {
        }

        #region ContextMenu
        [ContextMenu("Set FROM")]
        private void SetFrom()
        {
            From = GetComponent<RectTransform>().sizeDelta;
        }

        [ContextMenu("Set TO")]
        private void SetTo()
        {
            To = GetComponent<RectTransform>().sizeDelta;
        }

        [ContextMenu("Assume FROM")]
        private void AssumeFrom()
        {
            GetComponent<RectTransform>().sizeDelta = From;
        }

        [ContextMenu("Assume TO")]
        private void AssumeTo()
        {
            GetComponent<RectTransform>().sizeDelta = To;
        }

        public override void InitValueToFROM()
        {
            Value = From;

            base.InitValueToFROM();
        }

        public override void InitValueToTO()
        {
            Value = To;

            base.InitValueToTO();
        }
        #endregion
    }
}