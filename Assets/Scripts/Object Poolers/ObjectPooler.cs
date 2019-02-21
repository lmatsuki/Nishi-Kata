using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler current;
    public int pooledAmount = 0;
    public GameObject pooledPrefab;

    private IList<GameObject> pooledObjects;

    void Start()
    {
        current = this;
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject spawnedObject = Instantiate(pooledPrefab, transform);
            spawnedObject.SetActive(false);
            pooledObjects.Add(spawnedObject);
        }
    }

    public void Spawn(Vector3 localPosition, Quaternion localRotation)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                pooledObjects[i].transform.position = localPosition;
                pooledObjects[i].transform.rotation = localRotation;

                // Also need to set child's local position and rotation
                pooledObjects[i].transform.GetChild(0).localPosition = Vector3.zero;
                //bullets[i].transform.GetChild(0).localRotation = Quaternion.identity;

                pooledObjects[i].SetActive(true);
                break;
            }
        }
    }
}
