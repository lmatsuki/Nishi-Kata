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
            DisableAttacks();
            EnableItems();

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

    void PlayThemeSong()
    {
        AudioManager.instance.Play(Songs.PlayTheme);
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(2.0f);

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            // Load the menu
            SceneManager.LoadScene(0);
        }
    }
}
