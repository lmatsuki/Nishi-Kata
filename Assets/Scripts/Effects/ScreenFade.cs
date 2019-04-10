using NishiKata.Utilities;
using System.Linq;
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

        private Image fadeImage;
        private bool isFadingOut;
        private bool isFadingIn;

        void Start()
        {
            Image[] images = GetComponentsInChildren<Image>();
            fadeImage = images.FirstOrDefault(i => i.name == Names.FadeImage);

            // Set initial gray screen alpha
            Color newColor = fadeImage.color;
            newColor.a = 0.42f;
            fadeImage.color = newColor;
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
            if (!Mathf.Approximately(fadeImage.color.a, value))
            {
                Color newColor = fadeImage.color;
                newColor.a = Mathf.SmoothDamp(newColor.a, value, ref smoothScreenFadeVelocity, fadeOutTime);
                fadeImage.color = newColor;
            }
        }

        public void SetScreenFade(bool fadeOut)
        {
            isFadingOut = fadeOut;
            isFadingIn = !fadeOut;
        }

        public void InstantlyClearScreen()
        {
            if (fadeImage == null)
            {
                fadeImage = GetComponentInChildren<Image>();
            }

            Color newColor = fadeImage.color;
            newColor.a = 0f;
            fadeImage.color = newColor;
        }
    }
}
