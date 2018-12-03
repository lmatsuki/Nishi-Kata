using UnityEngine;

public class FireWeak : BaseEnemy
{
    public Transform player;
    public GameObject bullet;
    public Transform firePosition;
    public float fireRate;

    private float nextFireTime;

	void Start ()
    {
		
	}
	
	protected override void Update()
    {
        base.Update();

		if (canFire && Time.time > nextFireTime)
        {
            FireShot();
        }
	}

    void FireShot()
    {        
        GameObject bulletPrefab = Instantiate(bullet, firePosition.position, firePosition.rotation);
        bulletPrefab.transform.LookAt(player);
        nextFireTime = Time.time + fireRate;
    }
}
