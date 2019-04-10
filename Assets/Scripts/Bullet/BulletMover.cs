using NishiKata.Managers;
using UnityEngine;

namespace NishiKata.Bullet
{
    public class BulletMover : MonoBehaviour
    {
        public float speed;

        private Rigidbody bulletRigidbody;

        private void FixedUpdate()
        {
            MoveForward();
        }

        public void MoveForward()
        {
            if (bulletRigidbody == null)
            {
                bulletRigidbody = GetComponentInChildren<Rigidbody>();
            }

            if (LevelManager.instance.isPaused)
            {
                bulletRigidbody.velocity = Vector3.zero;
                return;
            }

            bulletRigidbody.velocity = transform.forward * speed;
        }
    }
}
