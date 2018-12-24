using UnityEngine;
using UnityEngine.PostProcessing;

public class ScreenFade : MonoBehaviour
{
    public float smoothFadeVelocity;
    public float fadeOutTime;
    public float fadeInTime;

    private DepthOfFieldModel.Settings depthOfFieldSettings;
    private PostProcessingProfile postProcessingProfile;
    private bool isFadingOut;
    private bool isFadingIn;

	void Start()
    {
        postProcessingProfile = GetComponent<PostProcessingBehaviour>().profile;
        depthOfFieldSettings = postProcessingProfile.depthOfField.settings;
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
        if (isFadingOut && !Mathf.Approximately(depthOfFieldSettings.aperture, 0.1f))
        {
            depthOfFieldSettings.aperture = Mathf.SmoothDamp(depthOfFieldSettings.aperture, 0.1f, ref smoothFadeVelocity, fadeOutTime);
            postProcessingProfile.depthOfField.settings = depthOfFieldSettings;
        }
        else
        {
            isFadingOut = false;
        }
    }

    void FadeIn()
    {
        if (isFadingIn && !Mathf.Approximately(depthOfFieldSettings.aperture, 32f))
        {
            depthOfFieldSettings.aperture = Mathf.SmoothDamp(depthOfFieldSettings.aperture, 32f, ref smoothFadeVelocity, fadeInTime);
            postProcessingProfile.depthOfField.settings = depthOfFieldSettings;
        }
        else
        {
            isFadingIn = false;
        }
    }
}
