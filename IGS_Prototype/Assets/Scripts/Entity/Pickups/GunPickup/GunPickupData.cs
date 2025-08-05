using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GunPickupData", menuName = "EntityData/Pickups/GunPickupData")]
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
        GunPickupEntity gunPickupEntity;

        if (entityManagerReference.entityPool.GetInActiveObject(typeof(GunPickupEntity), out var result))
        {
            gunPickupEntity = (GunPickupEntity)result;
            gunPickupEntity.Body.transform.position = position;
            
            entityManagerReference.entityPool.ActivateObject(gunPickupEntity);
        }
        else
        {
            gunPickupEntity = new GunPickupEntity();
            gunPickupEntity.Active = true;
            gunPickupEntity.Body = Instantiate(bodyPrefab, position, Quaternion.identity);
            
            entityManagerReference.entityPool.AddToPool(gunPickupEntity);
        }
        
        gunPickupEntity.gunId = gunId;
        gunPickupEntity.triggerRadius = pickupRadius;
        gunPickupEntity.Setup();
        
        return gunPickupEntity;
    }
}