using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> itemsToDisable;
    public List<GameObject> itemsToEnable;
    public float waittimeBeforeLoad;

    public ShipHealth playerShip;
    public BaseMovement playerMovement;
    public BaseFire playerFire;

    public ShipHealth lastEnemyShip;
    public GameObject lastEnemy;
    public BaseFire lastEnemyFire;

    public string[] levelThemeOverride;

    private GameObject victoryText;
    private GameObject defeatText;
    private ScreenFade screenFade;
    private bool levelBeat;
    private bool levelLost;

	void Start()
    {
        // Find inactive GameObjects
        GameObject canvas = GameObject.Find(Names.Canvas);
        victoryText = canvas.transform.Find(Names.VictoryText).gameObject;
        defeatText = canvas.transform.Find(Names.DefeatText).gameObject;

        screenFade = Camera.main.GetComponent<ScreenFade>();
        screenFade.SetScreenFade(false);
        PlayThemeSong();
	}

	void Update()
    {
		if (!lastEnemyShip.IsAlive() || !playerShip.IsAlive())
        {
            DisableItems();
            DisableMovement();
            StopVelocity();
            DisableAttacks();
            EnableItems();
            DestroyAllBullets();

            screenFade.SetScreenFade(true);
            DisplayVictoryOrDefeatText();
        }

        if (levelBeat)
        {
            StartCoroutine(LoadNextLevel());
        }
        else if (levelLost)
        {
            StartCoroutine(ReturnToMenu());
        }
	}

    void DisableItems()
    {
        if (itemsToDisable.Count <= 0)
        {
            return;
        }

        for (int i = 0; i < itemsToDisable.Count; i++)
        {
            itemsToDisable[i].SetActive(false);
        }
    }

    void EnableItems()
    {
        if (itemsToEnable.Count <= 0)
        {
            return;
        }

        for (int i = 0; i < itemsToEnable.Count; i++)
        {
            itemsToEnable[i].SetActive(true);
        }
    }

    void DisableMovement()
    {
        playerMovement.canMove = false;

        if (lastEnemy != null)
        {
            BaseMovement[] movements = lastEnemy.GetComponents<BaseMovement>();

            for (int i = 0; i < movements.Length; i++)
            {
                movements[i].canMove = false;
            }
        }
    }

    void StopVelocity()
    {
        playerMovement.GetComponent<Rigidbody>().velocity = Vector3.zero;
        lastEnemy.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    void DisableAttacks()
    {
        playerFire.canFire = false;
        lastEnemyFire.canFire = false;
    }

    void DisplayVictoryOrDefeatText()
    {
        if (!lastEnemyShip.IsAlive())
        {
            victoryText.SetActive(true);
            levelBeat = true;
        }
        else
        {
            defeatText.SetActive(true);
            levelLost = true;
        }
    }

    void DestroyAllBullets()
    {
        UtilityExtensions.DestroyObjectsByTag(Tags.PlayerBullet);
        UtilityExtensions.DestroyObjectsByTag(Tags.EnemyBullet);
    }

    void PlayThemeSong()
    {
        if (string.IsNullOrEmpty(AudioManager.instance.GetCurrentlyPlayingSongName()))
        {
            AudioManager.instance.PlaySong(Songs.PlayTheme);
        }
    }

    void StopCurrentlyPlayingSong()
    {
        string currentSongName = AudioManager.instance.GetCurrentlyPlayingSongName();

        if (!string.IsNullOrEmpty(currentSongName))
        {
            AudioManager.instance.StopSong(currentSongName);
        }
    }

    void CheckLevelThemeOverride(int buildIndex)
    {
        if (levelThemeOverride != null &&
            levelThemeOverride.Length > buildIndex &&
            !string.IsNullOrEmpty(levelThemeOverride[buildIndex]))
        {
            AudioManager.instance.StopSong(AudioManager.instance.GetCurrentlyPlayingSongName());
            AudioManager.instance.PlaySong(levelThemeOverride[buildIndex]);
        }
    }

    IEnumerator LoadSceneAsync(int buildIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(buildIndex);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(waittimeBeforeLoad);

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            int nextBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;
            CheckLevelThemeOverride(nextBuildIndex);

            StartCoroutine(LoadSceneAsync(nextBuildIndex));
        }
        else
        {
            // Load the menu
            StopCurrentlyPlayingSong();
            StartCoroutine(LoadSceneAsync(0));
        }
    }

    IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(waittimeBeforeLoad);

        // Load the menu
        StopCurrentlyPlayingSong();
        StartCoroutine(LoadSceneAsync(0));
    }
}
