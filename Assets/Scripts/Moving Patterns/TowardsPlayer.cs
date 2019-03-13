using NishiKata.Utilities;
using UnityEngine;

public class TowardsPlayer : BaseMovement
{
    public float speed;

    private Transform player;
    private new Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag(Tags.Player).transform;
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
