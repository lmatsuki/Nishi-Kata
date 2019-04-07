using NishiKata.Audio;
using NishiKata.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NishiKata.UI
{
    public class PauseMenu : MonoBehaviour
    {
        public GameObject pauseMenu;

        private string songName;

        void Update()
        {
            if (PressedPause())
            {
                pauseMenu.SetActive(!pauseMenu.activeSelf);
                LevelManager.instance.isPaused = pauseMenu.activeSelf;

                if (pauseMenu.activeSelf)
                {
                    songName = AudioManager.instance.GetCurrentlyPlayingSongName();
                    AudioManager.instance.PauseSong(songName);
                }
                else
                {
                    AudioManager.instance.PlaySong(songName);
                }
            }
        }

        private bool PressedPause()
        {
            return Input.GetKeyUp(KeyCode.Escape);
        }

        public void ResumeGame()
        {
            pauseMenu.SetActive(true);
            LevelManager.instance.isPaused = false;
            AudioManager.instance.PlaySong(songName);
        }

        public void ReturnToMenu()
        {
            LevelManager.instance.isPaused = false;
            SceneManager.LoadScene(0);
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