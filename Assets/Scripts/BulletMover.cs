using UnityEngine;

public class BulletMover : MonoBehaviour
{
    public float speed;

	void OnEnable ()
    {
        Rigidbody rigidbody = GetComponentInChildren<Rigidbody>();
        rigidbody.velocity = transform.forward * speed;
	}
}
