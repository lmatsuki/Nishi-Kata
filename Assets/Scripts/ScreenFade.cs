using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    public float smoothFadeVelocity;
    public float smoothScreenFadeVelocity;
    public float fadeOutTime;
    public float fadeInTime;

    private DepthOfFieldModel.Settings depthOfFieldSettings;
    private PostProcessingProfile postProcessingProfile;
    private Image image;
    private bool isFadingOut;
    private bool isFadingIn;

	void Start()
    {
        postProcessingProfile = GetComponent<PostProcessingBehaviour>().profile;
        depthOfFieldSettings = postProcessingProfile.depthOfField.settings;
        image = GetComponentInChildren<Image>();
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
        if (isFadingOut)
        {
            if (!Mathf.Approximately(depthOfFieldSettings.aperture, 0.1f))
            {
                depthOfFieldSettings.aperture = Mathf.SmoothDamp(depthOfFieldSettings.aperture, 0.1f, ref smoothFadeVelocity, fadeOutTime);
                postProcessingProfile.depthOfField.settings = depthOfFieldSettings;
            }

            if (!Mathf.Approximately(image.color.a, 0.42f))
            {
                Color newColor = image.color;
                newColor.a = Mathf.SmoothDamp(newColor.a, 0.42f, ref smoothScreenFadeVelocity, fadeOutTime);
                image.color = newColor;
            }
        }
        else
        {
            isFadingOut = false;
        }
    }

    void FadeIn()
    {
        if (isFadingIn)
        {
            if (!Mathf.Approximately(depthOfFieldSettings.aperture, 32f))
            {
                depthOfFieldSettings.aperture = Mathf.SmoothDamp(depthOfFieldSettings.aperture, 32f, ref smoothFadeVelocity, fadeInTime);
                postProcessingProfile.depthOfField.settings = depthOfFieldSettings;
            }

            if (!Mathf.Approximately(image.color.a, 0f))
            {
                Color newColor = image.color;
                newColor.a = Mathf.SmoothDamp(newColor.a, 0f, ref smoothScreenFadeVelocity, fadeOutTime);
                image.color = newColor;
            }
        }
        else
        {
            isFadingIn = false;
        }
    }
}
