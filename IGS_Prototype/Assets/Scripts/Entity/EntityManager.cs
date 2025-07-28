using LucasCustomClasses;
using UnityEngine;

public class EntityManager
{
    public ObjectPool<IEntity> entityPool = new ObjectPool<IEntity>();
    
    public void Update()
    {
        IEntity[] entities = entityPool.GetActiveObjects();
        foreach (var entity in entities)
        {
            entity.CustomUpdate();
        }
    }
}