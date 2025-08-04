using System;
using LucasCustomClasses;
using UnityEngine;
using Random = UnityEngine.Random;


[CreateAssetMenu(fileName = "ProjectileBuilder", menuName = "FPS/Guns/ProjectileBuilder")]
public class ProjectileBuilder : ScriptableObject
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileHitRadius;
    [SerializeField] private int maxDecorators;
    [SerializeField] private float despawnTimeAfterHit;

    public void Build(ProjectileEntity inactiveEntity, out ProjectileEntity activeEntity)
    {
        if (inactiveEntity == null || inactiveEntity.Body == null)
            inactiveEntity = CreateBase();
        
        inactiveEntity.triggerRadius = projectileHitRadius;
        inactiveEntity.ClearDamageTypes();
        AddDamageTypes(inactiveEntity);
        
        inactiveEntity.Active = true;
        Game.instance.GetEntityManager().entityPool.ActivateObject(inactiveEntity);
        activeEntity = inactiveEntity;
        
        activeEntity.despawnTimer = new Timer(despawnTimeAfterHit);
    }

    private ProjectileEntity CreateBase()
    {
        ProjectileEntity projectile = new ProjectileEntity();
        projectile.Body = GameObject.Instantiate(projectilePrefab);
        projectile.rb = projectile.Body.AddComponent<Rigidbody>();
        return projectile;
    }

    private void AddDamageTypes(ProjectileEntity projectile)
    {
        Array options = Enum.GetValues(typeof(DamageType));
        int amountOfDecorators = Random.Range(1, maxDecorators);
        for (int i = 0; i < amountOfDecorators; i++)
        {
            while (true)
            {
                DamageType type = (DamageType)Random.Range(0, options.Length);
                if (!projectile.HasAffinity(type))
                {
                    projectile.AddDamageTypeDecorator(type);
                    break;
                }
            }
        }
    }
}