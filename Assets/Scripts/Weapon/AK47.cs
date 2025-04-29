public class AK47 : Gun
{
    public override Bullet[] HandleBullet()
    {
        SoundManager.Instance.PlaySFX(SoundType.AK47);
        var bullet = bulletPool.Pool.Get();
        bullet.Direction = GetDirection();

        return new Bullet[] { bullet };
    }
}
