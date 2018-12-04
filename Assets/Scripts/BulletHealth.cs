using UnityEngine;

public class BulletHealth : MonoBehaviour
{
    public int health;

	void Start ()
    {
		
	}
	
	void Update ()
    {
		if (health <= 0)
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
