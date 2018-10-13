using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealth : MonoBehaviour {

    public int health;

    private bool alive;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void takeDamage()
    {
        if (health > 0 && alive)
        {
            health--;

            if (health == 0)
            {
                die();
            }
        }
    }

    void die()
    {
        alive = false;
        gameObject.SetActive(false);
    }
}
