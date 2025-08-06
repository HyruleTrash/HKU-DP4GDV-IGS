using System.Collections.Generic;
using UnityEngine;

public class GunHandler
{
    private PlayerEntity playerReference;
    private Dictionary<int, Gun> activeGuns = new();
    private List<int> gunOrder = new();
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
        if (!activeGuns.TryAdd(id, newGun))
            return;
        gunOrder.Add(id);
        GunInventoryUI.Instance.AddGun(newGun);
    }

    public void EquipNext()
    {
        if (!active) return;
        
        var currentIndexInOrder = gunOrder.IndexOf(activeGunId);
        var nextGunId = currentIndexInOrder + 1 == gunOrder.Count ? 0 : gunOrder[currentIndexInOrder + 1];
        if (!activeGuns.ContainsKey(nextGunId))
            nextGunId = 0;

        Unequip(activeGunId);
        Equip(nextGunId);
        activeGunId = nextGunId;
    }

    public void Equip(int id)
    {
        if (!active || !activeGuns.ContainsKey(id))
            return;
        activeGuns[id].Equip();
        GunInventoryUI.Instance.Equip(activeGuns[id]);
    }

    public void Unequip(int id)
    {
        if (!active || !activeGuns.ContainsKey(id))
            return;
        activeGuns[id].Unequip();
        GunInventoryUI.Instance.UnEquip(activeGuns[id]);
    }

    public void CustomUpdate()
    {
        if (!activeGuns.ContainsKey(activeGunId))
            return;
        Gun currentGun = activeGuns[activeGunId];
        currentGun.fireRateTimer.Update(Time.deltaTime);
        currentGun.reloadTimer.Update(Time.deltaTime);
    }
    
    public void TryShoot()
    {
        if (!active)
            return;
        if (!activeGuns.ContainsKey(activeGunId))
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
        if (!activeGuns.ContainsKey(activeGunId))
            return;
        Gun currentGun = activeGuns[activeGunId];
        currentGun.TriggerReload();
    }

    public void Destroy()
    {
        active = false;
    }
}