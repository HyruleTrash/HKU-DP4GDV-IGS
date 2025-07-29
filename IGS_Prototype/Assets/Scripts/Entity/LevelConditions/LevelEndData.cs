using UnityEngine;

[CreateAssetMenu(fileName = "LevelEndData", menuName = "EntityData/LevelConditions/LevelEndData")]
public class LevelEndData : LevelDataEntity
{
    [SerializeField] private GameObject bodyPrefab;
    public float triggerRadius;
    public override void Load()
    {
        // TODO: Make check for pre existing levelEnd
        
        EntityManager entityManagerReference = Game.instance.GetEntityManager();
        LevelEndEntity levelEndEntity;

        if (entityManagerReference.entityPool.GetInActiveObject(typeof(LevelEndEntity), out var result))
        {
            levelEndEntity = (LevelEndEntity)result;
            levelEndEntity.body.transform.position = position;
            levelEndEntity.data = this;
            entityManagerReference.entityPool.ActivateObject(levelEndEntity);
        }
        else
        {
            levelEndEntity = new LevelEndEntity(this);
            levelEndEntity.active = true;
            levelEndEntity.body = Instantiate(bodyPrefab, position, Quaternion.identity);
            
            Game.instance.GetEntityManager().entityPool.AddToPool(levelEndEntity);
        }
    }
}