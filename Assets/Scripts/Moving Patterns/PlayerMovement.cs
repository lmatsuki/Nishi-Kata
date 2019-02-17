using UnityEngine;

public class PlayerMovement : BaseMovement
{
	public float movementSpeed;
    public float rotationSpeed;
    public Transform characterTransform;

    private new Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

	void FixedUpdate() 
	{
        if (!canMove)
        {
            return;
        }

        float horizontalMovement = HandleHorizontalInput();
        float verticalMovement = HandleVerticalInput();
        Vector3 movement = new Vector3(horizontalMovement, 0.0f, verticalMovement);
        rigidbody.velocity = movement * movementSpeed;
        HandleRotationInput();
    }

    float HandleHorizontalInput()
    {
        float horizontalMovement = 0.0f;

        if (Input.GetKey(KeyCode.A))
        {
            horizontalMovement = -movementSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontalMovement = movementSpeed * Time.deltaTime;
        }

        return horizontalMovement;
    }

    float HandleVerticalInput()
    {
        float verticalMovement = 0.0f;

        if (Input.GetKey(KeyCode.W))
        {
            verticalMovement = movementSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            verticalMovement = -movementSpeed * Time.deltaTime;
        }

        return verticalMovement;
    }

    void HandleRotationInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(new Vector3(0, -rotationSpeed * Time.deltaTime, 0));
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
        }
    }
}
