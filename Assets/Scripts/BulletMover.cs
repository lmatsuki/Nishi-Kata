using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMover : MonoBehaviour
{
    public float speed;

	void Start ()
    {
        Rigidbody rigidbody = GetComponentInChildren<Rigidbody>();
        rigidbody.velocity = transform.forward * speed;
	}
}
