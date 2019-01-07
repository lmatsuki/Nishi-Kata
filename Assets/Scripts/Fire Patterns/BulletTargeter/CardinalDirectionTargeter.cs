using UnityEngine;

public class CardinalDirectionTargeter : BaseBulletTargeter
{
    private int currentBulletIndex;
    private float baseRotation;

    public void Start()
    {
        currentBulletIndex = 0;
    }

    public override void TargetBullet(Transform bullet)
    {
        bullet.Rotate(0f, (90f * currentBulletIndex + baseRotation), 0f);

        IncrementBulletIndex();
    }

    void IncrementBulletIndex()
    {
        currentBulletIndex++;
        baseRotation += 10;

        if (currentBulletIndex >= 4)
        {
            currentBulletIndex = 0;
        }
    }
}
