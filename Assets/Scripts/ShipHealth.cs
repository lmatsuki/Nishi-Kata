using System.Collections;
using UnityEngine;

public class ShipHealth : MonoBehaviour
{
    public int health;
    public float blinkTime;
    public Renderer renderer;
    public GameObject[] healthParts;

    private Color initialColor;
    private bool alive;

	void Start ()
    {
        alive = true;
        //renderer = transform.parent.GetComponentInChildren<Renderer>();
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
        if (other.tag.Contains(Tags.Bullet) && !other.tag.Contains(tag))
        {
            TakeDamage();
            Destroy(other.transform.parent.gameObject);
        }

        // Physical collision with ship
        var otherHealth = other.transform.parent.GetComponentInChildren<ShipHealth>();
        if (otherHealth != null)
        {
            otherHealth.TakeDamage();
        }
    }

    void TakeDamage()
    {
        if (health > 0 && alive)
        {
            health--;
            StartCoroutine(BlinkEffect());
            HideHealthPart();

            if (health == 0)
            {
                Die();
            }
        }
    }

    IEnumerator BlinkEffect()
    {
        renderer.material.color = Color.white;
        yield return new WaitForSeconds(blinkTime);
        renderer.material.color = initialColor;
    }

    void HideHealthPart()
    {
        if (healthParts.Length > 0 && healthParts.Length > health)
        {
            healthParts[health].SetActive(false);
        }
    }

    void Die()
    {
        alive = false;
        print(transform.parent.name + " has died!");
    }
}
