using System.Collections;
using UnityEngine;

public class Shotgun : Gun
{
    public int NumberBullet = 10;

    private float timeEffect = 0.1f;

    public override Bullet[] HandleBullet()
    {
        StartCoroutine(HandleEffect());

        Bullet[] bullets = new Bullet[NumberBullet];
        for (int i = 0; i < NumberBullet; i++)
        {
            bullets[i] = bulletPool.Pool.Get();
            bullets[i].Direction = GetDirection();
        }

        return bullets;
    }

    private IEnumerator HandleEffect()
    {
        SoundManager.Instance.PlaySFX(SoundType.ShotGun);
        effectFire.SetActive(true);
        yield return new WaitForSeconds(timeEffect);
        effectFire.SetActive(false);
    }

    protected override void Update()
    {
    }
}
