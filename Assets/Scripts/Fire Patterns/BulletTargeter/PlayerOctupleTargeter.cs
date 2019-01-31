using UnityEngine;

public class PlayerOctupleTargeter : BaseBulletTargeter
{
    public Transform player;
    public float angleBetweenBullets;

    private int currentBulletIndex;

    public void Start()
    {
        currentBulletIndex = 0;
    }

    public override void TargetBullet(Transform bullet)
    {
        bullet.LookAt(player);
        bullet.Rotate(0f, (angleBetweenBullets * currentBulletIndex), 0f);

        IncrementBulletIndex();
    }

    void IncrementBulletIndex()
    {
        currentBulletIndex++;

        if (currentBulletIndex >= 8)
        {
            currentBulletIndex = 0;
        }
    }
}
