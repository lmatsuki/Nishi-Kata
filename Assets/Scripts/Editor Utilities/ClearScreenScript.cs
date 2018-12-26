using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.UI;

public class ClearScreenScript : MonoBehaviour
{
    private Image fadeImage;
    private DepthOfFieldModel.Settings depthOfFieldSettings;
    private PostProcessingProfile postProcessingProfile;

    public void ClearScreen()
    {
        ClearDepthOfFieldEffect();
        ClearFadeScreen();
    }

    void ClearDepthOfFieldEffect()
    {
        if (postProcessingProfile == null)
        {
            postProcessingProfile = Camera.main.GetComponent<PostProcessingBehaviour>().profile;
            depthOfFieldSettings = postProcessingProfile.depthOfField.settings;
        }

        // Clear depth of field effect
        depthOfFieldSettings.aperture = 32f;
        postProcessingProfile.depthOfField.settings = depthOfFieldSettings;
    }

    void ClearFadeScreen()
    {
        if (fadeImage == null)
        {
            fadeImage = Camera.main.GetComponentInChildren<Image>();
        }

        Color newColor = fadeImage.color;
        newColor.a = 0f;
        fadeImage.color = newColor;
    }
}
