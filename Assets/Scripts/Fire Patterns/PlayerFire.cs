using UnityEngine;

public class PlayerFire : BaseFire
{
    public Transform firePosition;
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
            canFire && Time.time > nextFireTime)
        {
            AudioManager.instance.Play(Sounds.PlayerFire);
            GameObject bulletPrefab = PlayerBulletPooler.current.Spawn(firePosition.position, firePosition.rotation);
            MoveBullet(bulletPrefab);
            nextFireTime = Time.time + fireRate;
        }
    }

    void MoveBullet(GameObject bulletPrefab)
    {
        BulletMover bulletMover = bulletPrefab.GetComponent<BulletMover>();

        if (bulletMover != null)
        {
            bulletMover.MoveForward();
        }
    }
}
