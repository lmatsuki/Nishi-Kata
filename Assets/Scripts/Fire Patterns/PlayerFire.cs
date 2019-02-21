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

            //GameObject bulletPrefab = Instantiate(bullet, firePosition.position, firePosition.rotation);
            print("fireRot: " + firePosition.rotation.ToString());
            print("localfireRot: " + firePosition.localRotation.ToString());

            PlayerBulletPooler.current.Spawn(firePosition.position, firePosition.rotation);
            nextFireTime = Time.time + fireRate;
        }
    }
}
