using NishiKata.Audio;
using NishiKata.Managers;
using NishiKata.Utilities;
using UnityEngine;

namespace NishiKata.FirePatterns
{
    public class PlayerFire : BaseFire
    {
        public Transform firePosition;
        public float fireRate;
        public GameObject bullet;

        private float nextFireTime;

        protected override void Update()
        {
            base.Update();

            HandleFireInput();
        }

        private void HandleFireInput()
        {
            if (IsPressingFire() && !LevelManager.instance.isPaused &&
                canFire && Time.time > nextFireTime)
            {
                AudioManager.instance.Play(Sounds.PlayerFire);
                GameObject bulletPrefab = Poolable.TryGetPoolable(bullet);
                SetBulletTransform(bulletPrefab.transform);
                MoveBullet(bulletPrefab);

                nextFireTime = Time.time + fireRate;
            }
        }

        private bool IsPressingFire()
        {
            return Input.GetKey(KeyCode.Space) || (Input.touchCount > 0);
        }

        private void SetBulletTransform(Transform bullet)
        {
            bullet.position = firePosition.position;
            bullet.rotation = firePosition.rotation;

            // Also need to set child's local position and rotation
            if (bullet.childCount > 0)
            {
                bullet.GetChild(0).localPosition = Vector3.zero;
                bullet.GetChild(0).rotation = firePosition.rotation;
            }
        }
    }
}
