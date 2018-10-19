using UnityEngine;

public class PingPongHorizontal : MonoBehaviour
{
    public bool isMoving;
    public float speed;
    public float distance;
    public float deltaBufferSize;

    private Vector3 startingPosition;
    private bool isReadyToMove;

	void Start ()
    {
        startingPosition = transform.position;
	}
	
	void FixedUpdate ()
    {
        float currentStep = Mathf.Sin(Time.time * speed);

        if (isMoving && isReadyToMove)
        {
            Vector3 newPosition = startingPosition;
            newPosition.x += distance * currentStep;
            transform.position = newPosition;
        }
        else if (!isReadyToMove)
        {
            float delta = Mathf.Abs((distance * currentStep) - transform.position.x);
            if (delta < deltaBufferSize)
            {
                isReadyToMove = true;
            }
        }

        EnsureMovesWhenReady();
    }

    void EnsureMovesWhenReady()
    {
        if (!isMoving)
        {
            isReadyToMove = false;
        }
    }
}
