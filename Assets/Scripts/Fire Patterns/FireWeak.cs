using UnityEngine;

public class FireWeak : BaseFire
{
    public GameObject[] bullets;
    public Transform firePosition;
    public float fireRate;
    public float timeBetweenFire;
    public float[] individualDelay;

    private float nextBulletTime;
    private float nextFireTime;
    private int currentBulletIndex;
    private BaseBulletTargeter bulletTargeter;

    void Start ()
    {
        currentBulletIndex = 0;
        bulletTargeter = GetComponent<BaseBulletTargeter>();
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
            print("Bullet name: " + bullets[currentBulletIndex].name);
            GameObject bulletPrefab = Instantiate(bullets[currentBulletIndex], firePosition.position, firePosition.rotation);
            AimBullet(bulletPrefab.transform);
            AudioManager.instance.Play(Sounds.EnemyFire);

            // Keep track of current bullet
            currentBulletIndex = (currentBulletIndex + 1) % bullets.Length;
            nextBulletTime = Time.time + fireRate + AddIndividualDelay();
        }
        
        // Indicates that all bullets have been fired
        if (currentBulletIndex == 0)
        {
            nextFireTime = Time.time + timeBetweenFire;
        }
    }

    void AimBullet(Transform bullet)
    {
        if (bulletTargeter != null)
        {
            bulletTargeter.TargetBullet(bullet);
        }
    }

    float AddIndividualDelay()
    {
        if (individualDelay != null &&
            individualDelay.Length > currentBulletIndex)
        {
            return individualDelay[currentBulletIndex];
        }

        return 0f;
    }
}
