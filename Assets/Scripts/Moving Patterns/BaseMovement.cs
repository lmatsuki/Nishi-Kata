using NishiKata.Managers;
using UnityEngine;

public abstract class BaseMovement : MonoBehaviour
{
    public bool canMove;

    protected Vector3 startingPosition;
    protected bool inPositionToMove;

    protected void EnsureMovesWhenReady()
    {
        if (!canMove || LevelManager.instance.isPaused)
        {
            inPositionToMove = false;
        }
    }
}
