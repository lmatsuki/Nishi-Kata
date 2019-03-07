using NishiKata.Utilities;
using UnityEngine;

public class MapBoundary : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PlayerBullet) || other.CompareTag(Tags.EnemyBullet))
        {
            other.transform.parent.gameObject.SetActive(false);
            AudioManager.instance.Play(Sounds.BulletHit);
        }
    }
}
