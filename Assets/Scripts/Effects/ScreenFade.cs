using UnityEngine;
using UnityEngine.UI;

namespace NishiKata.Effects
{
    public class ScreenFade : MonoBehaviour
    {
        public float smoothFadeVelocity;
        public float smoothScreenFadeVelocity;
        public float fadeOutTime;
        public float fadeInTime;

        private Image image;
        private bool isFadingOut;
        private bool isFadingIn;

        void Start()
        {
            image = GetComponentInChildren<Image>();

            // Set initial gray screen alpha
            Color newColor = image.color;
            newColor.a = 0.42f;
            image.color = newColor;
        }

        void Update()
        {
            if (isFadingOut)
            {
                FadeOut();
            }

            if (isFadingIn)
            {
                FadeIn();
            }
        }

        void FadeOut()
        {
            TweenScreenAlphaToValue(0.42f);
        }

        void FadeIn()
        {
            TweenScreenAlphaToValue(0f);
        }

        void TweenScreenAlphaToValue(float value)
        {
            if (!Mathf.Approximately(image.color.a, value))
            {
                Color newColor = image.color;
                newColor.a = Mathf.SmoothDamp(newColor.a, value, ref smoothScreenFadeVelocity, fadeOutTime);
                image.color = newColor;
            }
        }

        public void SetScreenFade(bool fadeOut)
        {
            isFadingOut = fadeOut;
            isFadingIn = !fadeOut;
        }

        public void InstantlyClearScreen()
        {
            if (image == null)
            {
                image = GetComponentInChildren<Image>();
            }

            Color newColor = image.color;
            newColor.a = 0f;
            image.color = newColor;
        }
    }
}
