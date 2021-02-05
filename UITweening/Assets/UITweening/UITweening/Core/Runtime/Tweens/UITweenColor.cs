using UnityEngine;
using UnityEngine.UI;

namespace UITweening
{
    /// <summary>
    /// Does NOT work with UITweenAlpha.
    /// </summary>
    public class UITweenColor : UITweener
    {
        public Color From, To;

        public Color Value { get; private set; }

        private Graphic myImage;

        protected override void Wake()
        {
            myImage = gameObject.GetComponent<Graphic>();

            base.Wake();
        }

        protected override void SetValue(float clampedValue)
        {
            Color.RGBToHSV(From, out float fromH, out float fromS, out float fromV);
            Color.RGBToHSV(To, out float toH, out float toS, out float toV);

            float h = CalculateHue(clampedValue, fromH, toH);

            float s = CalculateSV(clampedValue, fromS, toS);
            float v = CalculateSV(clampedValue, fromV, toV);

            Value = Color.HSVToRGB(h, s, v);
        }

        private float CalculateHue(float clampedValue, float from, float to)
        {
            float diff = to - from;
            int dirSign = 1;

            if (Mathf.Abs(diff) > 0.5f)
            {
                if (diff > 0)
                    dirSign = -1;

                diff = 1 - Mathf.Abs(diff);
            }

            float newH = from + (dirSign * diff * clampedValue);
            if (newH > 1.0f)
                newH -= 1;
            else if (newH < 0.0f)
                newH += 1.0f;

            return newH;
        }

        private float CalculateSV(float clampedValue, float from, float to)
        {
            float diff = to - from;
            return from + (diff * clampedValue);
        }

        protected override void PlayAnim()
        {
            if (myImage == null)
                return;

            myImage.color = Value;
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
            From = GetComponent<Graphic>().color;
        }

        [ContextMenu("Set TO")]
        private void SetTo()
        {
            To = GetComponent<Graphic>().color;
        }

        [ContextMenu("Assume FROM")]
        private void AssumeFrom()
        {
            GetComponent<Graphic>().color = From;
        }

        [ContextMenu("Assume TO")]
        private void AssumeTo()
        {
            GetComponent<Graphic>().color = To;
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