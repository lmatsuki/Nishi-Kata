using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    public float smoothFadeVelocity;
    public float smoothScreenFadeVelocity;
    public float fadeOutTime;
    public float fadeInTime;

    private DepthOfField depthOfFieldSettings;
    private PostProcessProfile postProcessProfile;
    private Image image;
    private bool isFadingOut;
    private bool isFadingIn;

	void Start()
    {
        postProcessProfile = GetComponent<PostProcessVolume>().profile;
        image = GetComponentInChildren<Image>();

        if (!postProcessProfile.TryGetSettings(out depthOfFieldSettings))
        {
            DebugExtensions.LogNotFound(this, "Depth of field setting");
        }

        // Set initial depth of field
        depthOfFieldSettings.focalLength.value = 50;

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
        TweenDepthOfFieldToValue(170f);
        TweenScreenAlphaToValue(0.42f);
    }

    void FadeIn()
    {
        TweenDepthOfFieldToValue(1f);
        TweenScreenAlphaToValue(0f);
    }

    void TweenDepthOfFieldToValue(float value)
    {
        if (!Mathf.Approximately(depthOfFieldSettings.focalLength, value))
        {
            depthOfFieldSettings.focalLength.value = Mathf.SmoothDamp(depthOfFieldSettings.focalLength, value, ref smoothFadeVelocity, fadeInTime);
        }
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
        if (postProcessProfile == null)
        {
            // Clear depth of field effect
            postProcessProfile = GetComponent<PostProcessVolume>().profile;

            if (!postProcessProfile.TryGetSettings(out depthOfFieldSettings))
            {
                DebugExtensions.LogNotFound(this, "Depth of field setting");
            }

            depthOfFieldSettings.aperture.value = 32f;
        }

        if (image == null)
        {
            image = GetComponentInChildren<Image>();
        }

        Color newColor = image.color;
        newColor.a = 0f;
        image.color = newColor;
    }
}
