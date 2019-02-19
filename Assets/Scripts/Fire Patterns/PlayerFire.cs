using UnityEngine;

public class PlayerFire : BaseFire
{
    public Transform firePosition;
    public GameObject bullet;
    public float fireRate;

    private float nextFireTime;
    public IObjectPool bulletObjectPool;

    void Start()
    {
        bulletObjectPool = gameObject.GetComponentInChildren<IObjectPool>();
        bulletObjectPool.Initialize(bullet);
    }

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
            print("firePos: " + firePosition.position.ToString());
            print("localfirePos: " + firePosition.localPosition.ToString());
            bulletObjectPool.Spawn(firePosition.position, firePosition.rotation);
            nextFireTime = Time.time + fireRate;
        }
    }
}
