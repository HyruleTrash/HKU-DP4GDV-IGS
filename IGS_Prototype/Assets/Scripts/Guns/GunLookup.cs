using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "GunLookup", menuName = "FPS/Guns/GunLookup")]
public class GunLookup : ScriptableObjectSingleton<GunLookup>
{
    [SerializeField] private List<GunBuilder> registeredGuns = new();

    public GunBuilder GetBuilder(int id)
    {
        if (id >= registeredGuns.Count || id < 0)
            return null;
        return registeredGuns[id];
    }
    
    public int GetCount() => registeredGuns.Count;
}