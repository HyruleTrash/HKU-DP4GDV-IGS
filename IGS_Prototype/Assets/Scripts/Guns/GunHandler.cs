using System.Collections.Generic;
using UnityEngine;

public class GunHandler
{
    private PlayerEntity playerReference;
    private List<Gun> activeGuns = new();
    public int activeGunId = -1;

    public GunHandler(PlayerEntity playerReference)
    {
        this.playerReference = playerReference;
    }

    public void AddGun(Gun newGun) => activeGuns.Add(newGun);
    
    public void EquipNext()
    {
        Equip(activeGunId + 1);
    }

    public void Equip(int next)
    {
        if (IsGunIdValid(activeGunId))
            activeGuns[activeGunId].Unequip();
        activeGunId = next;
        if (activeGunId > activeGuns.Count - 1) // -1 due to zero indexing
            activeGunId = 0;
        activeGuns[activeGunId].Equip();
    }
    
    private bool IsGunIdValid(int id) => id >= 0 && id <= activeGuns.Count - 1;

    public void CustomUpdate()
    {
        if (!IsGunIdValid(activeGunId))
            return;
        Gun currentGun = activeGuns[activeGunId];
        currentGun.fireRateTimer.Update(Time.deltaTime);
        currentGun.reloadTimer.Update(Time.deltaTime);
    }
    
    public void TryShoot()
    {
        if (!IsGunIdValid(activeGunId))
            return;
        Gun currentGun = activeGuns[activeGunId];
        
        ShootData shootData = new ShootData();
        shootData.origin = playerReference.GetBulletOrigin();
        shootData.direction = playerReference.GetBulletDirection();
        
        currentGun.TryShoot(shootData);
    }

    public void Reload()
    {
        if (!IsGunIdValid(activeGunId))
            return;
        Gun currentGun = activeGuns[activeGunId];
        currentGun.TriggerReload();
    }
}