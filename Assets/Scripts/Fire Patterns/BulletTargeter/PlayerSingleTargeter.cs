using UnityEngine;

namespace NishiKata.FirePatterns
{
    public class PlayerSingleTargeter : BaseBulletTargeter
    {
        public override void TargetBullet(Transform bullet)
        {
            bullet.LookAt(player);
        }
    }

}