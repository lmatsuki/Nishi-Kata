namespace NishiKata.ObjectPoolers
{
    public class StrongBulletPooler : ObjectPooler
    {
        public static StrongBulletPooler current;

        new void Start()
        {
            current = this;
            base.Start();
        }
    }

}