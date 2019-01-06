using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class ClearScreenScript : MonoBehaviour
{
    private Image fadeImage;
    ////private DepthOfFieldModel.Settings depthOfFieldSettings;
    private PostProcessProfile postProcessProfile;

    public void ClearScreen()
    {
        ClearDepthOfFieldEffect();
        ClearFadeScreen();
    }

    void ClearDepthOfFieldEffect()
    {
        if (postProcessProfile == null)
        {
            ////postProcessProfile = Camera.main.GetComponent<PostProcessingBehaviour>().profile;
            ////depthOfFieldSettings = postProcessProfile.depthOfField.settings;
        }

        // Clear depth of field effect
        ////depthOfFieldSettings.aperture = 32f;
        ////postProcessProfile.depthOfField.settings = depthOfFieldSettings;
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
