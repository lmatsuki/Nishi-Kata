using UnityEngine;

public class OrbitCircular : BaseMovement
{
    public float rotateSpeed;
    public float radius;

    private Vector3 center;
    private float angle;

    void Start()
    {
        center = transform.position;
    }

    void FixedUpdate()
    {
        angle += rotateSpeed * Time.deltaTime;

        Vector3 offset = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * radius;
        Vector3 offsetPosition = center + offset;
        transform.position = new Vector3(offsetPosition.x, transform.position.y, offsetPosition.z);
    }
}
