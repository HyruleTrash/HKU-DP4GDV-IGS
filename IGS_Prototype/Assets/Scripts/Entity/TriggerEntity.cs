using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerEntity : IEntity
{
    public GameObject Body { get; set; }
    public bool Active { get; set; }
    protected Action<IEntity> onTrigger;
    protected System.Type[] layerMasks;
    public float triggerRadius;
    
    public virtual void OnEnableObject() { }
    public virtual void OnDisableObject() { }

    public virtual void DoDie()
    {
        Game.instance.GetEntityManager().entityPool.DeactivateObject(this);
    }

    public virtual void CustomUpdate()
    {
        List<IEntity> hitEntities = new();
        foreach (var layerMask in layerMasks)
        {
            CheckLayerMask(layerMask, hitEntities);
        }
    }

    private void CheckLayerMask(System.Type layerMask, List<IEntity> hitEntities)
    {
        IEntity[] entities = Game.instance.GetEntityManager().entityPool.GetActiveObjects(layerMask);
        foreach (IEntity entity in entities)
        {
            float otherRadius = 0;
            if (entity is TriggerEntity otherTriggerEntity)
                otherRadius = otherTriggerEntity.triggerRadius;
            
            if (!hitEntities.Contains(entity) && entity.Active && 
                Vector3.Distance(Body.transform.position, entity.Body.transform.position) <= triggerRadius + otherRadius)
            {
                hitEntities.Add(entity);
                onTrigger.Invoke(entity);
            }
        }
    }

    public virtual void CustomUpdateAtFixedRate() { }
}