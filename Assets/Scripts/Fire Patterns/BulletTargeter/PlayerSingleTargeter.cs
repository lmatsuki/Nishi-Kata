using UnityEngine;

public class PlayerSingleTargeter : BaseBulletTargeter
{
    public override void TargetBullet(Transform bullet)
    {
        bullet.LookAt(player);
    }
}
