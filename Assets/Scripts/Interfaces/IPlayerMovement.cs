using UnityEngine;

public interface IPlayerMovement
{
    void UpdateMovement();

    Rigidbody GetRigidbody();

    void SetCanMove(bool status);
}
