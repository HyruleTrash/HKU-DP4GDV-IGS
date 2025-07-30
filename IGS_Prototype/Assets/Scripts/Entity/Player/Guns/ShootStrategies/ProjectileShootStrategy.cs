using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileShootStrategy", menuName = "FPS/Guns/ShootStrategies/ProjectileShootStrategy")]
public class ProjectileShootStrategy : ScriptableObject, IShootStrategy
{
    [SerializeField] private ProjectileBuilder builder;
    
    public void Shoot(ShootData data)
    {
        EntityManager entityManagerReference = Game.instance.GetEntityManager();
        ProjectileEntity projectileEntity;

        if (entityManagerReference.entityPool.GetInActiveObject(typeof(ProjectileEntity), out var result))
        {
            builder.Build((ProjectileEntity)result, out projectileEntity);
        }
        else
        {
            builder.Build(new ProjectileEntity(), out projectileEntity);
        }
        
        projectileEntity.Body.transform.position = data.origin;
        projectileEntity.rb.AddForce(data.direction * data.force);
        projectileEntity.baseDamage = data.baseDamage;
    }
}
