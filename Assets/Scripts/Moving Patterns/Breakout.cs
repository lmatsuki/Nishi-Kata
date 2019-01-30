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
            ChangeDirection(other.name);
        }
    }

    private void RotateRandom()
    {
        transform.Rotate(0f, Random.Range(120f, 240f), 0f);
    }

    private void ChangeDirection(string wallName)
    {
        if (wallName == Names.LeftWall ||
            wallName == Names.RightWall)
        {
            transform.rotation = Quaternion.Inverse(transform.rotation);
        }
        else if (wallName == Names.TopWall ||
            wallName == Names.BottomWall)
        {
            transform.rotation *= Quaternion.Euler(0, 180f, 0);
            transform.rotation = Quaternion.Inverse(transform.rotation);
        }
    }
}
