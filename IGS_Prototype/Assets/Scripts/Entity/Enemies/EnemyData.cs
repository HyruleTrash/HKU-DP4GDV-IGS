using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "EntityData/EnemyData")]
public class EnemyData : LevelDataEntity
{
    [SerializeField] private GameObject bodyPrefab;
    [SerializeField] private List<DamageType> Weaknesses { get; set; }
    [SerializeField] private List<DamageType> Affinities { get; set; }
    
    public override void Load()
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
            enemyEntity = new EnemyEntity();
            enemyEntity.Active = true;
            enemyEntity.Body = Instantiate(bodyPrefab, position, Quaternion.identity);
            
            Game.instance.GetEntityManager().entityPool.AddToPool(enemyEntity);
        }
    }
}