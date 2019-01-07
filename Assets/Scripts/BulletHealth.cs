using UnityEngine;

public class BulletHealth : MonoBehaviour
{
    public int health;
    public bool indestructible;

	void Start ()
    {
		
	}
	
	void Update ()
    {
		if (!indestructible && health <= 0)
        {
            Destroy(transform.parent.gameObject);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PlayerBullet))
        {
            health--;
            Destroy(other.transform.parent.gameObject);
        }
    }
}
