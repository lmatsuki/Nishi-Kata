using UnityEngine;

public class MapBoundary : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PlayerBullet))
        {
            other.transform.parent.gameObject.SetActive(false);
            AudioManager.instance.Play(Sounds.BulletHit);
        }

        if (other.CompareTag(Tags.EnemyBullet))
        {
            // Replace with Object pooler's remove method
            Destroy(other.transform.parent.gameObject);
            AudioManager.instance.Play(Sounds.BulletHit);
        }
    }
}
