using UnityEngine;

public class PlayerFire : BaseFire
{
    public Transform firePosition;
    public GameObject bullet;
    public float fireRate;

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
            print("Fire!");
            GameObject bulletPrefab = Instantiate(bullet, firePosition.position, firePosition.rotation);
            nextFireTime = Time.time + fireRate;
        }
    }
}
