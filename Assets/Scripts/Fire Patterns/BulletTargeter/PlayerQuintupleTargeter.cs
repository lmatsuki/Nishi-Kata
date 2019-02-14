using UnityEngine;

public class PlayerQuintupleTargeter : BaseBulletTargeter
{
    public float angleBetweenBullets;

    private int currentBulletIndex;

    public void Start()
    {
        base.Start();
        currentBulletIndex = -2;
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

        if (currentBulletIndex >= 3)
        {
            currentBulletIndex = -2;
        }
    }
}
