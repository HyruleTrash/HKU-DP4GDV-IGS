using System;
using LucasCustomClasses;
using UnityEngine;

[CreateAssetMenu(fileName = "GunBuilder", menuName = "FPS/Guns/GunBuilder")]
public class GunBuilder : ScriptableObject
{
    [SerializeField] private float fireRate;
    [SerializeField] private float reloadTime;
    [SerializeField] private float baseDamage;
    [SerializeField] private int ammoCapacity;
    [SerializeField] private ScriptableObject shootStrategy;
    [SerializeField] private Vector3 bulletOrigin;
    [Header("For projectile based guns")]
    [SerializeField] private float force = 0;

    private void OnValidate()
    {
        if (shootStrategy == null)
            return;
        if (shootStrategy is not IShootStrategy strategy)
            shootStrategy = null;
    }

    public Gun Build()
    {
        Gun newGun = new Gun(ammoCapacity, baseDamage, force);
        newGun.fireRateTimer = new Timer(fireRate);
        newGun.reloadTimer = new Timer(reloadTime);
        newGun.shootStrategy = (IShootStrategy)shootStrategy;
        newGun.bulletOrigin = bulletOrigin;
        // TODO: make a use for this being a builder, such as adding randomness
        return newGun;
    }
}