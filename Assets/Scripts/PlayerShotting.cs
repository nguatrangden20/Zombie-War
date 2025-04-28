using UnityEngine;

public class PlayerShotting : MonoBehaviour
{
    [SerializeField] private PlayerAnimation playerAnimation;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private C4 c4;

    private SwitchWeapon switchWeapon;

    private void Awake()
    {
        switchWeapon = GetComponent<SwitchWeapon>();

        inputManager.OnC4Click += () => c4.DropC4(transform.position);
    }

    private void Update()
    {
        if (inputManager.IsFirePress)
        {
            var weapon = switchWeapon.GetCurrentWeapon();

            var bullets = weapon.GetBullet();

            if (bullets == null) return;

            foreach (var bullet in bullets)
            {
                bullet.FireBullet(bulletSpawnPoint.position);
            }

            playerAnimation.FireAnimation();
        }
    }
}
