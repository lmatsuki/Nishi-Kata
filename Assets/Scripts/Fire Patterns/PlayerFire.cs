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
            PlayerBulletPooler.current.Spawn(firePosition.position, firePosition.rotation);
            nextFireTime = Time.time + fireRate;
        }
    }
}
