using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerEntity : IEntity
{
    public GameObject Body { get; set; }
    protected SphereCollider collider = null;
    public bool Active { get; set; }
    protected Action<IEntity> onTrigger;
    protected System.Type[] layerMasks;
    public float triggerRadius;
    public bool isChecking = true;

    public virtual void OnEnableObject()
    {
        if (Body == null)
            Body = new GameObject("TriggerEntity");
        if (collider == null)
            SetupCollider();
    }
    public virtual void OnDisableObject() { }

    private void SetupCollider()
    {
        collider = Body.AddComponent<SphereCollider>();
        collider.isTrigger = true;
        collider.radius = triggerRadius;
    }

    public virtual void DoDie()
    {
        Game.instance.GetEntityManager().entityPool.DeactivateObject(this);
    }

    public virtual void CustomUpdate()
    {
        if (!isChecking || !Active || layerMasks == null || layerMasks.Length == 0)
            return;
        List<IEntity> hitEntities = new();
        foreach (var layerMask in layerMasks)
        {
            Game.instance.GetEntityManager().OverlapRadiusOnLayerMask(Body.transform.position, triggerRadius, layerMask, hitEntities, onTrigger);
        }
    }

    public virtual void CustomUpdateAtFixedRate() { }
}