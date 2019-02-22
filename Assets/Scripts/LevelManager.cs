using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> itemsToDisable;
    public List<GameObject> itemsToEnable;
    public float waittimeBeforeLoad;

    public string[] levelThemeOverride;

    public static LevelManager instance;
    private ShipHealth playerShip;
    private IPlayerMovement playerMovement;
    private BaseFire playerFire;

    private ShipHealth lastEnemyShip;
    private GameObject lastEnemy;
    private BaseFire lastEnemyFire;

    private GameObject victoryText;
    private GameObject defeatText;
    private ScreenFade screenFade;
    private bool levelBeat;
    private bool levelLost;
    private bool loadNextLevel;
    private bool returnToMenu;

    void Awake()
    {
        if (!IsMenuScene())
        {
            AssignPlayerComponents();
            AssignLastEnemyComponents();

            // Find inactive GameObjects
            GameObject canvas = GameObject.Find(Names.Canvas);
            victoryText = canvas.transform.Find(Names.VictoryText).gameObject;
            defeatText = canvas.transform.Find(Names.DefeatText).gameObject;

            screenFade = Camera.main.GetComponent<ScreenFade>();
            screenFade.SetScreenFade(false);
        }

        PlayThemeSong();
        
        if (instance != null)
        {
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
    }

    void AssignPlayerComponents()
    {
        GameObject player = GameObject.FindGameObjectWithTag(Tags.Player);
        
        if (player == null)
        {
            Debug.LogError("GameObject with Tag Player not found in LevelManager.cs!");
            return;
        }

        playerShip = player.GetComponentInChildren<ShipHealth>();
        playerMovement = player.GetComponent<IPlayerMovement>();
        playerFire = player.GetComponent<PlayerFire>();
    }

    void AssignLastEnemyComponents()
    {
        lastEnemy = GameObject.FindGameObjectWithTag(Tags.LastEnemy);

        if (lastEnemy == null)
        {
            Debug.LogError("GameObject with Tag LastEnemy not found in LevelManager.cs!");
            return;
        }

        lastEnemyShip = lastEnemy.GetComponentInChildren<ShipHealth>();
        lastEnemyFire = lastEnemy.GetComponent<BaseFire>();
    }

	void Update()
    {
        if (lastEnemyShip == null || playerShip == null)
        {
            return;
        }

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
            StartCoroutine(WaitBeforeNextLevel());
        }
        else if (levelLost)
        {
            StartCoroutine(WaitBeforeReturnToMenu());
        }

        if (loadNextLevel)
        {
            loadNextLevel = false;
            LoadNextLevel();
        }
        else if (returnToMenu)
        {
            returnToMenu = false;
            ReturnToMenu();
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
        playerMovement.SetCanMove(false);

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
        if (playerMovement != null)
        {
            playerMovement.GetRigidbody().velocity = Vector3.zero;
        }
        
        if (lastEnemy != null)
        {
            lastEnemy.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
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
        if (!string.IsNullOrEmpty(AudioManager.instance.GetCurrentlyPlayingSongName()))
        {
            return;
        }

        int currentBuildIndex = SceneManager.GetActiveScene().buildIndex;

        if (levelThemeOverride != null &&
            levelThemeOverride.Length > currentBuildIndex &&
            !string.IsNullOrEmpty(levelThemeOverride[currentBuildIndex]))
        {
            AudioManager.instance.PlaySong(levelThemeOverride[currentBuildIndex]);
        }
        else
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

    private void LoadNextLevel()
    {
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

    private bool IsMenuScene()
    {
        return SceneManager.GetActiveScene().buildIndex == 0;
    }

    IEnumerator LoadSceneAsync(int buildIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(buildIndex);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    IEnumerator WaitBeforeNextLevel()
    {       
        yield return new WaitForSeconds(waittimeBeforeLoad);
        loadNextLevel = true;
    }

    IEnumerator WaitBeforeReturnToMenu()
    {
        yield return new WaitForSeconds(waittimeBeforeLoad);
        returnToMenu = true;
    }

    private void ReturnToMenu()
    {
        // Load the menu
        StopCurrentlyPlayingSong();
        StartCoroutine(LoadSceneAsync(0));
    }
}
