using NishiKata.Audio;
using NishiKata.Managers;
using NishiKata.Utilities;
using UnityEngine;

namespace NishiKata.FirePatterns
{
    public class FireWeak : BaseFire
    {
        public GameObject[] bullets;
        public Transform firePosition;
        public float fireRate;
        public float timeBetweenFire;
        public float[] individualDelay;

        private float nextBulletTime;
        private float nextFireTime;
        private int currentBulletIndex;
        private BaseBulletTargeter bulletTargeter;

        void Start()
        {
            currentBulletIndex = 0;
            bulletTargeter = GetComponent<BaseBulletTargeter>();
        }

        protected override void Update()
        {
            base.Update();

            if (canFire && IsInitialDelayOver() &&
                Time.time > nextFireTime &&
                !LevelManager.instance.isPaused)
            {
                FireShot();
            }
        }

        private void FireShot()
        {
            if (Time.time > nextBulletTime)
            {
                GameObject bulletPrefab = GetInitializedPrefab(bullets[currentBulletIndex], firePosition.position, firePosition.rotation);
                AimBullet(bulletPrefab.transform);

                // Need to move the bullet AFTER the change in direction/forward
                MoveBullet(bulletPrefab);
                AudioManager.instance.Play(Sounds.EnemyFire);

                // Keep track of current bullet
                currentBulletIndex = (currentBulletIndex + 1) % bullets.Length;
                nextBulletTime = Time.time + fireRate + AddIndividualDelay();
            }

            // Indicates that all bullets have been fired
            if (currentBulletIndex == 0)
            {
                nextFireTime = Time.time + timeBetweenFire;
            }
        }

        private GameObject GetInitializedPrefab(GameObject bulletPrefab, Vector3 position, Quaternion rotation)
        {
            GameObject pooledPrefab = Poolable.TryGetPoolable(bulletPrefab);

            pooledPrefab.transform.position = position;
            pooledPrefab.transform.rotation = rotation;

            // Also need to set child's local position and rotation
            if (pooledPrefab.transform.childCount > 0)
            {
                pooledPrefab.transform.GetChild(0).localPosition = Vector3.zero;
                pooledPrefab.transform.GetChild(0).rotation = rotation;
            }

            return pooledPrefab;
        }

        private void AimBullet(Transform bullet)
        {
            if (bulletTargeter != null)
            {
                bulletTargeter.TargetBullet(bullet);
            }
        }

        private float AddIndividualDelay()
        {
            if (individualDelay != null &&
                individualDelay.Length > currentBulletIndex)
            {
                return individualDelay[currentBulletIndex];
            }

            return 0f;
        }
    }
}
