using System;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "RaycastShootStrategy", menuName = "FPS/Guns/ShootStrategies/RaycastShootStrategy")]
public class RaycastShootStrategy : ScriptableObject, IShootStrategy
{
    public class RaycastDamageHolder : IDamager
    {
        private readonly float damage;
        public RaycastDamageHolder(float damage) => this.damage = damage;
        public DamageData RetrieveDamage(IDamagable other)
        {
            return new DamageData(damage);
        }
    }
    
    [SerializeField] private Material bulletPathMaterial;
    [SerializeField] private float bulletPathDisappearSpeed = 2f;
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
            bulletLineEntity = new BulletLineEntity(bulletPathMaterial);
            bulletLineEntity.Active = true;
            entityManagerReference.entityPool.AddToPool(bulletLineEntity);
        }
        bulletLineEntity.disappearSpeed = bulletPathDisappearSpeed;

        bool hitAnEntity = entityManagerReference.Raycast(data.origin, data.direction, out RaycastHit hit, out var entity, maxDistance, layerMask);
        if (hit.collider != null)
        {
            bulletLineEntity.SetData(data.origin, hit.point, bulletPathMaterial);
            if (hitAnEntity && entity is IDamagable damagable)
            {
                damagable.TakeDamage(new RaycastDamageHolder(data.baseDamage));
            }
        }
        else
        {
            bulletLineEntity.SetData(data.origin, data.direction * maxDistance, bulletPathMaterial);
        }
    }
}
