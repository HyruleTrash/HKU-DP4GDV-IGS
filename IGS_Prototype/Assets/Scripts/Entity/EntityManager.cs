using System;
using System.Collections.Generic;
using LucasCustomClasses;
using Unity.Mathematics;
using UnityEngine;

public class EntityManager
{
    public ObjectPool<IEntity> entityPool = new ObjectPool<IEntity>();
    
    public void CustomUpdate()
    {
        IEntity[] entities = entityPool.GetActiveObjects();
        foreach (var entity in entities)
        {
            entity.CustomUpdate();
        }
    }

    public void CustomUpdateAtFixedRate()
    {
        IEntity[] entities = entityPool.GetActiveObjects();
        foreach (var entity in entities)
        {
            entity.CustomUpdateAtFixedRate();
        }
    }

    public void DeactivateAllEntities()
    {
        IEntity[] entities = entityPool.GetActiveObjects();
        foreach (var entity in entities)
        {
            entityPool.DeactivateObject(entity);
        }
    }
    
    public void OverlapRadiusOnLayerMask(Vector3 origin, float radius, System.Type layerMask, List<IEntity> hitEntities, Action<IEntity> result)
    {
        IEntity[] entities = entityPool.GetActiveObjects(layerMask);
        foreach (IEntity entity in entities)
        {
            float otherRadius = 0;
            if (entity is TriggerEntity otherTriggerEntity)
                otherRadius = otherTriggerEntity.triggerRadius;
            
            if (!hitEntities.Contains(entity) && entity.Active && 
                Vector3.Distance(origin, entity.Body.transform.position) <= radius + otherRadius)
            {
                hitEntities.Add(entity);
                result.Invoke(entity);
            }
        }
    }

    public bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hit, out IEntity hitEntity, float maxDistance = math.INFINITY, int layerMask = Physics.DefaultRaycastLayers)
    {
        IEntity[] entities = entityPool.GetActiveObjects();
        if (Physics.Raycast(origin, direction, out hit, maxDistance, layerMask))
        {
            foreach (var entity in entities)
            {
                if (entity.Body == hit.collider.gameObject ||
                    IsDescendant(hit.collider.gameObject.transform, entity.Body.transform))
                {
                    hitEntity = entity;
                    return true;
                }
            }
        }
        hitEntity = null;
        return false;
    }
    
    private static bool IsDescendant(Transform potentialDescendant, Transform potentialAncestor)
    {
        if (!potentialDescendant || !potentialAncestor)
            return false;
            
        Transform current = potentialDescendant.parent;
        while (current != null)
        {
            if (current == potentialAncestor)
                return true;
            current = current.parent;
        }
        return false;
    }
}