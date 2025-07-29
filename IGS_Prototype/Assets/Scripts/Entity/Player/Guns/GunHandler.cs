public class GunHandler
{
    public int activeGunId;
    
    public void EquipNext(GunInventory inventory)
    {
        inventory.GetGunAt(activeGunId).Unequip();
        activeGunId++;
        if (activeGunId > inventory.GetGunCount())
            activeGunId = 0;
        inventory.GetGunAt(activeGunId).Equip();
    }
    
    public void TryShoot(GunInventory gunInventory)
    {
        throw new System.NotImplementedException();
    }

    public void Reload(GunInventory gunInventory)
    {
        throw new System.NotImplementedException();
    }
}