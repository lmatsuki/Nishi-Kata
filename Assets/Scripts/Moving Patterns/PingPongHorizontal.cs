using UnityEngine;

public class PingPongHorizontal : BaseMovement
{
    public float speed;
    public float distance;
    public float deltaBufferSize;

	void Start ()
    {
        inPositionToMove = true;
        startingPosition = transform.position;
	}
	
	void FixedUpdate ()
    {
        float currentStep = Mathf.Sin(Time.time * speed);

        if (canMove && inPositionToMove)
        {
            Vector3 newPosition = GetNewPosition();
            newPosition.x += distance * currentStep;
            transform.position = newPosition;
        }
        else if (!inPositionToMove)
        {
            float delta = Mathf.Abs((distance * currentStep) - transform.position.x + startingPosition.x);

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
        newPosition.x = startingPosition.x;
        return newPosition;
    }
}
