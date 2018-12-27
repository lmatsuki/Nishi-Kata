using UnityEngine;

public class FireWeak : BaseFire
{
    public Transform player;
    public GameObject[] bullets;
    public Transform firePosition;
    public float fireRate;
    public float timeBetweenFire;

    private float nextBulletTime;
    private float nextFireTime;
    private int currentBulletIndex;

    void Start ()
    {
        currentBulletIndex = 0;
    }
	
	protected override void Update()
    {
        base.Update();

		if (canFire && IsInitialDelayOver() && 
            Time.time > nextFireTime)
        {
            FireShot();
        }
	}

    void FireShot()
    {        
        if (Time.time > nextBulletTime)
        {
            GameObject bulletPrefab = Instantiate(bullets[currentBulletIndex], firePosition.position, firePosition.rotation);
            bulletPrefab.transform.LookAt(player);
            AudioManager.instance.Play(Sounds.EnemyFire);

            // Keep track of current bullet
            currentBulletIndex = (currentBulletIndex + 1) % bullets.Length;
            nextBulletTime = Time.time + fireRate;
        }
        
        // Indicates that all bullets have been fired
        if (currentBulletIndex == 0)
        {
            nextFireTime = Time.time + timeBetweenFire;
        }
    }
}
