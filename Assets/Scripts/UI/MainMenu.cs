using NishiKata.Audio;
using NishiKata.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NishiKata.UI
{
    public class MainMenu : MonoBehaviour
    {
        void Start()
        {
            AudioManager.instance.PlaySong(Songs.MenuTheme);
        }

        public void PlayGame()
        {
            AudioManager.instance.StopSong(Songs.MenuTheme);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void QuitGame()
        {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
        }
    }
}
