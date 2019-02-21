using UnityEngine;

public class BulletMover : MonoBehaviour
{
    public float speed;

	void OnEnable()
    {
        MoveForward();
    }

    void Start()
    {
        MoveForward();
    }

    private void MoveForward()
    {
        Rigidbody rigidbody = GetComponentInChildren<Rigidbody>();
        rigidbody.velocity = transform.forward * speed;
    }
}
