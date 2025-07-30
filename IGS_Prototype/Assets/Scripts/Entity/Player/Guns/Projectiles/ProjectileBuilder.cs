using UnityEngine;


[CreateAssetMenu(fileName = "ProjectileBuilder", menuName = "FPS/Guns/ProjectileBuilder")]
public class ProjectileBuilder : ScriptableObject
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileHitRadius;

    public void Build(ProjectileEntity inactiveEntity, out ProjectileEntity activeEntity)
    {
        if (inactiveEntity == null || inactiveEntity.Body == null)
        {
            inactiveEntity = new ProjectileEntity();
            inactiveEntity.Body = GameObject.Instantiate(projectilePrefab);
            inactiveEntity.rb = inactiveEntity.Body.AddComponent<Rigidbody>();
            inactiveEntity.triggerRadius = projectileHitRadius;
        }
        
        // TODO randomize damagetypes
        
        inactiveEntity.Active = true;
        Game.instance.GetEntityManager().entityPool.ActivateObject(inactiveEntity);
        activeEntity = inactiveEntity;
    }
}