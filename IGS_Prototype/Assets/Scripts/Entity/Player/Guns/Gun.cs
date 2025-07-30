using LucasCustomClasses;
using UnityEngine;

public class Gun
{
    public bool canShoot;
    public Timer fireRateTimer;
    public Timer reloadTimer;
    public IShootStrategy shootStrategy;
    private int ammoCapacity;
    private int currentAmmo = 0;
    private float baseDamage;

    public Gun(int ammoCapacity, float baseDamage)
    {
        this.ammoCapacity = ammoCapacity;
        this.baseDamage = baseDamage;
    }

    public void Equip()
    {
        reloadTimer.running = false;
        fireRateTimer.running = false;
        reloadTimer.Reset();
        fireRateTimer.Reset();
        canShoot = currentAmmo > 0;
        
        reloadTimer.onEnd = Reload;
        fireRateTimer.onEnd = () => canShoot = true;
    }

    public void Unequip()
    {
        reloadTimer.Reset();
        fireRateTimer.Reset();
    }

    public void TriggerReload()
    {
        canShoot = false;
        reloadTimer.Reset();
    }
    
    private void Reload()
    {
        currentAmmo = ammoCapacity;
        canShoot = true;
    }

    public void TryShoot(ShootData shootData)
    {
        if (currentAmmo <= 0)
            TriggerReload();
        if (!canShoot)
            return;
        
        shootData.baseDamage = baseDamage;
        shootStrategy.Shoot(shootData);
        
        currentAmmo--;
        canShoot = false;
        fireRateTimer.Reset();
    }
}