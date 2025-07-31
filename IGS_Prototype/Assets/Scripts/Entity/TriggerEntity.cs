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
            Game.instance.GetEntityManager().OverlapRadiusOnLayerMask(Body.transform.position, triggerRadius, layerMask, hitEntities, onTrigger);
        }
    }

    public virtual void CustomUpdateAtFixedRate() { }
}