using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GunPickupData", menuName = "EntityData/GunPickupData")]
public class GunPickupData : LevelDataEntity
{
    [Header("Gun info")]
    [SerializeField] private int gunId;
    [Header("Pickup info")]
    [SerializeField] private GameObject bodyPrefab;
    [SerializeField] private float pickupRadius;

    private void OnValidate()
    {
        if (CheckGunId(gunId, out var lookupFound)) return;
        
        if (!lookupFound)
            return;
        gunId = -1;
    }

    private bool CheckGunId(int id, out bool lookupFound)
    {
        GunLookup guns = GunLookup.Instance;
        lookupFound = guns;
        if (!guns) return false;
        var lookupCount = GunLookup.Instance.GetCount();
        return gunId < lookupCount && gunId >= 0;
    }

    public override IEntity Load()
    {
        if (!CheckGunId(gunId, out var lookupFound)) return null;
        
        EntityManager entityManagerReference = Game.instance.GetEntityManager();
        GunPickup gunPickup;

        if (entityManagerReference.entityPool.GetInActiveObject(typeof(GunPickup), out var result))
        {
            gunPickup = (GunPickup)result;
            gunPickup.Body.transform.position = position;
            
            entityManagerReference.entityPool.ActivateObject(gunPickup);
        }
        else
        {
            gunPickup = new GunPickup();
            gunPickup.Active = true;
            gunPickup.Body = Instantiate(bodyPrefab, position, Quaternion.identity);
            
            entityManagerReference.entityPool.AddToPool(gunPickup);
        }
        
        gunPickup.gunId = gunId;
        gunPickup.triggerRadius = pickupRadius;
        gunPickup.Setup();
        
        return gunPickup;
    }
}