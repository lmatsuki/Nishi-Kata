using UnityEngine;

public class PlayerFire : BaseFire
{
    public Transform firePosition;
    public GameObject bullet;
    public float fireRate;

    private float nextFireTime;
    private AudioManager audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
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
            audioManager.Play("PlayerFire");
            
            GameObject bulletPrefab = Instantiate(bullet, firePosition.position, firePosition.rotation);
            nextFireTime = Time.time + fireRate;
        }
    }
}
