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
    [SerializeField] private List<GunBuilder> registeredGuns = new();

    public void Load(GunHandler gunHandler)
    {
        shootAction.action.performed += ctx => gunHandler.TryShoot();
        equipAction.action.performed += ctx => gunHandler.EquipNext();
        reloadAction.action.performed += ctx => gunHandler.Reload();
        
        // TODO: port this to a triggerEntity item pickup
        foreach (var builder in registeredGuns)
        {
            gunHandler.AddGun(builder.Build());
        }
    }
}
