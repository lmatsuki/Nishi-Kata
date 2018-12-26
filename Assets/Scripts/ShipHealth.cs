using System.Collections;
using UnityEngine;

public class ShipHealth : MonoBehaviour
{
    public int health;
    public float blinkTime;
    public float smoothVelocity;
    public Renderer renderer;
    public GameObject[] healthParts;
    public string deathSoundName;

    private Color initialColor;
    private bool takingDamage;
    private float currentSmoothTime;
    private bool alive;
    private AudioManager audioManager;

    void Start()
    {
        alive = true;
        initialColor = renderer.material.color;
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        if (takingDamage)
        {
            FadeWhiteEffect();
        }
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
        ShipHealth otherHealth = other.transform.parent.GetComponentInChildren<ShipHealth>();
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
            takingDamage = true;
            currentSmoothTime = 0;
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

    void FadeWhiteEffect()
    {
        currentSmoothTime = Mathf.SmoothDamp(currentSmoothTime, 1, ref smoothVelocity, blinkTime);
        renderer.material.color = Color.Lerp(Color.white, initialColor, currentSmoothTime);

        if (Mathf.Approximately(currentSmoothTime, 1))
        {
            takingDamage = false;
        }
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
        if (!string.IsNullOrEmpty(deathSoundName))
        {
            audioManager.Play(deathSoundName);
        }

        alive = false;
        print(transform.parent.name + " has died!");
    }

    public bool IsAlive()
    {
        return alive;
    }
}
