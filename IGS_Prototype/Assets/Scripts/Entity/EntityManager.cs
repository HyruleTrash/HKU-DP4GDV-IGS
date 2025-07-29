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