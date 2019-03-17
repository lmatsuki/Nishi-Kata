namespace NishiKata.ObjectPoolers
{
    public class PlayerBulletPooler : ObjectPooler
    {
        public static PlayerBulletPooler current;

        new void Start()
        {
            current = this;
            base.Start();
        }
    }
}
