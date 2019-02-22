public class WeakBulletPooler : ObjectPooler
{
    public static WeakBulletPooler current;

    new void Start()
    {
        current = this;
        base.Start();
    }
}
