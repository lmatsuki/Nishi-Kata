using UnityEngine;

public class AndroidPlayerMovement : BaseMovement, IPlayerMovement
{
    public float movementSpeed;
    public float movementBuffer;

    private new Rigidbody rigidbody;
    private Joystick movementJoystick;
    public Joystick rotationJoystick;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        AssignJoysticks();
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
        HandleRotationInput();
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

    void HandleRotationInput()
    {
        float heading = Mathf.Atan2(rotationJoystick.Horizontal, rotationJoystick.Vertical);
        transform.rotation = Quaternion.Euler(0f, heading * Mathf.Rad2Deg, 0f);
    }

    void AssignJoysticks()
    {
        FixedJoystick[] joysticks = Camera.main.gameObject.GetComponentsInChildren<FixedJoystick>();

        for (int i = 0; i < joysticks.Length; i++)
        {
            if (joysticks[i].gameObject.name == Names.MovementJoystick)
            {
                movementJoystick = joysticks[i];
            }
            else if (joysticks[i].gameObject.name == Names.RotationJoystick)
            {
                rotationJoystick = joysticks[i];
            }
        }
    }
}
