using System.Collections.Generic;
using UnityEngine;

public class BulletObjectPool : MonoBehaviour, IObjectPool
{
    public int pooledAmount = 20;

    public IList<GameObject> bullets;

    // Instantiate the GameObjects and fill the pool, nested under this GameObject
    public void Initialize(GameObject gameObject)
    {
        bullets = new List<GameObject>();

        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject bullet = Instantiate(gameObject, transform);
            bullet.SetActive(false);
            bullets.Add(bullet);
        }
    }

    // Set the GameObject active
    public void Spawn(Vector3 localPosition, Quaternion localRotation)
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            if (!bullets[i].activeInHierarchy)
            {
                bullets[i].transform.position = localPosition;
                bullets[i].transform.rotation = localRotation;

                // Also need to set child's local position and rotation
                bullets[i].transform.GetChild(0).localPosition = Vector3.zero;
                //bullets[i].transform.GetChild(0).localRotation = Quaternion.identity;
                bullets[i].SetActive(true);
                break;
            }
        }
    }
}
