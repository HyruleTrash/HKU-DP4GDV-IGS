using System.Collections.Generic;
using UnityEngine;

public class GunHandler
{
    private PlayerEntity playerReference;
    private Dictionary<int, Gun> activeGuns = new();
    private GameObject bulletOrigin;
    private bool active = true;
    public int activeGunId = -1;

    public GunHandler(PlayerEntity playerReference)
    {
        this.playerReference = playerReference;
        bulletOrigin = new GameObject("BulletOrigin")
        {
            transform =
            {
                position = Vector3.zero,
                parent = playerReference.GetCameraTransform()
            }
        };
    }

    public void AddGun(Gun newGun, int id)
    {
        activeGuns.Add(id, newGun);
        GunInventoryUI.Instance.AddGun(newGun);
    }

    public void EquipNext()
    {
        if (active)
            Equip(activeGunId + 1);
    }

    public void Equip(int next)
    {
        if (!active)
            return;
        if (IsGunIdValid(activeGunId))
        {
            activeGuns[activeGunId].Unequip();
            GunInventoryUI.Instance.UnEquip(activeGunId);
        }
        activeGunId = next;
        if (activeGunId > activeGuns.Count - 1) // -1 due to zero indexing
            activeGunId = 0;
        activeGuns[activeGunId].Equip();
        GunInventoryUI.Instance.Equip(activeGunId);
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
        if (!active)
            return;
        if (!IsGunIdValid(activeGunId))
            return;
        Gun currentGun = activeGuns[activeGunId];
        bulletOrigin.transform.localPosition = currentGun.bulletOrigin;
        
        ShootData shootData = new ShootData();
        shootData.origin = bulletOrigin.transform.position;
        shootData.direction = playerReference.GetBulletDirection();
        
        currentGun.TryShoot(shootData);
    }

    public void Reload()
    {
        if (!active)
            return;
        if (!IsGunIdValid(activeGunId))
            return;
        Gun currentGun = activeGuns[activeGunId];
        currentGun.TriggerReload();
    }

    public void Destroy()
    {
        active = false;
    }
}