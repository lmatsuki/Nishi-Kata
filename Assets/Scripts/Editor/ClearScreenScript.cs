using UnityEngine;
using UnityEngine.UI;

public class ClearScreenScript : MonoBehaviour
{
    private Image fadeImage;

    public void ClearScreen()
    {
        ClearFadeScreen();
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
