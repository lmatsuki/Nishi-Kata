using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public List<GameObject> itemsToDisable;
    public ShipHealth playerShip;
    public BaseMovement playerMovement;
    public ShipHealth lastEnemyShip;
    public BaseMovement lastEnemyMovement;

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
            print("DISABLE SHIT NAO");
            DisableItems();
            playerMovement.canMove = false;
            lastEnemyMovement.canMove = false;
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
}
