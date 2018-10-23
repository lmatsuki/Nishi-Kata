using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        print("------ TAG: " + other.tag);
    }
}
