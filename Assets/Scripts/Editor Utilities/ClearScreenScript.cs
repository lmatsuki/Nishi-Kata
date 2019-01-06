using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class ClearScreenScript : MonoBehaviour
{
    private Image fadeImage;
    private DepthOfField depthOfFieldSettings;
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
            postProcessProfile = Camera.main.GetComponent<PostProcessVolume>().profile;

            if (!postProcessProfile.TryGetSettings(out depthOfFieldSettings))
            {
                Debug.LogError("ClearScreenScript.cs: Depth of field setting not found!");
            }
        }

        // Clear depth of field effect
        depthOfFieldSettings.aperture.value = 32f;
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
