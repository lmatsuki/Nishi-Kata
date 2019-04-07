using NishiKata.Audio;
using UnityEngine;

namespace NishiKata.UI
{
    public class PauseMenu : MonoBehaviour
    {
        public GameObject pauseMenu;

        private string songName;

        void Start()
        {

        }

        void Update()
        {
            if (PressedPause())
            {
                pauseMenu.SetActive(!pauseMenu.activeSelf);

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
    }

}