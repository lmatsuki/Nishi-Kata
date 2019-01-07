using UnityEngine;

public class OrbitCircular : BaseMovement
{
    public float RotateSpeed;
    public float Radius;

    private Vector3 center;
    private float angle;

    void Start()
    {
        center = transform.position;
    }

    void FixedUpdate()
    {
        angle += RotateSpeed * Time.deltaTime;

        Vector3 offset = new Vector3(Mathf.Sin(angle), 0, Mathf.Cos(angle)) * Radius;
        Vector3 offsetPosition = center + offset;
        transform.position = new Vector3(offsetPosition.x, transform.position.y, offsetPosition.z);
    }
}
