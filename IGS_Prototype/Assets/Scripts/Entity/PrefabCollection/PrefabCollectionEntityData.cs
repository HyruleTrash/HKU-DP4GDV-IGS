using UnityEngine;

[CreateAssetMenu(fileName = "PrefabCollectionEntityData", menuName = "EntityData/PrefabCollectionEntityData")]
public class PrefabCollectionEntityData : LevelDataEntity
{
    [SerializeField] private GameObject prefab;
    
    public override IEntity Load()
    {
        var entityManagerReference = Game.instance.GetEntityManager();
        PrefabCollectionEntity prefabCollectionEntity;
        
        prefabCollectionEntity = new PrefabCollectionEntity
        {
            Active = true,
            Body = Instantiate(prefab, position, Quaternion.identity)
        };

        entityManagerReference.entityPool.AddToPool(prefabCollectionEntity);

        return prefabCollectionEntity;
    }
}