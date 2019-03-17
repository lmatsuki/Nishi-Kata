using NishiKata.Audio;
using NishiKata.Utilities;
using UnityEngine;

public class MapBoundary : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PlayerBullet) || other.CompareTag(Tags.EnemyBullet))
        {
            if (other.name.Contains("Bullet"))
            {
                Poolable.TryPool(other.gameObject);
            }
            else
            {
                Poolable.TryPool(other.transform.parent.gameObject);
            }

            AudioManager.instance.Play(Sounds.BulletHit);
        }
    }
}
