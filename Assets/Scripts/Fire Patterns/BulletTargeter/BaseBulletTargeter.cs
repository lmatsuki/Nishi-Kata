using UnityEngine;

public abstract class BaseBulletTargeter : MonoBehaviour
{
    protected Transform player;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player).transform;
    }

    public abstract void TargetBullet(Transform bullet);
}
