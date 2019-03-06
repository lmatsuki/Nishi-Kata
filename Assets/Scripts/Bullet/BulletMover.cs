using UnityEngine;

public class BulletMover : MonoBehaviour
{
    public float speed;

    private Rigidbody bulletRigidbody;

    void Start()
    {
        MoveForward();
    }

    public void MoveForward()
    {
        if (bulletRigidbody == null)
        {
            bulletRigidbody = GetComponentInChildren<Rigidbody>();
        }

        bulletRigidbody.velocity = transform.forward * speed;
    }
}
