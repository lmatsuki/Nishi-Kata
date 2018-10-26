using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBoundary : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet") || other.CompareTag("EnemyBullet"))
        {
            // Replace with Object pooler's remove method
            Destroy(other.transform.parent.gameObject);
        }
    }
}
