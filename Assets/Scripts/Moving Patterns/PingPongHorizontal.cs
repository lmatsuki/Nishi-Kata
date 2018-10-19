using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingPongHorizontal : MonoBehaviour
{
    public bool moving;
    public float speed;
    public float distance;

    private Vector3 startingPosition;

	void Start ()
    {
        startingPosition = transform.position;
	}
	
	void FixedUpdate ()
    {
        if (moving)
        {
            Vector3 newPosition = startingPosition;
            newPosition.x += distance * Mathf.Sin(Time.time * speed);
            transform.position = newPosition;
        }
    }
}
