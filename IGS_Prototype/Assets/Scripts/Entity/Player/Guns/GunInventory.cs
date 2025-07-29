using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "GunInventory", menuName = "FPS/GunInventory")]
public class GunInventory : ScriptableObject
{
    [Header("Input")]
    [SerializeField] private InputActionReference shootAction;
    [SerializeField] private InputActionReference equipAction;
    [SerializeField] private InputActionReference reloadAction;
    [Header("Guns")]
    [SerializeField] private List<GunBuilder> guns;
    private List<Gun> activeGuns;

    public void Load(GunHandler gunHandler)
    {
        shootAction.action.performed += ctx => gunHandler.TryShoot(this);
        equipAction.action.performed += ctx => gunHandler.EquipNext(this);
        reloadAction.action.performed += ctx => gunHandler.Reload(this);
    }

    public Gun GetGunAt(int id) => activeGuns[id];
    public int GetGunCount() => activeGuns.Count;
}
