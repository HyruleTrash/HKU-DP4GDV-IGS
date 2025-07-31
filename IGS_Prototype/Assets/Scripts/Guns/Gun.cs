using LucasCustomClasses;
using UnityEngine;

public class Gun
{
    public Vector3 bulletOrigin;
    public bool canShoot;
    public Timer fireRateTimer;
    public Timer reloadTimer;
    public IShootStrategy shootStrategy;
    private int ammoCapacity;
    private int CurrentAmmo
    {
        get => currentAmmo;
        set
        {
            currentAmmo = value;
            UpdateUI();
        }
    }

    private int currentAmmo = 0;
    private float baseDamage;
    private float force;

    public Gun(int ammoCapacity, float baseDamage, float force)
    {
        this.ammoCapacity = ammoCapacity;
        this.currentAmmo = ammoCapacity;
        this.baseDamage = baseDamage;
        this.force = force;
    }

    public void Equip()
    {
        reloadTimer.running = false;
        fireRateTimer.running = false;
        reloadTimer.Reset();
        fireRateTimer.Reset();
        canShoot = CurrentAmmo > 0;
        reloadTimer.onEnd = Reload;
        fireRateTimer.onEnd = () => canShoot = true;
        UpdateUI();
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
        SetReloadUI("Reloading...");
    }
    
    private void Reload()
    {
        CurrentAmmo = ammoCapacity;
        canShoot = true;
        SetReloadUI("");
    }

    private void SetReloadUI(string text)
    {
        GunInventoryUI ui = GunInventoryUI.Instance;
        if (!ui.inventoryReloadingPrefab.instance)
            return;
        ui.inventoryReloadingPrefab.instanceTextComponent.text = text;
    }

    public void TryShoot(ShootData shootData)
    {
        if (CurrentAmmo <= 0 && !reloadTimer.running)
            TriggerReload();
        if (!canShoot)
            return;
        
        shootData.baseDamage = baseDamage;
        shootData.force = force;
        shootStrategy.Shoot(shootData);
        
        CurrentAmmo--;
        canShoot = false;
        fireRateTimer.Reset();
    }

    private void UpdateUI()
    {
        GunInventoryUI ui = GunInventoryUI.Instance;
        if (!ui.inventoryAmmoCounterPrefab.instance)
            return;
        GunInventoryUI.Instance.inventoryAmmoCounterPrefab.instanceTextComponent.text = $"{CurrentAmmo} / {ammoCapacity}";
    }
}