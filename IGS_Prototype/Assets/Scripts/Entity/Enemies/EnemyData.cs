using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "EntityData/EnemyData")]
public class EnemyData : LevelDataEntity
{
    [SerializeField] private GameObject bodyPrefab;
    [SerializeField] private List<DamageType> weaknesses = new List<DamageType>();
    [SerializeField] private List<DamageType> affinities = new List<DamageType>();
    [SerializeField] private HealthData healthData;
    [SerializeField] private List<HurtTriggerData> hurtTriggerDatas = new List<HurtTriggerData>();
    
    public override IEntity Load()
    {
        EntityManager entityManagerReference = Game.instance.GetEntityManager();
        EnemyEntity enemyEntity;

        if (entityManagerReference.entityPool.GetInActiveObject(typeof(EnemyEntity), out var result))
        {
            enemyEntity = (EnemyEntity)result;
            enemyEntity.Body.transform.position = position;
            
            entityManagerReference.entityPool.ActivateObject(enemyEntity);
        }
        else
        {
            enemyEntity = new EnemyEntity(healthData);
            enemyEntity.Active = true;
            enemyEntity.Body = Instantiate(bodyPrefab, position, Quaternion.identity);
            
            Game.instance.GetEntityManager().entityPool.AddToPool(enemyEntity);
        }

        List<HurtTriggerEntity> hurtTriggers = new List<HurtTriggerEntity>();
        foreach (HurtTriggerData hurtTriggerData in hurtTriggerDatas)
            hurtTriggerData.Load(enemyEntity.Body.transform, enemyEntity.TakeDamage, weaknesses, affinities);
        enemyEntity.hurtTriggers = hurtTriggers.ToArray();

        return enemyEntity;
    }
}