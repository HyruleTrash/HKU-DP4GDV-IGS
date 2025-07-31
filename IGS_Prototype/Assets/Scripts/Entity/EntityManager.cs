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
    
    #if UNITY_EDITOR
    private List<Tuple<Vector3, Vector3, bool>> debugRaycastList = new (); // origin, end point, is entity or not
    #endif

    public bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hit, out IEntity hitEntity, float maxDistance = math.INFINITY, int layerMask = Physics.DefaultRaycastLayers)
    {
        hitEntity = null;
        IEntity[] entities = entityPool.GetActiveObjects();
        bool result = Physics.Raycast(origin, direction, out hit, maxDistance, layerMask);
        
        if (!result || hit.collider == null)
            return false;
        
        bool isEntity = IsBodyAnEntity(hit.collider.gameObject, entities, out hitEntity);
        #if UNITY_EDITOR
        debugRaycastList.Add(new (origin, hit.point, isEntity));
        #endif
        return isEntity;
    }

    private bool IsBodyAnEntity(GameObject body, IEntity[] entities, out IEntity hitEntity)
    {
        hitEntity = null;
        foreach (var entity in entities)
        {
            if (entity == null || entity.Body == null || body == null)
                continue;
            
            if (entity.Body == body ||
                IsDescendant(body.transform, entity.Body.transform))
            {
                hitEntity = entity;
                return true;
            }
        }
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

    public void OnDrawGizmos()
    {
        #if UNITY_EDITOR
        IEntity[] entities = entityPool.GetActiveObjects();
        foreach (IEntity entity in entities)
        {
            Gizmos.color = new Color(0,0,1,0.5f);
            if (entity is TriggerEntity triggerEntity)
                Gizmos.DrawSphere(entity.Body.transform.position, triggerEntity.triggerRadius);
            
            if (!entity.Body)
                continue;
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(entity.Body.transform.position, 0.1f);
        }

        foreach (var data in debugRaycastList)
        {
            Gizmos.color = data.Item3 ? Color.red : Color.green;
            Gizmos.DrawLine(data.Item1, data.Item2);
        }
        
        #endif
    }
}