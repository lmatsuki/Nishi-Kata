using UnityEngine;

public class InputController2D : BaseMovement
{
	public float speed;
    public Transform characterTransform;
    public Transform firePosition;
    public GameObject bullet;
    public float fireRate;

    private new Rigidbody rigidbody;
    private float nextFireTime;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

	void FixedUpdate () 
	{
        if (!canMove)
        {
            return;
        }

        float horizontalMovement = handleHorizontalInput();
        float verticalMovement = handleVerticalInput();
        Vector3 movement = new Vector3(horizontalMovement, 0.0f, verticalMovement);
        rigidbody.velocity = movement * speed;
    }

    void Update()
    {
        handleFireInput();
    }

    float handleHorizontalInput()
    {
        float horizontalMovement = 0.0f;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            horizontalMovement = -speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            horizontalMovement = speed * Time.deltaTime;
        }

        return horizontalMovement;
    }

    float handleVerticalInput()
    {
        float verticalMovement = 0.0f;

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            verticalMovement = speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            verticalMovement = -speed * Time.deltaTime;
        }

        return verticalMovement;
    }

    void handleFireInput()
    {
        if (Input.GetKey(KeyCode.Space) && Time.time > nextFireTime)
        {
            print("Fire!");
            GameObject bulletPrefab = Instantiate(bullet, firePosition.position, firePosition.rotation);
            nextFireTime = Time.time + fireRate;
        }
    }
}
