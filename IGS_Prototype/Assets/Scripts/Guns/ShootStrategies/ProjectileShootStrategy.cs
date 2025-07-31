using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileShootStrategy", menuName = "FPS/Guns/ShootStrategies/ProjectileShootStrategy")]
public class ProjectileShootStrategy : ScriptableObject, IShootStrategy
{
    [SerializeField] private ProjectileBuilder builder;
    [SerializeField] private float movementDirectionWeight = 1;
    
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

        AddShooterVelocity(data, projectileEntity, out Vector3 movementDirection);
        
        projectileEntity.rb.AddForce((data.direction + movementDirection * movementDirectionWeight) * data.force, ForceMode.Impulse);
        projectileEntity.baseDamage = data.baseDamage;
    }

    private void AddShooterVelocity(ShootData data, ProjectileEntity projectileEntity, out Vector3 movementDirection)
    {
        movementDirection = Vector3.zero;
        if (data.shooter == null || data.shooter.Body == null) return;
        
        Rigidbody shooterRigidbody = data.shooter.Body.GetComponent<Rigidbody>();
        if (!shooterRigidbody) return;
        
        projectileEntity.rb.linearVelocity = shooterRigidbody.linearVelocity;
        movementDirection = shooterRigidbody.linearVelocity.normalized;
    }
}
