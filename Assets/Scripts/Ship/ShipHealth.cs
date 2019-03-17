using EZCameraShake;
using NishiKata.Audio;
using NishiKata.Utilities;
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
    public bool screenShakeOnDeath;
    public GameObject onDeathParticleFX;
    public string hitSoundName;
    public GameObject onHitParticleFX;

    private Color initialColor;
    private bool takingDamage;
    private float currentSmoothTime;
    private bool alive;
    private GameObject currentOnHitFX;

    void Start()
    {
        alive = true;

        if (renderer.material.HasProperty("_Color"))
        {
            initialColor = renderer.material.color;
        }
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

        // Enemy bullet
        if (other.tag.Contains(Tags.Bullet) && !other.tag.Contains(tag))
        {
            TakeDamage();
            other.transform.parent.gameObject.SetActive(false);
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
            PlayOnHitSoundEffect();
            PlayOnHitParticleEffect();

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
            AudioManager.instance.Play(deathSoundName);
        }

        ShakeScreenOnDeath();
        PlayOnDeathParticleEffect();
        alive = false;
        print(transform.parent.name + " has died!");
    }

    void ShakeScreenOnDeath()
    {
        if (screenShakeOnDeath)
        {
            CameraShaker.Instance.ShakeOnce(3f, 5f, 0, 1.5f);
        }
    }

    void PlayOnHitSoundEffect()
    {
        if (!string.IsNullOrEmpty(hitSoundName))
        {
            AudioManager.instance.Play(hitSoundName);
        }
    }

    void PlayOnHitParticleEffect()
    {
        if (onHitParticleFX == null)
        {
            return;
        }

        // Use currentOnHitFX to keep track of currently playing on hit particle system
        if (currentOnHitFX != null)
        {
            Destroy(currentOnHitFX);
        }

        currentOnHitFX = Instantiate(onHitParticleFX, transform) as GameObject;
    }

    void PlayOnDeathParticleEffect()
    {
        if (onDeathParticleFX == null)
        {
            return;
        }

        Instantiate(onDeathParticleFX, transform.position, Quaternion.identity);
    }

    public bool IsAlive()
    {
        return alive;
    }
}
