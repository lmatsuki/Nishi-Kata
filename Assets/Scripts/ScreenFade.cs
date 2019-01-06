using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    public float smoothFadeVelocity;
    public float smoothScreenFadeVelocity;
    public float fadeOutTime;
    public float fadeInTime;

    ////private DepthOfFieldModel.Settings depthOfFieldSettings;
    private PostProcessProfile postProcessProfile;
    private Image image;
    private bool isFadingOut;
    private bool isFadingIn;

	void Start()
    {
        ////postProcessProfile = GetComponent<PostProcessingBehaviour>().profile;
        ////depthOfFieldSettings = postProcessProfile.depthOfField.settings;
        image = GetComponentInChildren<Image>();

        // Set initial gray screen alpha
        Color newColor = image.color;
        newColor.a = 0.42f;
        image.color = newColor;
	}
	
	void Update()
    {
        FadeOut();
        FadeIn();
    }

    public void SetScreenFade(bool fadeOut)
    {
        isFadingOut = fadeOut;
        isFadingIn = !fadeOut;
    }

    void FadeOut()
    {
        if (!isFadingOut)
        {
            return;
        }

        ////if (!Mathf.Approximately(depthOfFieldSettings.aperture, 0.1f))
        ////{
        ////    depthOfFieldSettings.aperture = Mathf.SmoothDamp(depthOfFieldSettings.aperture, 0.1f, ref smoothFadeVelocity, fadeOutTime);
        ////    postProcessProfile.depthOfField.settings = depthOfFieldSettings;
        ////}

        if (!Mathf.Approximately(image.color.a, 0.42f))
        {
            Color newColor = image.color;
            newColor.a = Mathf.SmoothDamp(newColor.a, 0.42f, ref smoothScreenFadeVelocity, fadeOutTime);
            image.color = newColor;
        }
    }

    void FadeIn()
    {
        if (!isFadingIn)
        {
            return;
        }

        ////if (!Mathf.Approximately(depthOfFieldSettings.aperture, 32f))
        ////{
        ////    depthOfFieldSettings.aperture = Mathf.SmoothDamp(depthOfFieldSettings.aperture, 32f, ref smoothFadeVelocity, fadeInTime);
        ////    ////postProcessProfile.depthOfField.settings = depthOfFieldSettings;
        ////}

        if (!Mathf.Approximately(image.color.a, 0f))
        {
            Color newColor = image.color;
            newColor.a = Mathf.SmoothDamp(newColor.a, 0f, ref smoothScreenFadeVelocity, fadeOutTime);
            image.color = newColor;
        }
    }

    public void InstantlyClearScreen()
    {
        if (postProcessProfile == null)
        {
            // Clear depth of field effect
            ////postProcessProfile = GetComponent<PostProcessingBehaviour>().profile;
            ////depthOfFieldSettings = postProcessProfile.depthOfField.settings;
            ////depthOfFieldSettings.aperture = 32f;
            ////postProcessProfile.depthOfField.settings = depthOfFieldSettings;
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
