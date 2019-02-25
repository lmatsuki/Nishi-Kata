using UnityEngine;

public class AndroidPlayerMovement : BaseMovement, IPlayerMovement
{
    public float movementSpeed;
    //public float rotationSpeed;
    public float verticalOffset;
    public float movementBuffer;

    private Transform characterTransform;
    private new Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        characterTransform = transform.Find(Names.PlayerPrism);
    }

    public void UpdateMovement()
    {
        if (!canMove)
        {
            return;
        }

        if (Input.touchCount > 0)
        {
            float horizontalMovement = HandleHorizontalInput();
            float verticalMovement = HandleVerticalInput();
            Vector3 movement = new Vector3(horizontalMovement, 0.0f, verticalMovement);
            rigidbody.velocity = movement * movementSpeed;
            //HandleRotationInput();
        }
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
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

        if (LessThanBuffer(touchPosition.x, transform.position.x))
        {
            return horizontalMovement;
        }

        if (touchPosition.x > transform.position.x)
        {
            horizontalMovement = movementSpeed * Time.deltaTime;
        }
        else if (touchPosition.x < transform.position.x)
        {
            horizontalMovement = -movementSpeed * Time.deltaTime;
        }

        return horizontalMovement;
    }

    float HandleVerticalInput()
    {
        float verticalMovement = 0.0f;
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);

        if (LessThanBuffer(touchPosition.z, transform.position.z))
        {
            return verticalMovement;
        }

        if (touchPosition.z + verticalOffset > transform.position.z)
        {
            verticalMovement = movementSpeed * Time.deltaTime;
        }
        else if (touchPosition.z + verticalOffset < transform.position.z)
        {
            verticalMovement = -movementSpeed * Time.deltaTime;
        }

        return verticalMovement;
    }

    bool LessThanBuffer(float touchValue, float transformValue)
    {
        return Mathf.Abs(touchValue - transformValue) <= movementBuffer;
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
