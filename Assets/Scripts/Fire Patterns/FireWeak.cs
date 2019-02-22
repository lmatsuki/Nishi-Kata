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
            GameObject bulletPrefab = FirePooledBulletByName(bullets[currentBulletIndex].name, firePosition.position, firePosition.rotation);
            AimBullet(bulletPrefab.transform);

            // Need to move the bullet AFTER the change in direction/forward
            MoveBullet(bulletPrefab);
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

    GameObject FirePooledBulletByName(string bulletName, Vector3 position, Quaternion rotation)
    {
        switch (bulletName)
        {
            case Names.WeakBullet:
                return WeakBulletPooler.current.Spawn(position, rotation);
            case Names.StrongBullet:
                return StrongBulletPooler.current.Spawn(position, rotation);
            default:
                return null;
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
