using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController2D : MonoBehaviour {

	public float speed;

    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponentInChildren<Rigidbody>();
    }

	void FixedUpdate () 
	{
        //float horizontalMovement = handleHorizontalInputForce();
        //float verticalMovement = handleVerticalInputForce();

        //rigidbody.AddForce(horizontalMovement * speed, 0, verticalMovement * speed, ForceMode.Acceleration);

        handleHorizontalInput();
        handleVerticalInput();
    }

    float handleHorizontalInputForce()
    {
        float horizontalMovement = 0.0f;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            horizontalMovement = -speed;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            horizontalMovement = speed;
        }

        return horizontalMovement;
        //rigidbody.AddForce(transform.right * horizontalMovement);
    }

    float handleVerticalInputForce()
    {
        float verticalMovement = 0.0f;

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            verticalMovement = speed;
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            verticalMovement = -speed;
        }

        return verticalMovement;
        //rigidbody.AddForce(transform.forward * verticalMovement);
    }

    void handleHorizontalInput()
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

        transform.Translate(horizontalMovement, 0, 0);
    }

    void handleVerticalInput()
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

        transform.Translate(0, 0, verticalMovement);
    }
}
