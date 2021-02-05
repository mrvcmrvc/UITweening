using UnityEngine;
using UnityEngine.UI;

namespace UITweening
{
    /// <summary>
    /// Does NOT work with UITweenColor.
    /// </summary>
    public class UITweenAlpha : UITweener
    {
        [Range(0.0f, 1.0f)] public float From;
        [Range(0.0f, 1.0f)] public float To;

        public float Value { get; private set; }

        private Graphic _myImage;
        private CanvasGroup _myCanvasGroup;

        protected override void Wake()
        {
            _myImage = gameObject.GetComponent<Graphic>();
            _myCanvasGroup = gameObject.GetComponent<CanvasGroup>();

            base.Wake();
        }

        protected override void SetValue(float clampedValue)
        {
            Value = From + CalculateA(clampedValue);
        }

        private float CalculateA(float clampedValue)
        {
            float diff = To - From;
            return diff * clampedValue;
        }

        protected override void PlayAnim()
        {
            UpdateImage();

            UpdateCanvasGroup();
        }

        private void UpdateImage()
        {
            if (_myImage == null)
                return;

            Color color = _myImage.color;
            color.a = Value;

            _myImage.color = color;
        }

        private void UpdateCanvasGroup()
        {
            if (_myCanvasGroup == null)
                return;

            _myCanvasGroup.alpha = Value;
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
            if (GetComponent<Graphic>() != null)
                From = GetComponent<Graphic>().color.a;
            else if (GetComponent<CanvasGroup>() != null)
                From = GetComponent<CanvasGroup>().alpha;
        }

        [ContextMenu("Set TO")]
        private void SetTo()
        {
            if (GetComponent<Graphic>() != null)
                To = GetComponent<Graphic>().color.a;
            else if (GetComponent<CanvasGroup>() != null)
                To = GetComponent<CanvasGroup>().alpha;
        }

        [ContextMenu("Assume FROM")]
        private void AssumeFrom()
        {
            if (GetComponent<Graphic>() != null)
            {
                Color color = GetComponent<Graphic>().color;
                color.a = From;

                GetComponent<Graphic>().color = color;
            }
            else if (GetComponent<CanvasGroup>() != null)
                GetComponent<CanvasGroup>().alpha = From;
        }

        [ContextMenu("Assume TO")]
        private void AssumeTo()
        {
            if (GetComponent<Graphic>() != null)
            {
                Color color = GetComponent<Graphic>().color;
                color.a = To;

                GetComponent<Graphic>().color = color;
            }
            else if (GetComponent<CanvasGroup>() != null)
                GetComponent<CanvasGroup>().alpha = To;
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