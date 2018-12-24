﻿using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public List<GameObject> itemsToDisable;
    public List<GameObject> itemsToEnable;
    public GameObject victoryText;
    public GameObject defeatText;

    public ShipHealth playerShip;
    public BaseMovement playerMovement;
    public BaseFire playerFire;

    public ShipHealth lastEnemyShip;
    public BaseMovement lastEnemyMovement;
    public BaseFire lastEnemyFire;

    private ScreenFade screenFade;

	void Start()
    {
        screenFade = GameObject.Find("Main Camera").GetComponent<ScreenFade>();
        screenFade.SetScreenFade(false);
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
            gameObject.SetActive(false);
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
        lastEnemyMovement.canMove = false;
    }

    void DisableAttacks()
    {
        playerFire.canFire = false;

        // Override initial delay
        lastEnemyFire.enabled = false;
        lastEnemyFire.canFire = false;
    }

    void DisplayVictoryOrDefeatText()
    {
        if (!lastEnemyShip.IsAlive())
        {
            victoryText.SetActive(true);
        }
        else
        {
            defeatText.SetActive(true);
        }
    }
}
