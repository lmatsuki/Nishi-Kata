using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> itemsToDisable;
    public List<GameObject> itemsToEnable;
    public GameObject victoryText;
    public GameObject defeatText;

    public ShipHealth playerShip;
    public BaseMovement playerMovement;
    public BaseFire playerFire;

    public ShipHealth lastEnemyShip;
    public GameObject lastEnemy;
    public BaseFire lastEnemyFire;

    private ScreenFade screenFade;
    private bool levelBeat;

	void Start()
    {
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
        }
    }

    void DestroyAllBullets()
    {
        UtilityExtensions.DestroyObjectsByTag(Tags.PlayerBullet);
        UtilityExtensions.DestroyObjectsByTag(Tags.EnemyBullet);
    }

    void PlayThemeSong()
    {
        AudioManager.instance.PlaySong(Songs.PlayTheme);
    }

    void StopThemeSong()
    {
        AudioManager.instance.StopSong(Songs.PlayTheme);
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
        yield return new WaitForSeconds(1.5f);

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            int nextBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;
            StartCoroutine(LoadSceneAsync(nextBuildIndex));
        }
        else
        {
            // Load the menu
            StopThemeSong();
            StartCoroutine(LoadSceneAsync(0));
        }
    }
}
