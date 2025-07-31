using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "GunInventory", menuName = "FPS/Guns/GunInventory")]
public class GunInventory : ScriptableObject
{
    [Header("Input")]
    [SerializeField] private InputActionReference shootAction;
    [SerializeField] private InputActionReference equipAction;
    [SerializeField] private InputActionReference reloadAction;
    [Header("Guns")]
    [SerializeField] private List<int> registeredGuns = new();

    private void OnValidate()
    {
        int lookupCount = GunLookup.instance.GetCount();
        List<int> newRegisteredGuns = new();
        foreach (var id in registeredGuns)
        {
            if (id >= lookupCount || id < 0)
                continue;
            newRegisteredGuns.Add(id);
        }
        registeredGuns = newRegisteredGuns;
    }

    public void Load(GunHandler gunHandler)
    {
        shootAction.action.performed += ctx => gunHandler.TryShoot();
        equipAction.action.performed += ctx => gunHandler.EquipNext();
        reloadAction.action.performed += ctx => gunHandler.Reload();
        
        foreach (var id in registeredGuns)
        {
            gunHandler.AddGun(GunLookup.instance.GetBuilder(id).Build());
        }
    }
}
