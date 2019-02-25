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
        if (IsPressingFire() &&
            canFire && Time.time > nextFireTime)
        {
            AudioManager.instance.Play(Sounds.PlayerFire);
            GameObject bulletPrefab = PlayerBulletPooler.current.Spawn(firePosition.position, firePosition.rotation);
            MoveBullet(bulletPrefab);
            nextFireTime = Time.time + fireRate;
        }
    }

    bool IsPressingFire()
    {
        return Input.GetKey(KeyCode.Space) || (Input.touchCount > 0);
    }
}
