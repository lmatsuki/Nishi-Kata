using UnityEngine;

public interface IObjectPool
{
    void Initialize(GameObject gameObject);

    void Spawn(Vector3 localPosition, Quaternion localRotation);
}