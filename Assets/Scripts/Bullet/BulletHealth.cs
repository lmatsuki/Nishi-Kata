using NishiKata.Utilities;
using UnityEngine;

namespace NishiKata.Bullet
{
    public class BulletHealth : MonoBehaviour
    {
        public int health;
        public bool indestructible;

        private int initialHealth;

        void Start()
        {
            initialHealth = health;
        }

        void Update()
        {
            if (!indestructible && health <= 0)
            {
                ResetHealth();
                transform.parent.gameObject.SetActive(false);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.PlayerBullet))
            {
                health--;
                other.transform.parent.gameObject.SetActive(false);
            }
        }

        void ResetHealth()
        {
            health = initialHealth;
        }
    }
}
