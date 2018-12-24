using UnityEngine;

public class PlayerMovement : BaseMovement
{
	public float speed;
    public Transform characterTransform;

    private new Rigidbody rigidbody;

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
}
