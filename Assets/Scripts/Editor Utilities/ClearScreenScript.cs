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
        if (postProcessingProfile == null)
        {
            postProcessingProfile = GameObject.Find("Main Camera").GetComponent<PostProcessingBehaviour>().profile;
            depthOfFieldSettings = postProcessingProfile.depthOfField.settings;
        }

        // Clear depth of field effect
        depthOfFieldSettings.aperture = 32f;
        postProcessingProfile.depthOfField.settings = depthOfFieldSettings;

        if (fadeImage == null)
        {
            fadeImage = GameObject.Find("Main Camera").GetComponentInChildren<Image>();
        }

        Color newColor = fadeImage.color;
        newColor.a = 0f;
        fadeImage.color = newColor;
    }
}
