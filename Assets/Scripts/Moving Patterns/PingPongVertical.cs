using UnityEngine;

public class PingPongVertical : BaseMovement
{
    public float speed;
    public float distance;
    public float deltaBufferSize;

	void Start ()
    {
        startingPosition = transform.position;
	}
	
	void FixedUpdate ()
    {
        float currentStep = Mathf.Sin(Time.time * speed);

        if (canMove && inPositionToMove)
        {
            Vector3 newPosition = GetNewPosition();
            newPosition.z += distance * currentStep;
            transform.position = newPosition;
        }
        else if (!inPositionToMove)
        {
            float delta = Mathf.Abs((distance * currentStep) - transform.position.z + startingPosition.z);

            if (delta < deltaBufferSize)
            {
                inPositionToMove = true;
            }
        }

        EnsureMovesWhenReady();
    }

    protected Vector3 GetNewPosition()
    {
        Vector3 newPosition = transform.position;
        newPosition.z = startingPosition.z;
        return newPosition;
    }
}
