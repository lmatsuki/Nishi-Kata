using UnityEngine;

public class TowardsPlayer : BaseMovement
{
    public float speed;
    public Transform player;

    private new Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!canMove)
        {
            return;
        }

        transform.LookAt(player);
        rigidbody.velocity = transform.forward * speed;
    }
}
