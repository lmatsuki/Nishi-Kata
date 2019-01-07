using UnityEngine;

public class PlayerSingleTargeter : BaseBulletTargeter
{
    public Transform player;

    public override void TargetBullet(Transform bullet)
    {
        bullet.LookAt(player);
    }
}
