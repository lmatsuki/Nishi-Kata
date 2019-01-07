using UnityEngine;

public abstract class BaseMovement : MonoBehaviour
{
    public bool canMove;

    protected Vector3 startingPosition;
    protected bool inPositionToMove;

    protected void EnsureMovesWhenReady()
    {
        if (!canMove)
        {
            inPositionToMove = false;
        }
    }
}
