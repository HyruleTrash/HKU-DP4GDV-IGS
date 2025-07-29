using LucasCustomClasses;
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
        IEntity[] entitiesTemp = entityPool.GetInActiveObjects();
        foreach (var entity in entitiesTemp)
        {
            Debug.Log($"entity: {entity}, state: {entity.active}");
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
}