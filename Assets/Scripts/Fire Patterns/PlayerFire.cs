using UnityEngine;

public class PlayerFire : BaseFire
{
    public Transform firePosition;
    public GameObject bullet;
    public float fireRate;
    public AudioSource fireSound;

    private float nextFireTime;

    protected override void Update()
    {
        base.Update();

        handleFireInput();
    }

    void handleFireInput()
    {
        if (Input.GetKey(KeyCode.Space) &&
            canFire && 
            Time.time > nextFireTime)
        {
            if (fireSound != null)
            {
                fireSound.Play();
            }
            
            GameObject bulletPrefab = Instantiate(bullet, firePosition.position, firePosition.rotation);
            nextFireTime = Time.time + fireRate;
        }
    }
}
