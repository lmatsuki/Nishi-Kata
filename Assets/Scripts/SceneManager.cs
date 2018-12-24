using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public List<GameObject> itemsToDisable;

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

            screenFade.SetScreenFade(true);
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

    void DisableMovement()
    {
        playerMovement.canMove = false;
        lastEnemyMovement.canMove = false;
    }

    void DisableAttacks()
    {
        playerFire.canFire = false;
        lastEnemyFire.canFire = false;
    }
}
