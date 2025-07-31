using UnityEngine;

[CreateAssetMenu(fileName = "LevelEndData", menuName = "EntityData/LevelConditions/LevelEndData")]
public class LevelEndData : LevelDataEntity
{
    [SerializeField] private GameObject bodyPrefab;
    public float triggerRadius;
    public override IEntity Load()
    {
        EntityManager entityManagerReference = Game.instance.GetEntityManager();
        LevelEndEntity levelEndEntity;

        if (entityManagerReference.entityPool.GetInActiveObject(typeof(LevelEndEntity), out var result))
        {
            levelEndEntity = (LevelEndEntity)result;
            levelEndEntity.Body.transform.position = position;
            levelEndEntity.data = this;
            entityManagerReference.entityPool.ActivateObject(levelEndEntity);
        }
        else
        {
            levelEndEntity = new LevelEndEntity(this);
            levelEndEntity.Active = true;
            levelEndEntity.Body = Instantiate(bodyPrefab, position, Quaternion.identity);
            
            Game.instance.GetEntityManager().entityPool.AddToPool(levelEndEntity);
        }
        
        return levelEndEntity;
    }
}