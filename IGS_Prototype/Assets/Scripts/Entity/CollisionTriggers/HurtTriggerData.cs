using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "HurtTriggerData", menuName = "EntityData/HurtTrigger")]
public class HurtTriggerData : LevelDataEntity
{
    private Transform parent;
    [SerializeField] private List<DamageType> weaknesses = new List<DamageType>();
    [SerializeField] private List<DamageType> affinities = new List<DamageType>();
    [SerializeField] private UnityEvent<DamageData> onDamaged;
    [SerializeField] public float triggerRadius;
    
    public override IEntity Load()
    {
        return Load(parent, damageData => { onDamaged.Invoke(damageData); }, weaknesses, affinities);
    }

    public IEntity Load(Transform parent, Action<DamageData> onDamaged, List<DamageType> weaknesses, List<DamageType> affinities)
    {
        EntityManager entityManagerReference = Game.instance.GetEntityManager();
        HurtTriggerEntity hurtTriggerEntity;

        if (entityManagerReference.entityPool.GetInActiveObject(typeof(HurtTriggerEntity), out var result))
        {
            hurtTriggerEntity = (HurtTriggerEntity)result;
            hurtTriggerEntity.Body.transform.parent = parent;
            hurtTriggerEntity.Body.transform.localPosition = position;
            hurtTriggerEntity.Initialize(onDamaged.Invoke,weaknesses, affinities, triggerRadius);
            
            entityManagerReference.entityPool.ActivateObject(hurtTriggerEntity);
        }
        else
        {
            hurtTriggerEntity = new HurtTriggerEntity();
            hurtTriggerEntity.Initialize(onDamaged.Invoke,weaknesses, affinities, triggerRadius);
            
            hurtTriggerEntity.Body = new GameObject("HurtTriggerEntity")
            {
                transform = { parent = parent }
            };
            hurtTriggerEntity.Body.transform.localPosition = position;
            hurtTriggerEntity.Active = true;
            
            Game.instance.GetEntityManager().entityPool.AddToPool(hurtTriggerEntity);
        }

        return hurtTriggerEntity;
    }
}