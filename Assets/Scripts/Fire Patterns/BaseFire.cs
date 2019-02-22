using System.Collections;
using UnityEngine;

public abstract class BaseFire : MonoBehaviour
{
    public bool canFire;  
    public float initialDelay;

    protected virtual void Update()
    {
        if (initialDelay > 0)
        {
            StartCoroutine(WaitForInitialDelay());
        }
    }

    IEnumerator WaitForInitialDelay()
    {
        yield return new WaitForSeconds(initialDelay);

        initialDelay = 0;
        canFire = true;
    }

    public bool IsInitialDelayOver()
    {
        return (initialDelay == 0);
    }

    protected void MoveBullet(GameObject bulletPrefab)
    {
        BulletMover bulletMover = bulletPrefab.GetComponent<BulletMover>();

        if (bulletMover != null)
        {
            bulletMover.MoveForward();
        }
    }
}
