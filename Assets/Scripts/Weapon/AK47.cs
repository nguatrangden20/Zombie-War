public class AK47 : Gun
{
    public override Bullet[] HandleBullet()
    {
        var bullet = bulletPool.Pool.Get();
        bullet.Direction = GetDirection();

        return new Bullet[] { bullet };
    }
}
