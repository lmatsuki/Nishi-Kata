public class PlayerBulletPooler : ObjectPooler
{
    public static PlayerBulletPooler current;

    protected override void Start()
    {
        base.Start();
        current = this;
    }
}
