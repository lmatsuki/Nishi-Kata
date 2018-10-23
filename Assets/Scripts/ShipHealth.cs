using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipHealth : MonoBehaviour
{
    public int health;
    public float blinkTime;

    private Renderer renderer;
    private Color initialColor;
    private bool alive;

	void Start ()
    {
        alive = true;
        renderer = transform.parent.GetComponentInChildren<Renderer>();
        initialColor = renderer.material.color;
	}

    void OnTriggerEnter(Collider other)
    {
        // Ignore if other is sibling
        if (other.tag == tag)
        {
            return;
        }

        Debug.Log(other.name + " entered " + transform.parent.name);
        // Enemy bullet
        if (other.tag.Contains("Bullet") && !other.tag.Contains(tag))
        {
            takeDamage();
            Destroy(other.transform.parent.gameObject);
        }

        // Physical collision with ship
        var otherHealth = other.transform.parent.GetComponentInChildren<ShipHealth>();
        if (otherHealth != null)
        {
            otherHealth.takeDamage();
        }
    }

    void takeDamage()
    {
        if (health > 0 && alive)
        {
            health--;
            StartCoroutine("blinkEffect");

            if (health == 0)
            {
                die();
            }
        }
    }

    IEnumerator blinkEffect()
    {
        renderer.material.color = Color.white;
        yield return new WaitForSeconds(blinkTime);
        renderer.material.color = initialColor;
    }

    void die()
    {
        alive = false;
        transform.parent.gameObject.SetActive(false);
        print(transform.parent.name + " has died!");
    }
}
