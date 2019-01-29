using UnityEngine;

public class Breakout : BaseMovement
{
    public float speed;

    private new Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        RotateRandom();
    }

    void FixedUpdate()
    {
        if (!canMove)
        {
            return;
        }

        rigidbody.velocity = transform.forward * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Wall))
        {
            ChangeDirection();
        }
    }

    private void RotateRandom()
    {
        transform.Rotate(0f, Random.Range(0, 360), 0f);
    }

    private void ChangeDirection()
    {
        // get the angle the enemy is facing
        // 0 -> 90 && 180 -> 270 = turn right
        // 90 -> 180 && 270 -> 360 = turn left
    }
}
