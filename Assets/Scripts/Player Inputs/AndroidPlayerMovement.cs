using UnityEngine;

public class AndroidPlayerMovement : BaseMovement, IPlayerMovement
{
    public float movementSpeed;
    public float movementBuffer;
    //public float rotationSpeed;

    private new Rigidbody rigidbody;
    private Joystick movementJoystick;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        movementJoystick = Camera.main.gameObject.GetComponentInChildren<FixedJoystick>();
    }

    public void UpdateMovement()
    {
        if (!canMove)
        {
            return;
        }

        float horizontalMovement = HandleHorizontalInput();
        float verticalMovement = HandleVerticalInput();
        Vector3 movement = new Vector3(horizontalMovement, 0.0f, verticalMovement);
        rigidbody.velocity = movement * movementSpeed;
        //HandleRotationInput();
    }

    public Rigidbody GetRigidbody()
    {
        return rigidbody;
    }

    public void SetCanMove(bool status)
    {
        canMove = status;
    }

    float HandleHorizontalInput()
    {
        float horizontalMovement = 0.0f;

        if (movementJoystick.Horizontal - movementBuffer > 0)
        {
            horizontalMovement = movementSpeed * Time.deltaTime;
        }
        else if (movementJoystick.Horizontal + movementBuffer < 0)
        {
            horizontalMovement = -movementSpeed * Time.deltaTime;
        }

        return horizontalMovement;
    }

    float HandleVerticalInput()
    {
        float verticalMovement = 0.0f;

        if (movementJoystick.Vertical - movementBuffer > 0)
        {
            verticalMovement = movementSpeed * Time.deltaTime;
        }
        else if (movementJoystick.Vertical + movementBuffer < 0)
        {
            verticalMovement = -movementSpeed * Time.deltaTime;
        }

        return verticalMovement;
    }

    //void HandleRotationInput()
    //{
    //    if (Input.GetKey(KeyCode.LeftArrow))
    //    {
    //        transform.Rotate(new Vector3(0, -rotationSpeed * Time.deltaTime, 0));
    //    }
    //    else if (Input.GetKey(KeyCode.RightArrow))
    //    {
    //        transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
    //    }
    //}
}
