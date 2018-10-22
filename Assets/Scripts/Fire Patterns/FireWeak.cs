using UnityEngine;

public class FireWeak : MonoBehaviour
{
    public Transform player;
    public GameObject bullet;
    public Transform firePosition;
    public bool canFire;
    public float fireRate;

    private float nextFireTime;

	void Start ()
    {
		
	}
	
	void Update ()
    {
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
