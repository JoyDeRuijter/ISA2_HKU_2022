// Written by Joy de Ruijter
using UnityEngine;

[CreateAssetMenu(fileName = "New Ranged Weapon", menuName = "Weapons/RangedWeapon")]
public class RangedWeapon : Weapon
{
    #region Variables

    [Header("Weapon Properties")]
    public float damage;
    public float range;
    public float reloadTime;
    public float maxAmmo;
    public float fireRate;

    [Header("Weapon Visualisations")]
    [Space(10)]
    public ParticleSystem muzzleFlash;
    public Animator weaponAnimator;
    public Sprite icon;

    [Header("Weapon Sounds")]
    [Space(10)]
    public AudioSource shotSound;
    public AudioSource reloadSound;

    private float currentAmmo;

    #endregion

    private void Awake()
    {
        currentAmmo = maxAmmo;
    }

    private void Update()
    {
        Mathf.Clamp(currentAmmo, 0, maxAmmo);
    }
}
