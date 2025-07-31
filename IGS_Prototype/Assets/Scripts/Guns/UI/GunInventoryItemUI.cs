using UnityEngine;

public class GunInventoryItemUI
{
    private GameObject instance;
    private GameObject equipIcon;
    private bool equipped;
    
    public GunInventoryItemUI(GameObject instance, GameObject equipIconPrefab, bool equipped)
    {
        this.instance = instance;
        this.equipIcon = GameObject.Instantiate(equipIconPrefab, instance.transform);
        this.equipped = equipped;
        if (equipped)
            Equip();
        else
            UnEquip();
    }

    public void Equip()
    {
        equipped = true;
        equipIcon.SetActive(true);
    }

    public void UnEquip()
    {
        equipped = false;
        equipIcon.SetActive(false);
    }
}