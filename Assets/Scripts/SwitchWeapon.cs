using UnityEngine;
using UnityEngine.Animations.Rigging;

public enum WeaponType
{
    Ak47,
    Shotgun
}

public class SwitchWeapon : MonoBehaviour
{
    [SerializeField] private AK47 aK47;
    [SerializeField] private Shotgun shotgun;
    [SerializeField] private Transform shotGunLeftHand, ak47LeftHand;
    [SerializeField] private TwoBoneIKConstraint iKConstraint;
    [SerializeField] private InputManager inputManager;

    private IGun currentWeapon;

    private void Awake()
    {
        SwitchWeaponType(WeaponType.Ak47);
        inputManager.OnSwitchClick += SwitchWeaponType;
    }

    private void SwitchWeaponType()
    {
        if (currentWeapon as AK47) SwitchWeaponType(WeaponType.Shotgun);
        else SwitchWeaponType(WeaponType.Ak47);
    }

    public IGun GetCurrentWeapon()
    {
        return currentWeapon;
    }

    public void SwitchWeaponType(WeaponType type)
    {
        switch (type)
        {
            case WeaponType.Ak47:
                currentWeapon = aK47;
                iKConstraint.data.target = ak47LeftHand;
                aK47.gameObject.SetActive(true);
                shotgun.gameObject.SetActive(false);
                break;
            case WeaponType.Shotgun:
                currentWeapon = shotgun;
                iKConstraint.data.target = shotGunLeftHand;
                shotgun.gameObject.SetActive(true);
                aK47.gameObject.SetActive(false);
                break;
        }
    }
}
