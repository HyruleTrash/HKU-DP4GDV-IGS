using System;
using UnityEngine;

[CreateAssetMenu(fileName = "RaycastShootStrategy", menuName = "FPS/Guns/ShootStrategies/RaycastShootStrategy")]
public class RaycastShootStrategy : ScriptableObject, IShootStrategy
{
    [SerializeField] private Material BulletPathMaterial;
    [SerializeField] private float maxDistance = 1000f;
    [SerializeField] private LayerMask layerMask;
    
    public void Shoot(ShootData data)
    {
        EntityManager entityManagerReference = Game.instance.GetEntityManager();
        BulletLineEntity bulletLineEntity;

        if (entityManagerReference.entityPool.GetInActiveObject(typeof(BulletLineEntity), out var result))
        {
            bulletLineEntity = (BulletLineEntity)result;
            entityManagerReference.entityPool.ActivateObject(bulletLineEntity);
        }
        else
        {
            bulletLineEntity = new BulletLineEntity(BulletPathMaterial);
            bulletLineEntity.active = true;
            Game.instance.GetEntityManager().entityPool.AddToPool(bulletLineEntity);
        }

        if (Physics.Raycast(data.origin, data.direction, out RaycastHit hit, maxDistance, layerMask))
        {
            bulletLineEntity.SetData(data.origin, hit.point, BulletPathMaterial);
            // TODO: implement collision logic (aka damage dealing)
        }
        else
        {
            bulletLineEntity.SetData(data.origin, data.direction * maxDistance, BulletPathMaterial);
        }
    }
}
