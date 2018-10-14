﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealth : MonoBehaviour
{
    public int health;

    private bool alive;

	void Start ()
    {
        alive = true;
	}

    void OnTriggerEnter(Collider other)
    {
        // Ignore if other is sibling
        if (other.tag == tag)
        {
            return;
        }

        Debug.Log(other.name + " entered");
        var health = other.transform.parent.GetComponentInChildren<ShipHealth>();
        health.takeDamage();
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
        transform.parent.gameObject.SetActive(false);
        print(transform.parent.name + "has died!");
    }
}
