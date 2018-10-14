using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController2D : MonoBehaviour {

	public float speed;
    public Transform characterTransform;

    void Start()
    {
        
    }

	void FixedUpdate () 
	{
        float horizontalMovement = handleHorizontalInput();
        float verticalMovement = handleVerticalInput();
        characterTransform.Translate(horizontalMovement, 0, verticalMovement);

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
