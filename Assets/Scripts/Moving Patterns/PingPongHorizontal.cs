using UnityEngine;

public class PingPongHorizontal : MonoBehaviour
{
    public bool canMove;
    public float speed;
    public float distance;
    public float deltaBufferSize;

    private Vector3 startingPosition;
    private bool inPositionToMove;

	void Start ()
    {
        startingPosition = transform.position;
	}
	
	void FixedUpdate ()
    {
        float currentStep = Mathf.Sin(Time.time * speed);

        if (canMove && inPositionToMove)
        {
            Vector3 newPosition = startingPosition;
            newPosition.x += distance * currentStep;
            transform.position = newPosition;
        }
        else if (!inPositionToMove)
        {
            float delta = Mathf.Abs((distance * currentStep) - transform.position.x);
            if (delta < deltaBufferSize)
            {
                inPositionToMove = true;
            }
        }

        EnsureMovesWhenReady();
    }

    void EnsureMovesWhenReady()
    {
        if (!canMove)
        {
            inPositionToMove = false;
        }
    }
}
