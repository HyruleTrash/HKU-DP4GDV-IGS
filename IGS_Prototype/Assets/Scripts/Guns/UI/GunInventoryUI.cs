using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "GunInventoryUI", menuName = "FPS/Guns/GunInventoryUI")]
public class GunInventoryUI : ScriptableObjectSingleton<GunInventoryUI>
{
    [SerializeField] private GameObject inventoryPrefab;
    [SerializeField] private GameObject inventoryItemPrefab;
    [SerializeField] private GameObject inventoryEquipIconPrefab;
    private GameObject inventoryInstance;
    private List<GunInventoryItemUI> inventoryItemInstance = new ();
    
    public void AddGun(Gun newGun)
    {
        if (inventoryItemInstance.Count == 0)
            Setup();
        
        inventoryItemInstance.Add(new GunInventoryItemUI(Instantiate(inventoryItemPrefab, inventoryInstance.transform), inventoryEquipIconPrefab, false));
    }

    private void Setup()
    {
        inventoryInstance = Instantiate(inventoryPrefab, Game.instance.gameInterface.transform);
    }
    
    private bool IsIdValid(int id) => id >= 0 && id < inventoryItemInstance.Count;
    
    public void Equip(int id)
    {
        if (!IsIdValid(id))
            return;
        inventoryItemInstance[id].Equip();
    }

    public void UnEquip(int id)
    {
        if (!IsIdValid(id))
            return;
        inventoryItemInstance[id].UnEquip();
    }
}
